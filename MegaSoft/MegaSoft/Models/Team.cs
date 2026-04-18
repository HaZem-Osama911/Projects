using System.ComponentModel.DataAnnotations;

namespace MegaSoft.Models
{
    public class Team
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Team name is required")]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string CreatedById { get; set; } = default!;

        public ApplicationUser? CreatedBy { get; set; }

        public ICollection<TeamMember> Members { get; set; } = new List<TeamMember>();
        public ICollection<TaskItems> Tasks { get; set; } = new List<TaskItems>();
    }
}