using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G_Project.Models;

/// <summary>
/// Records which courses a student has already passed (set by Admin after parsing transcript).
/// </summary>
public class StudentCompletedCourse
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string StudentId { get; set; } = string.Empty;

    [ForeignKey(nameof(StudentId))]
    public ApplicationUser? Student { get; set; }

    public int CourseId { get; set; }

    [ForeignKey(nameof(CourseId))]
    public Course? Course { get; set; }

    public DateTime CompletedAt { get; set; } = DateTime.UtcNow;
}
