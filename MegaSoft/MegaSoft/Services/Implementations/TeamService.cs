using MegaSoft.Models;
using MegaSoft.Repositories.Interfaces;
using MegaSoft.Services.Interfaces;
using MegaSoft.ViewModels.TeamViewModels;

namespace MegaSoft.Services.Implementations
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;

        public TeamService(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<List<Team>> GetTeamsForUserAsync(string userId, string? role)
        {
            var allTeams = await _teamRepository.GetAllAsync();

            if (role == "ADMIN" || role == "MANAGER")
                return allTeams;

            return allTeams.Where(t => t.Members.Any(m => m.UserId == userId)).ToList();
        }

        public async Task<Team?> GetTeamDetailsAsync(int id)
        {
            return await _teamRepository.GetByIdAsync(id);
        }

        public async Task<Team> CreateTeamAsync(Team team)
        {
            return await _teamRepository.CreateAsync(team);
        }

        public async Task<EditTeamViewModel?> GetTeamForEditAsync(int id)
        {
            var team = await _teamRepository.GetByIdAsync(id);
            if (team == null) return null;

            var allUsers = await _teamRepository.GetAllUsersAsync();

            return new EditTeamViewModel
            {
                Id = team.Id,
                Name = team.Name,
                Members = allUsers.Select(u => new TeamMemberCheckboxViewModel
                {
                    UserId = u.Id,
                    FullName = u.UserName ?? "Unknown",
                    IsSelected = team.Members.Any(m => m.UserId == u.Id)
                }).ToList()
            };
        }

        public async Task<bool> UpdateTeamAsync(EditTeamViewModel model)
        {
            var team = await _teamRepository.GetByIdAsync(model.Id);
            if (team == null) return false;

            team.Name = model.Name;

            var updated = await _teamRepository.UpdateAsync(team);
            if (!updated) return false;

            var selectedUserIds = (model.Members ?? new List<TeamMemberCheckboxViewModel>())
                .Where(m => m.IsSelected)
                .Select(m => m.UserId);

            await _teamRepository.UpdateTeamMembersAsync(team.Id, selectedUserIds);
            return true;
        }

        public async Task<bool> DeleteTeamAsync(int id)
        {
            return await _teamRepository.DeleteAsync(id);
        }

        public async Task<Team?> GetByIDAsync(int id)
        {
            return await _teamRepository.GetByIdAsync(id);
        }
    }
}