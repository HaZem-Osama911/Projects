using EduPlatform.Data;
using EduPlatform.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduPlatform.Controllers.MVCControllers
{
    [Authorize(Roles = "STUDENT,Super_Admin")]
    public class StudentsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public StudentsController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: ChooseTeachers
        public async Task<IActionResult> ChooseTeachers()
        {
            var student = await _userManager.GetUserAsync(User);
            var allTeachers = new List<ApplicationUser>();

            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, "TEACHER"))
                    allTeachers.Add(user);
            }

            // Teachers already assigned
            var assignedTeacherIds = _context.StudentTeachers
                .Where(st => st.StudentId == student.Id)
                .Select(st => st.TeacherId)
                .ToList();

            ViewBag.AssignedTeacherIds = assignedTeacherIds;
            return View(allTeachers);
        }

        // POST: ChooseTeachers
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChooseTeachers(string[] selectedTeachers)
        {
            var student = await _userManager.GetUserAsync(User);

            // Remove existing assignments
            var existing = _context.StudentTeachers.Where(st => st.StudentId == student.Id);
            _context.StudentTeachers.RemoveRange(existing);

            // Add new selections
            if (selectedTeachers != null)
            {
                foreach (var teacherId in selectedTeachers)
                {
                    _context.StudentTeachers.Add(new StudentTeacher
                    {
                        StudentId = student.Id,
                        TeacherId = teacherId
                    });
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MyTeachers));
        }

        // GET: MyTeachers
        public async Task<IActionResult> MyTeachers()
        {
            var student = await _userManager.GetUserAsync(User);

            var teacherIds = _context.StudentTeachers
                .Where(st => st.StudentId == student.Id)
                .Select(st => st.TeacherId)
                .ToList();

            var teachers = await _userManager.Users
                .Where(u => teacherIds.Contains(u.Id))
                .ToListAsync();

            return View(teachers);
        }
    }
}
