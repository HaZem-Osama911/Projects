using MegaSoft.Models;

namespace MegaSoft.Repositories.Interfaces
{
    public interface ITaskRebository
    {
        Task<List<TaskItems>> GetAllAsync();
        Task<List<TaskItems>> GetByTeamIdAsync(int teamId);
        Task<TaskItems?> GetByIdAsync(int id);
        Task<List<TaskItems>> GetByEmployeeIdAsync(string employeeId);
        Task<TaskItems> CreateAsync(TaskItems task);
        Task<bool> UpdateAsync(TaskItems task);
        Task<bool> DeleteAsync(int id);
    }
}