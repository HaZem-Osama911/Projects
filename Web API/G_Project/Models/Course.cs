using System.ComponentModel.DataAnnotations;

namespace G_Project.Models;

public class Course
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(20)]
    public string Code { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    public int CreditHours { get; set; }

    /// <summary>True if admin has opened this course for registration.</summary>
    public bool IsOpen { get; set; } = true;

    // Navigation
    public ICollection<CoursePrerequisite> Prerequisites { get; set; } = new List<CoursePrerequisite>();
    public ICollection<CoursePrerequisite> IsPrerequisiteFor { get; set; } = new List<CoursePrerequisite>();
}
