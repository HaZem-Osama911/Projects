using MegaSoft.Models;
using MegaSoft.Services.Interfaces;
using MegaSoft.ViewModels.TeamViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.ComponentModel;

namespace MegaSoft.Controllers
{
    [Authorize(Roles = "ADMIN,TECHNICAL_HEAD,MANAGER")]
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;
        private readonly ITeamRepository _teamRebo;

        public TeamController(ITeamService teamService, ITeamRepository teamRebo)
        {
            _teamService = teamService;
            _teamRebo=teamRebo;
        }

        public async Task<IActionResult> Team_Dashboard()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var role = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Role)?.Value;

            if (string.IsNullOrWhiteSpace(userId))
                return Forbid();

            var teams = await _teamService.GetTeamsForUserAsync(userId, role);

            // ✅ حول الـ List<Team> لـ List<TeamIndexViewModel>
            var viewModel = teams.Select(t => new TeamIndexViewModel
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description,
                MembersCount = t.Members?.Count ?? 0,
                TasksCount = t.Tasks?.Count(task => !task.IsDeleted) ?? 0
            }).ToList();

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTeamViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var team = new Team
            {
                Name = model.Name,
                Description = model.Description,
                CreatedById = userId!
            };

            await _teamService.CreateTeamAsync(team);
            return RedirectToAction("Team_Dashboard");
        }

        public async Task<IActionResult> Team_Details(int id)
        {
            var team = await _teamService.GetTeamDetailsAsync(id);
            if (team == null) return NotFound();

            // ✅ حول الـ Team لـ TeamDetailsViewModel
            var viewModel = new TeamDetailsViewModel
            {
                Id = team.Id,
                Name = team.Name,
                Description = team.Description,
                CreatedAt = team.CreatedAt,
                Members = team.Members?.Select(m => new TeamMemberDetailViewModel
                {
                    UserId = m.UserId,
                    FullName = m.User?.UserName ?? "Unknown",
                    Email = m.User?.Email
                }).ToList() ?? new List<TeamMemberDetailViewModel>(),
                TasksCount = team.Tasks?.Count(t => !t.IsDeleted) ?? 0
            };

            return View(viewModel);
        }

        // GET: Team/Edit/5
        public async Task<IActionResult> Edit_Team(int id)
        {
            var model = await _teamService.GetTeamForEditAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        // POST: Team/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Team(EditTeamViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var reloadedModel = await _teamService.GetTeamForEditAsync(model.Id);
                if (reloadedModel != null)
                {
                    model.Members = reloadedModel.Members;
                }
                return View(model);
            }

            try
            {
                await _teamService.UpdateTeamAsync(model);
                TempData["SuccessMessage"] = "Team updated successfully!";
                return RedirectToAction("Team_Dashboard");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                var reloadedModel = await _teamService.GetTeamForEditAsync(model.Id);
                if (reloadedModel != null)
                {
                    model.Members = reloadedModel.Members;
                }
                return View(model);
            }
        }

        public async Task<IActionResult> Delete_Team(int id)
        {
            var team = await _teamService.GetByIDAsync(id);
            if (team == null) return NotFound();
            return View(team);
        }

        [HttpPost, ActionName("Delete_Team")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var result = await _teamService.DeleteTeamAsync(id);
                if (!result) return NotFound();
                TempData["SuccessMessage"] = "Team deleted successfully!";
                return RedirectToAction("Team_Dashboard");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                var team = await _teamService.GetByIDAsync(id);
                return View("Delete_Team", team);
            }
        }

        public async Task<IActionResult> TotalNumberofmember()
        {
            var total = _teamRebo.GetAllAsync().Result.Count;
            return View(total);
        }
    }
}