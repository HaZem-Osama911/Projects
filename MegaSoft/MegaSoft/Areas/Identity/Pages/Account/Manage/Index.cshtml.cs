#nullable disable
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using MegaSoft.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MegaSoft.Areas.Identity.Pages.Account.Manage
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

        public string Username { get; set; }
        public string ProfileImageUrl { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "First name")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last name")]
            public string LastName { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Profile Image")]
            public IFormFile ProfileImage { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;
            ProfileImageUrl = "/uploads/profiles/" + (user.ProfileImage ?? "default.jpeg");

            Input = new InputModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound("User not found.");

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound("User not found.");

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            // تحديث البيانات الأساسية
            user.FirstName = Input.FirstName;
            user.LastName = Input.LastName;

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var result = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!result.Succeeded)
                {
                    StatusMessage = "Error updating phone number.";
                    return RedirectToPage();
                }
            }

            // ✅ رفع الصورة محسّن
            if (Input.ProfileImage != null && Input.ProfileImage.Length > 0)
            {
                try
                {
                    // التحقق من نوع الملف
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var extension = Path.GetExtension(Input.ProfileImage.FileName).ToLowerInvariant();

                    if (string.IsNullOrEmpty(extension) || !allowedExtensions.Contains(extension))
                    {
                        StatusMessage = "Error: Only image files (.jpg, .jpeg, .png, .gif) are allowed.";
                        await LoadAsync(user);
                        return Page();
                    }

                    // التحقق من حجم الملف (5 MB)
                    if (Input.ProfileImage.Length > 5 * 1024 * 1024)
                    {
                        StatusMessage = "Error: Image size must be less than 5 MB.";
                        await LoadAsync(user);
                        return Page();
                    }

                    var fileName = $"{Guid.NewGuid()}{extension}";
                    var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "profiles");

                    // إنشاء المجلد إذا لم يكن موجوداً
                    if (!Directory.Exists(uploadPath))
                        Directory.CreateDirectory(uploadPath);

                    // ✅ حذف الصورة القديمة
                    if (!string.IsNullOrEmpty(user.ProfileImage) &&
                        user.ProfileImage != "default.jpeg" &&
                        user.ProfileImage != "default.png")
                    {
                        var oldFile = Path.Combine(uploadPath, user.ProfileImage);
                        if (System.IO.File.Exists(oldFile))
                        {
                            try
                            {
                                System.IO.File.Delete(oldFile);
                            }
                            catch
                            {
                                // تجاهل خطأ الحذف
                            }
                        }
                    }

                    // حفظ الصورة الجديدة
                    var filePath = Path.Combine(uploadPath, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Input.ProfileImage.CopyToAsync(stream);
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

            // تحديث المستخدم
            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                StatusMessage = "Error updating profile.";
                await LoadAsync(user);
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}