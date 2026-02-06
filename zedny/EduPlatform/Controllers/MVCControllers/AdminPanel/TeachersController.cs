
namespace EduPlatform.Controllers.MVCControllers
{
    [Authorize(Roles = "Super_Admin")]
    public class TeachersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public TeachersController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Teacher_Dashboard()
        {
            var allTeachers = new List<ApplicationUser>();
            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, "TEACHER"))
                    allTeachers.Add(user);
            }

            // بناء ViewModel لكل مدرس مع الطلاب
            var teacherWithStudents = allTeachers.Select(t => new TeacherWithStudentsViewModel
            {
                Teacher = t,
                Students = _context.StudentTeachers
                            .Where(st => st.TeacherId == t.Id)
                            .Select(st => _userManager.Users.FirstOrDefault(u => u.Id == st.StudentId))
                            .Where(s => s != null)
                            .ToList()
            }).ToList();

            return View(teacherWithStudents);
        }

        public IActionResult ADD_Teacher()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ADD_Teacher(string FirstName, string LastName, string Email, string password)
        {
            if (string.IsNullOrWhiteSpace(FirstName) ||
                string.IsNullOrWhiteSpace(LastName) ||
                string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError(string.Empty, "All fields are required.");
                return View(new ApplicationUser { FirstName = FirstName, LastName = LastName, Email = Email });
            }

            var teacher = new ApplicationUser
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                UserName = Email,
                EmailConfirmed = true,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(teacher, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(teacher, "TEACHER");
                return RedirectToAction("Teacher_Dashboard");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(new ApplicationUser { FirstName = FirstName, LastName = LastName, Email = Email });
        }
        public async Task<IActionResult> Edit_Teacher(string id)
        {
            if (id == null) return NotFound();

            var teacher = await _userManager.FindByIdAsync(id);
            if (teacher == null) return NotFound();

            return View(teacher);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Teacher(string id, string FirstName, string LastName, string Email)
        {
            if (id == null) return NotFound();

            var teacher = await _userManager.FindByIdAsync(id);
            if (teacher == null) return NotFound();

            teacher.FirstName = FirstName;
            teacher.LastName = LastName;
            teacher.Email = Email;
            teacher.UserName = Email;

            var result = await _userManager.UpdateAsync(teacher);

            if (result.Succeeded)
            {
                return RedirectToAction("Teacher_Dashboard");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(teacher);
        }



        public async Task<IActionResult> Assign_Students(string teacherId)
        {
            if (teacherId == null) return NotFound();

            var teacher = await _userManager.FindByIdAsync(teacherId);
            if (teacher == null) return NotFound();

            var allStudents = await _userManager.Users.ToListAsync();

            var assignedStudentIds = _context.StudentTeachers
                .Where(st => st.TeacherId == teacherId)
                .Select(st => st.StudentId)
                .ToList();

            ViewBag.Teacher = teacher;
            ViewBag.AssignedStudentIds = assignedStudentIds;

            return View(allStudents);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Assign_Students(string teacherId, string[] selectedStudents)
        {
            if (teacherId == null) return NotFound();

            var teacher = await _userManager.FindByIdAsync(teacherId);
            if (teacher == null) return NotFound();

            var existing = _context.StudentTeachers
                .Where(st => st.TeacherId == teacherId);
            _context.StudentTeachers.RemoveRange(existing);

            if (selectedStudents != null)
            {
                foreach (var studentId in selectedStudents)
                {
                    _context.StudentTeachers.Add(new StudentTeacher
                    {
                        TeacherId = teacherId,
                        StudentId = studentId
                    });
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Teacher_Dashboard");
        }

        public async Task<IActionResult> Delete_Teacher(string id)
        {
            if (id == null) return NotFound();

            var teacher = await _userManager.FindByIdAsync(id);
            if (teacher == null) return NotFound();

            return View(teacher);
        }

        [HttpPost, ActionName("Delete_Teacher")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var teacher = await _userManager.FindByIdAsync(id);
            if (teacher == null) return NotFound();

            var result = await _userManager.DeleteAsync(teacher);
            if (result.Succeeded)
                return RedirectToAction("Teacher_Dashboard");

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(teacher);
        }

        public async Task<IActionResult> ViewStudents(string teacherId)
        {
            if (string.IsNullOrEmpty(teacherId)) return NotFound();

            var studentIds = await _context.StudentTeachers
                .Where(st => st.TeacherId == teacherId)
                .Select(st => st.StudentId)
                .ToListAsync();

            var students = new List<ApplicationUser>();

            foreach (var userId in studentIds)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null && await _userManager.IsInRoleAsync(user, "STUDENT"))
                {
                    students.Add(user);
                }
            }

            var teacher = await _userManager.FindByIdAsync(teacherId);
            ViewBag.TeacherName = teacher.FirstName + " " + teacher.LastName;

            return View(students);
        }

    }
}
