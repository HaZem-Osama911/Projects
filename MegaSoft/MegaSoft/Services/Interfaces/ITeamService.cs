using MegaSoft.Models;
using MegaSoft.ViewModels.TeamViewModels;

namespace MegaSoft.Services.Interfaces
{
    public interface ITeamService
    {
        Task<List<Team>> GetTeamsForUserAsync(string userId, string? role);
        Task<Team?> GetByIDAsync(int id);
        Task<Team> CreateTeamAsync(Team team); 
        Task<Team?> GetTeamDetailsAsync(int id); 
        Task<EditTeamViewModel?> GetTeamForEditAsync(int id);
        Task<bool> UpdateTeamAsync(EditTeamViewModel model); 
        Task<bool> DeleteTeamAsync(int id);
    }
}