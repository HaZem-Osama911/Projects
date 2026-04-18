using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MegaSoft.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(50)]
        public string? FirstName { get; set; }

        [MaxLength(50)]
        public string? LastName { get; set; }

        [MaxLength(255)]
        public string ProfileImage { get; set; } = "default.jpeg";

        public bool IsActive { get; set; } = true;

        // Navigation
        public ICollection<TeamMember> TeamMemberships { get; set; } = new List<TeamMember>();
        public ICollection<TaskItems> AssignedTasks { get; set; } = new List<TaskItems>();
    }
}