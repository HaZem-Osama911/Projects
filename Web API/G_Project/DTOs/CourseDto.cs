namespace G_Project.DTOs;

public class CourseDto
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int CreditHours { get; set; }
    public bool IsOpen { get; set; }
    public List<CourseDto> Prerequisites { get; set; } = new();
}

public class CreateCourseDto
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int CreditHours { get; set; }
    public bool IsOpen { get; set; } = true;
}

public class AddPrerequisiteDto
{
    public int PrerequisiteId { get; set; }
}

public class MarkCompletedCourseDto
{
    public string StudentId { get; set; } = string.Empty;
    public int CourseId { get; set; }
}
