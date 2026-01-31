using System.ComponentModel.DataAnnotations.Schema;

namespace EduPlatform.Models
{
    public class StudentTeacher
    {
        public string StudentId { get; set; } = string.Empty;
        public ApplicationUser Student { get; set; } = null!;

        public string TeacherId { get; set; } = string.Empty;
        public ApplicationUser Teacher { get; set; } = null!;
    }
}
