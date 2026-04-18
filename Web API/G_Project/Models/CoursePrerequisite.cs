using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G_Project.Models;

/// <summary>
/// Join table: Course → requires → PrerequisiteCourse
/// </summary>
public class CoursePrerequisite
{
    public int CourseId { get; set; }
    [ForeignKey(nameof(CourseId))]
    public Course? Course { get; set; }

    public int PrerequisiteId { get; set; }
    [ForeignKey(nameof(PrerequisiteId))]
    public Course? Prerequisite { get; set; }
}
