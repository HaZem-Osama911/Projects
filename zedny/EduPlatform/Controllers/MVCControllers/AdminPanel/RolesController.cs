using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EduPlatform.Controllers.MVCControllers.AdminPanel
{
    [Authorize(Roles = "Super_Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        // عرض كل Roles
        public IActionResult Roles_Dashboard()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        // Create - GET
        public IActionResult ADD_Roles()
        {
            return View();
        }

        // Create - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ADD_Roles(string roleName)
        {
            if (!string.IsNullOrEmpty(roleName))
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                    return RedirectToAction("Roles_Dashboard");
                }
                ModelState.AddModelError("", "Role already exists");
            }
            return View();
        }

        // Edit - GET
        public async Task<IActionResult> Edit_Roles(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();
            return View(role);
        }

        // Edit - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Roles(string id, string newRoleName)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();

            role.Name = newRoleName;
            role.NormalizedName = newRoleName.ToUpper();
            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded) return RedirectToAction("Roles_Dashboard");

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(role);
        }

        // Delete - GET
        public async Task<IActionResult> Delete_Roles(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();
            return View(role);
        }

        // Delete - POST
        [HttpPost, ActionName("Delete_Roles")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
                await _roleManager.DeleteAsync(role);

            return RedirectToAction("Roles_Dashboard");
        }
    }
}
