namespace G_Project.DTOs;

public class StudentProfileDto
{
    public string StudentId { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public TranscriptInfoDto? Transcript { get; set; }
    public string? LatestAiResult { get; set; }
    public List<CourseDto> SelectedCourses { get; set; } = new();
}

public class TranscriptInfoDto
{
    public int Id { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public DateTime UploadDate { get; set; }
}
