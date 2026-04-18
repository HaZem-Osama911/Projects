using MegaSoft.Models;

namespace MegaSoft.Services.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskItems>> GetAllTasksAsync();
        Task<List<TaskItems>> GetTasksByTeamIdAsync(int teamId);
        Task<List<TaskItems>> GetTasksByEmployeeIdAsync(string employeeId);
        Task<TaskItems?> GetTaskByIdAsync(int id);
        Task<TaskItems> CreateTaskAsync(TaskItems task);
        Task<bool> UpdateTaskAsync(TaskItems task);
        Task<bool> DeleteTaskAsync(int id);
    }
}