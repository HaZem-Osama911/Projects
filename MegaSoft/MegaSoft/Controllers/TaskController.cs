

using SQLitePCL;

namespace MegaSoft.Controllers
{
    [Authorize(Roles = "ADMIN,TECHNICAL_HEAD,MANAGER,EMPLOYEE")]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly ITeamService _teamService;

        public TaskController(ITaskService taskService, ITeamService teamService)
        {
            _taskService = taskService;
            _teamService = teamService;
        }

        // ================= Task Dashboard ================
        public async Task<IActionResult> Task_Dashboard()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            List<TaskItems> tasks = new();

            if (role == "ADMIN" || role == "MANAGER")
            {
                tasks = await _taskService.GetAllTasksAsync();
            }
            else if (role == "TECHNICAL_HEAD")
            {
                if (string.IsNullOrWhiteSpace(userId))
                    return Forbid();

                var teams = await _teamService.GetTeamsForUserAsync(userId, role);
                var teamIds = teams.Select(t => t.Id).Distinct().ToList();
                var perTeamTasks = await Task.WhenAll(teamIds.Select(id => _taskService.GetTasksByTeamIdAsync(id)));
                tasks = perTeamTasks.SelectMany(x => x).GroupBy(t => t.Id).Select(g => g.First()).ToList();
            }
            else if (role == "EMPLOYEE")
            {
                if (string.IsNullOrWhiteSpace(userId))
                    return Forbid();
                tasks = await _taskService.GetTasksByEmployeeIdAsync(userId);
            }

            return View(tasks);
        }

        // ================= Create Task (GET) ===============
        public async Task<IActionResult> Create_Task(int? teamId)
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value ?? "EMPLOYEE";
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

            TaskItems task = new TaskItems();

            List<Team> availableTeams = new();

            if (role == "ADMIN" || role == "MANAGER")
            {
                availableTeams = await _teamService.GetTeamsForUserAsync(userId, role);
            }
            else if (role == "TECHNICAL_HEAD" || role == "EMPLOYEE")
            {
                availableTeams = await _teamService.GetTeamsForUserAsync(userId, role);
            }

            if (teamId.HasValue && teamId.Value > 0)
            {
                if (availableTeams.Any(t => t.Id == teamId.Value))
                    task.TeamId = teamId.Value;
            }

            if (!availableTeams.Any())
            {
                TempData["ErrorMessage"] = "No teams available. Please contact your administrator.";
                return RedirectToAction("Task_Dashboard");
            }

            ViewBag.Teams = new SelectList(availableTeams, "Id", "Name", task.TeamId);

            return View(task);
        }

        // ================= Create Task (POST) ===============
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create_Task(TaskItems task)
        {
            Console.WriteLine($"TeamId received: {task.TeamId}");
            Console.WriteLine($"Title: {task.Title}");

            if (task.TeamId == 0)
            {
                ModelState.AddModelError("TeamId", "Please select a team");
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                foreach (var error in errors)
                {
                    Console.WriteLine($"ERROR: {error}");
                }

                var role = User.FindFirst(ClaimTypes.Role)?.Value ?? "EMPLOYEE";
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

                List<Team> availableTeams = await _teamService.GetTeamsForUserAsync(userId, role);
                ViewBag.Teams = new SelectList(availableTeams, "Id", "Name", task.TeamId);

                return View(task);
            }

            task.AssignedToId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            try
            {
                await _taskService.CreateTaskAsync(task);
                TempData["SuccessMessage"] = "Task created successfully!";
                return RedirectToAction("Task_Dashboard");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                ModelState.AddModelError("", $"Error creating task: {ex.Message}");

                var role = User.FindFirst(ClaimTypes.Role)?.Value ?? "EMPLOYEE";
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
                List<Team> availableTeams = await _teamService.GetTeamsForUserAsync(userId, role);
                ViewBag.Teams = new SelectList(availableTeams, "Id", "Name", task.TeamId);

                return View(task);
            }
        }

        public async Task<IActionResult> Edit_Task(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            var role = User.FindFirst(ClaimTypes.Role)?.Value ?? "EMPLOYEE";
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
            List<Team> availableTeams = await _teamService.GetTeamsForUserAsync(userId, role);
            ViewBag.Teams = new SelectList(availableTeams, "Id", "Name", task.TeamId);
            return View(task);

        }

        


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Task(TaskItems task)
        {
            if (!ModelState.IsValid)
            {
                var role = User.FindFirst(ClaimTypes.Role)?.Value ?? "EMPLOYEE";
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

                var teams = await _teamService.GetTeamsForUserAsync(userId, role);
                ViewBag.Teams = new SelectList(teams, "Id", "Name", task.TeamId);

                return View(task);
            }

            await _taskService.UpdateTaskAsync(task);

            TempData["SuccessMessage"] = "Task updated successfully!";
            return RedirectToAction("Task_Dashboard");
        }

        public async Task<IActionResult> Details_Task(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
                return NotFound();

            return View(task);
        }

        public async Task<IActionResult> Delete_Task(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        [HttpPost, ActionName("Delete_Task")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _taskService.DeleteTaskAsync(id);

            if (!result)
                return NotFound();

            TempData["SuccessMessage"] = "Task deleted successfully!";
            return RedirectToAction("Task_Dashboard");
        }
    }
}