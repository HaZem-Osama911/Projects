using MegaSoft.Models;
using MegaSoft.Repositories.Interfaces;
using MegaSoft.Services.Interfaces;

namespace MegaSoft.Services.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRebository _taskRepository;

        public TaskService(ITaskRebository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<List<TaskItems>> GetAllTasksAsync()
        {
            return await _taskRepository.GetAllAsync();
        }

        public async Task<List<TaskItems>> GetTasksByTeamIdAsync(int teamId)
        {
            return await _taskRepository.GetByTeamIdAsync(teamId);
        }

        public async Task<List<TaskItems>> GetTasksByEmployeeIdAsync(string employeeId)
        {
            return await _taskRepository.GetByEmployeeIdAsync(employeeId);
        }

        public async Task<TaskItems?> GetTaskByIdAsync(int id)
        {
            return await _taskRepository.GetByIdAsync(id);
        }

        public async Task<TaskItems> CreateTaskAsync(TaskItems task)
        {
            return await _taskRepository.CreateAsync(task);
        }

        public async Task<bool> UpdateTaskAsync(TaskItems task)
        {
            return await _taskRepository.UpdateAsync(task);
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            return await _taskRepository.DeleteAsync(id);
        }
    }
}