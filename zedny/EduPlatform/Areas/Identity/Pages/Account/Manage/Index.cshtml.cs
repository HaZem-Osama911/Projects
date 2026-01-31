using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EduPlatform.Models;
using System.ComponentModel.DataAnnotations;

namespace EduPlatform.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ProfileImageUrl { get; set; }

        public class InputModel
        {
            [Display(Name = "Username")]
            public string? UserName { get; set; }

            [Required(ErrorMessage = "الاسم الأول مطلوب")]
            [Display(Name = "First Name")]
            public string FirstName { get; set; } = string.Empty;

            [Required(ErrorMessage = "الاسم الأخير مطلوب")]
            [Display(Name = "Last Name")]
            public string LastName { get; set; } = string.Empty;

            [Phone]
            [Display(Name = "Phone Number")]
            public string? PhoneNumber { get; set; }

            [Display(Name = "Profile Picture")]
            public IFormFile? ProfilePicture { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            Input = new InputModel
            {
                UserName = await _userManager.GetUserNameAsync(user),
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = await _userManager.GetPhoneNumberAsync(user)
            };

            ProfileImageUrl = string.IsNullOrEmpty(user.ProfileImage) || user.ProfileImage == "default.jpeg"
                ? "/Asset/images/default.jpeg"
                : $"/uploads/profiles/{user.ProfileImage}";
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound("User not found.");

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound("User not found.");

            // 🔥 أهم سطر: إزالة الحقول التي لا تأتي من الفورم لمنع فشل الـ Validation
            ModelState.Remove("Input.UserName");

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            // تحديث البيانات النصية
            user.FirstName = Input.FirstName;
            user.LastName = Input.LastName;

            var currentPhone = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != currentPhone)
            {
                await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
            }

            // معالجة الصورة
            if (Input.ProfilePicture != null && Input.ProfilePicture.Length > 0)
            {
                try
                {
                    var extension = Path.GetExtension(Input.ProfilePicture.FileName).ToLower();
                    var fileName = $"{Guid.NewGuid()}{extension}";
                    var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "profiles");

                    if (!Directory.Exists(uploadPath))
                        Directory.CreateDirectory(uploadPath);

                    // حذف القديمة بشرط ألا تكون الافتراضية
                    if (!string.IsNullOrEmpty(user.ProfileImage) && user.ProfileImage != "default.jpeg")
                    {
                        var oldFile = Path.Combine(uploadPath, user.ProfileImage);
                        if (System.IO.File.Exists(oldFile)) System.IO.File.Delete(oldFile);
                    }

                    var filePath = Path.Combine(uploadPath, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Input.ProfilePicture.CopyToAsync(stream);
                    }

                    user.ProfileImage = fileName;
                }
                catch (Exception ex)
                {
                    StatusMessage = "Error uploading image: " + ex.Message;
                    await LoadAsync(user);
                    return Page();
                }
            }

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                StatusMessage = "Your profile has been updated";
                return RedirectToPage(); // إعادة التوجيه لتحديث البيانات في الكاش
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            await LoadAsync(user);
            return Page();
        }
    }
}