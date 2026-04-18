using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G_Project.Models;

public class StudentTranscript
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string StudentId { get; set; } = string.Empty;
    
    [ForeignKey(nameof(StudentId))]
    public ApplicationUser? Student { get; set; }

    [Required]
    public string FilePath { get; set; } = string.Empty;

    public string UploadedByAdminId { get; set; } = string.Empty;

    public DateTime UploadDate { get; set; } = DateTime.UtcNow;
}
