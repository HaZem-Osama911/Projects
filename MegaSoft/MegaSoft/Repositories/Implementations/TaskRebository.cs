using MegaSoft.Data;
using MegaSoft.Models;
using MegaSoft.Models.Enums;
using MegaSoft.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MegaSoft.Repositories.Implementations
{
    public class TaskRebository : ITaskRebository
    {
        private readonly ApplicationDbContext _context;

        public TaskRebository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskItems>> GetAllAsync()
        {
            return await _context.TaskItems
                .Where(t => !t.IsDeleted)
                .Include(t => t.Team)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<TaskItems>> GetByTeamIdAsync(int teamId)
        {
            return await _context.TaskItems
                .Where(t => t.TeamId == teamId && !t.IsDeleted)
                .Include(t => t.Team)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<TaskItems?> GetByIdAsync(int id)
        {
            return await _context.TaskItems
                            .Where(t => t.Id == id && !t.IsDeleted)
                            .Include(t => t.Team)
                            .FirstOrDefaultAsync();
        }

        public async Task<List<TaskItems>> GetAllTasksAsyncEm()
        {
            return await _context.TaskItems
                .Include(t => t.Team)
                .Include(t => t.AssignedTo)
                .ToListAsync();
        }

        public async Task<List<TaskItems>> GetByEmployeeIdAsync(string employeeId)
        {
            return await _context.TaskItems
                .Where(t => t.AssignedToId == employeeId && !t.IsDeleted)
                .Include(t => t.Team)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<TaskItems> CreateAsync(TaskItems task)
        {
            try
            {
                Console.WriteLine($"Attempting to create task with TeamId: {task.TeamId}");

                if (task.TeamId == 0)
                {
                    throw new Exception("TeamId cannot be 0");
                }

                var teamExists = await _context.Teams
                    .AnyAsync(t => t.Id == task.TeamId && !t.IsDeleted);

                if (!teamExists)
                {
                    throw new Exception($"Team with ID {task.TeamId} does not exist or is deleted");
                }

                task.CreatedAt = DateTime.UtcNow;
                task.IsDeleted = false;
                task.Priority ??= TaskPriority.Medium;
                task.Status ??= Models.Enums.TaskStatus.ToDo;

                task.Team = null;
                task.AssignedTo = null;

                _context.TaskItems.Add(task);
                await _context.SaveChangesAsync();

                Console.WriteLine($"Task created successfully with ID: {task.Id}");
                return task;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"DbUpdateException: {ex.InnerException?.Message}");
                throw new Exception($"Database error: {ex.InnerException?.Message}", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(TaskItems task)
        {
            var existingTask = await _context.TaskItems
                .FirstOrDefaultAsync(t => t.Id == task.Id && !t.IsDeleted);

            if (existingTask == null)
                return false;

            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.Status = task.Status;
            existingTask.Priority = task.Priority;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var task = await _context.TaskItems.FindAsync(id);
            if (task == null || task.IsDeleted)
                return false;

            task.IsDeleted = true;
            _context.TaskItems.Update(task);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}