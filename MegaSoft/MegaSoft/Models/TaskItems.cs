using MegaSoft.Models.Enums;
using System.ComponentModel.DataAnnotations;
using TaskStatus = MegaSoft.Models.Enums.TaskStatus;

namespace MegaSoft.Models
{
    public class TaskItems
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? Deadline { get; set; }

        public TaskStatus? Status { get; set; } = TaskStatus.ToDo;


        public TaskPriority? Priority { get; set; } = TaskPriority.Medium;

        public bool IsDeleted { get; set; } = false;

        public string? AssignedToId { get; set; }
        public ApplicationUser? AssignedTo { get; set; }

        [Required]
        public int TeamId { get; set; } // ✅ ارجعها required
        public Team? Team { get; set; }
    }
}
