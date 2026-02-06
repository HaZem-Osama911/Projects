using EduPlatform.Data;
using EduPlatform.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduPlatform.Controllers.MVCControllers
{
    [Authorize(Roles = "Teacher,Super_Admin")]
    public class TeacherPanelController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public TeacherPanelController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> MyStudents()
        {
            var teacher = await _userManager.GetUserAsync(User);
            if (teacher == null) return Unauthorized();

            var studentIds = await _context.StudentTeachers
                .Where(st => st.TeacherId == teacher.Id)
                .Select(st => st.StudentId)
                .ToListAsync();

            var students = await _userManager.Users
                .Where(u => studentIds.Contains(u.Id))
                .ToListAsync();

            return View(students);
        }

        public async Task<IActionResult> EditStudent(string studentId)
        {
            var teacher = await _userManager.GetUserAsync(User);

            var isAssigned = await _context.StudentTeachers
                .AnyAsync(st => st.TeacherId == teacher.Id && st.StudentId == studentId);

            if (!isAssigned) return Forbid();

            var student = await _userManager.FindByIdAsync(studentId);
            if (student == null) return NotFound();

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStudent(string studentId, ApplicationUser updatedStudent)
        {
            var teacher = await _userManager.GetUserAsync(User);

            var isAssigned = await _context.StudentTeachers
                .AnyAsync(st => st.TeacherId == teacher.Id && st.StudentId == studentId);

            if (!isAssigned) return Forbid();

            var student = await _userManager.FindByIdAsync(studentId);
            if (student == null) return NotFound();

            student.FirstName = updatedStudent.FirstName;
            student.LastName = updatedStudent.LastName;
            student.Email = updatedStudent.Email;
            student.UserName = updatedStudent.Email;

            var result = await _userManager.UpdateAsync(student);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(MyStudents));
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(updatedStudent);
        }

        public async Task<IActionResult> RemoveStudent(string studentId)
        {
            var teacher = await _userManager.GetUserAsync(User);

            var assignment = await _context.StudentTeachers
                .FirstOrDefaultAsync(st => st.TeacherId == teacher.Id && st.StudentId == studentId);

            if (assignment == null) return Forbid();

            _context.StudentTeachers.Remove(assignment);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(MyStudents));
        }
    }
}
