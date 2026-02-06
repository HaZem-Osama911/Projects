using EduPlatform.Data;
using EduPlatform.Models;
using EduPlatform.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduPlatform.Controllers.MVCControllers.TeacherPanel
{
    [Authorize(Roles = "Teacher")]
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CoursesController(ApplicationDbContext context,
                                 UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Course_Dashboard()
        {
            var teacher = await _userManager.GetUserAsync(User);
            if (teacher == null) return Unauthorized();

            var courses = await _context.Courses
                .Where(c => c.TeacherId == teacher.Id)
                .Include(c => c.Enrollments)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            return View(courses);
        }

        public IActionResult ADD_NewCourse()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ADD_NewCourse(AddCourseViewModel model)
        {
            var teacher = await _userManager.GetUserAsync(User);
            if (teacher == null) return Unauthorized();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var course = new Course
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                TeacherId = teacher.Id,
                CreatedAt = DateTime.UtcNow
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Course added successfully!";
            return RedirectToAction(nameof(Course_Dashboard));
        }

        public async Task<IActionResult> Edit_Course(int id)
        {
            var teacher = await _userManager.GetUserAsync(User);
            if (teacher == null) return Unauthorized();

            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == id && c.TeacherId == teacher.Id);

            if (course == null) return NotFound();

            var model = new EditCourseViewModel
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                Price = course.Price
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Course(int id, EditCourseViewModel model)
        {
            var teacher = await _userManager.GetUserAsync(User);
            if (teacher == null) return Unauthorized();

            if (id != model.Id)
            {
                return BadRequest();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == id && c.TeacherId == teacher.Id);

            if (course == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            course.Name = model.Name;
            course.Description = model.Description;
            course.Price = model.Price;

            _context.Courses.Update(course);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Course updated successfully!";
            return RedirectToAction(nameof(Course_Dashboard));
        }


        public async Task<IActionResult> Delete_Course(int id)
        {
            var teacher = await _userManager.GetUserAsync(User);
            if (teacher == null) return Unauthorized();

            var course = await _context.Courses
                .Include(c => c.Enrollments)
                .FirstOrDefaultAsync(c => c.Id == id && c.TeacherId == teacher.Id);

            if (course == null) return NotFound();

            return View(course);
        }

        [HttpPost, ActionName("Delete_Course")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacher = await _userManager.GetUserAsync(User);
            if (teacher == null) return Unauthorized();

            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == id && c.TeacherId == teacher.Id);

            if (course == null) return NotFound();

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Course deleted successfully!";
            return RedirectToAction(nameof(Course_Dashboard));
        }

        public async Task<IActionResult> Course_Students(int id)
        {
            var teacher = await _userManager.GetUserAsync(User);
            if (teacher == null) return Unauthorized();

            var course = await _context.Courses
                .Include(c => c.Enrollments)
                    .ThenInclude(e => e.Student)
                .FirstOrDefaultAsync(c => c.Id == id && c.TeacherId == teacher.Id);

            if (course == null) return NotFound();

            ViewBag.CourseName = course.Name;
            ViewBag.CourseId = course.Id;
            ViewBag.TotalStudents = course.Enrollments?.Count(e => e.IsActive) ?? 0;

            var enrollments = course.Enrollments?
                .Where(e => e.IsActive && e.Student != null)
                .OrderByDescending(e => e.EnrolledAt)
                .ToList() ?? new List<Enrollment>();

            return View(enrollments);
        }
    }
}