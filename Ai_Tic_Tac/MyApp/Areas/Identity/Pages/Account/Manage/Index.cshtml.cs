#nullable disable
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyApp.Models.UserModels;
namespace MyApp.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [TempData]
        public string StatusMessage { get; set; }
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            [Required]
            [Display(Name = "User Name")]
            public string UserName { get; set; }
            [Required]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }
            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            // الصورة متخزنة byte[] في الداتا بيز
            [Display(Name = "Profile Picture")]
            public IFormFile ProfilePicture { get; set; }
        }
        private async Task LoadAsync(ApplicationUser user)
        {
            Input = new InputModel
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber
            };
            ViewData["ProfilePicture"] = user.ProfilePicture;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();
            await LoadAsync(user);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();
            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }
            // UserName
            if (Input.UserName != user.UserName)
                await _userManager.SetUserNameAsync(user, Input.UserName);
            // PhoneNumber
            if (Input.PhoneNumber != user.PhoneNumber)
                await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
            user.FirstName = Input.FirstName;
            user.LastName = Input.LastName;
            // حفظ الصورة في DB
            if (Input.ProfilePicture != null)
            {
                using var ms = new MemoryStream();
                await Input.ProfilePicture.CopyToAsync(ms);
                user.ProfilePicture = ms.ToArray();
            }
            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}