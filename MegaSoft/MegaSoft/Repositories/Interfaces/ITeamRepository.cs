using MegaSoft.Models;

namespace MegaSoft.Repositories.Interfaces
{
    public interface ITeamRepository
    {
        Task<List<Team>> GetAllAsync();
        Task<Team?> GetByIdAsync(int id);
        Task<Team> CreateAsync(Team team);
        Task<bool> EditTeamNameAsync(int id, string teamName);
        Task<bool> UpdateAsync(Team team); 
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateTeamMembersAsync(int teamId, IEnumerable<string> selectedUserIds);
        Task<List<ApplicationUser>> GetAllUsersAsync();
        Task SaveChangesAsync();
    }
}