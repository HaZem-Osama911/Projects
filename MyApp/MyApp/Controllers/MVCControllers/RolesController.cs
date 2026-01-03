using Microsoft.EntityFrameworkCore;
using MyApp.ViewModels.Roles;
using System.Threading.Tasks;

namespace MyApp.Controllers.MVCControllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _roleManager.Roles.ToListAsync());
        }

        [HttpGet]
        public IActionResult RoleForm()
        {
            var model = new RoleFormViewModel();
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ADD(RoleFormViewModel model)
        {
            if (!ModelState.IsValid) return View("Index", await _roleManager.Roles.ToListAsync());

            var roleIsExist = await _roleManager.RoleExistsAsync(model.RoleName);
            if (roleIsExist)
            {
                ModelState.AddModelError("Name", "Roles Is Exist");
                return View("Index", await _roleManager.Roles.ToListAsync());
            }

            await _roleManager.CreateAsync(new IdentityRole(model.RoleName.Trim()));
            return RedirectToAction("Index");
        }

        // ----------------------- Edit ------------------------

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if(id==null) return NotFound();

            var role = await _roleManager.FindByIdAsync(id);
            if(role == null) return NotFound();

            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, string name)
        {
            if (id == null) return NotFound();

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();

            role.Name = name;
            role.NormalizedName = name.ToUpper();

            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded) return RedirectToAction(nameof(Index));

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(role);
        }


        // ---------------------- Delete ---------------------

        [HttpGet]
        public async Task<IActionResult>Delete(string id)
        {
            if (id==null) return NotFound();

            var role=await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();

            return View(role);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (id==null) return NotFound();

            var role=await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded) return RedirectToAction(nameof(Index));

            foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);

            return View(role);
        }

    }
}
