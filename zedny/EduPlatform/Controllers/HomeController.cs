using EduPlatform.Models;
using EduPlatform.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EduPlatform.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var teachersInRole = await _userManager.GetUsersInRoleAsync("TEACHER");

            var teachers = teachersInRole.Select(u => new TeacherCardVM
            {
                Id = u.Id,
                FullName = $"{u.FirstName} {u.LastName}",
                ImageUrl = string.IsNullOrEmpty(u.ProfileImage) || u.ProfileImage == "default.jpeg"
                           ? "/Asset/images/default.jpeg"
                           : $"/uploads/profiles/{u.ProfileImage}",
                CoursesCount = _context.Courses.Count(c => c.TeacherId == u.Id)
            }).ToList();

            return View(teachers);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}