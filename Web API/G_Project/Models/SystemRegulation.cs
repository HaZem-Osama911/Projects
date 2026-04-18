using System;
using System.ComponentModel.DataAnnotations;

namespace G_Project.Models;

public class SystemRegulation
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string FilePath { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
