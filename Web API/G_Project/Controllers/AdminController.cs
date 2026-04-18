using G_Project.Common;
using G_Project.Data;
using G_Project.DTOs;
using G_Project.Models;
using G_Project.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace G_Project.Controllers;

[ApiController]
[Route("api/admin")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly IFileService _fileService;

    public AdminController(ApplicationDbContext db, IFileService fileService)
    {
        _db = db;
        _fileService = fileService;
    }

    // ─────────────────────────────────────────────────────────────────────────
    // 1. Upload student transcript
    // ─────────────────────────────────────────────────────────────────────────
    [HttpPost("upload-student-transcript/{studentId}")]
    public async Task<IActionResult> UploadStudentTranscript(string studentId, IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest(ApiResponse<string>.Fail("No file provided."));

        var student = await _db.Users.FindAsync(studentId);
        if (student == null)
            return NotFound(ApiResponse<string>.Fail($"Student with Id '{studentId}' not found."));

        var adminId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "";

        var uploadResult = await _fileService.UploadFileAsync(file);
        if (!uploadResult.Success)
            return BadRequest(uploadResult);

        // Replace any existing transcript for this student
        var existing = await _db.StudentTranscripts.FirstOrDefaultAsync(t => t.StudentId == studentId);
        if (existing != null)
            _db.StudentTranscripts.Remove(existing);

        _db.StudentTranscripts.Add(new StudentTranscript
        {
            StudentId = studentId,
            FilePath = uploadResult.Data!,
            UploadedByAdminId = adminId,
            UploadDate = DateTime.UtcNow
        });

        await _db.SaveChangesAsync();
        return Ok(ApiResponse<string>.Ok(uploadResult.Data!, "Transcript uploaded successfully."));
    }

    // ─────────────────────────────────────────────────────────────────────────
    // 2. Upload university regulation (replace old one)
    // ─────────────────────────────────────────────────────────────────────────
    [HttpPost("upload-regulation")]
    public async Task<IActionResult> UploadRegulation(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest(ApiResponse<string>.Fail("No file provided."));

        var uploadResult = await _fileService.UploadFileAsync(file);
        if (!uploadResult.Success)
            return BadRequest(uploadResult);

        // Mark all old regulations inactive
        await _db.SystemRegulations.ExecuteUpdateAsync(s =>
            s.SetProperty(r => r.IsActive, false));

        _db.SystemRegulations.Add(new SystemRegulation
        {
            FilePath = uploadResult.Data!,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        });

        await _db.SaveChangesAsync();
        return Ok(ApiResponse<string>.Ok(uploadResult.Data!, "Regulation uploaded and set as active."));
    }

    // ─────────────────────────────────────────────────────────────────────────
    // 3. Course CRUD
    // ─────────────────────────────────────────────────────────────────────────
    [HttpGet("courses")]
    public async Task<IActionResult> GetCourses()
    {
        var courses = await _db.Courses
            .Include(c => c.Prerequisites)
                .ThenInclude(p => p.Prerequisite)
            .ToListAsync();

        var result = courses.Select(c => MapCourseDto(c)).ToList();
        return Ok(ApiResponse<List<CourseDto>>.Ok(result));
    }

    [HttpPost("courses")]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Code) || string.IsNullOrWhiteSpace(dto.Name))
            return BadRequest(ApiResponse<string>.Fail("Course code and name are required."));

        if (await _db.Courses.AnyAsync(c => c.Code == dto.Code))
            return Conflict(ApiResponse<string>.Fail($"Course with code '{dto.Code}' already exists."));

        var course = new Course
        {
            Code = dto.Code.Trim(),
            Name = dto.Name.Trim(),
            CreditHours = dto.CreditHours,
            IsOpen = dto.IsOpen
        };

        _db.Courses.Add(course);
        await _db.SaveChangesAsync();
        return Ok(ApiResponse<CourseDto>.Ok(MapCourseDto(course), "Course created successfully."));
    }

    [HttpPut("courses/{id}")]
    public async Task<IActionResult> UpdateCourse(int id, [FromBody] CreateCourseDto dto)
    {
        var course = await _db.Courses.FindAsync(id);
        if (course == null)
            return NotFound(ApiResponse<string>.Fail("Course not found."));

        course.Code = dto.Code.Trim();
        course.Name = dto.Name.Trim();
        course.CreditHours = dto.CreditHours;
        course.IsOpen = dto.IsOpen;

        await _db.SaveChangesAsync();
        return Ok(ApiResponse<CourseDto>.Ok(MapCourseDto(course), "Course updated successfully."));
    }

    [HttpDelete("courses/{id}")]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        var course = await _db.Courses.FindAsync(id);
        if (course == null)
            return NotFound(ApiResponse<string>.Fail("Course not found."));

        _db.Courses.Remove(course);
        await _db.SaveChangesAsync();
        return Ok(ApiResponse<string>.Ok("Deleted", "Course deleted successfully."));
    }

    // ─────────────────────────────────────────────────────────────────────────
    // 4. Course Prerequisites
    // ─────────────────────────────────────────────────────────────────────────
    [HttpPost("courses/{id}/prerequisites")]
    public async Task<IActionResult> AddPrerequisite(int id, [FromBody] AddPrerequisiteDto dto)
    {
        if (id == dto.PrerequisiteId)
            return BadRequest(ApiResponse<string>.Fail("A course cannot be its own prerequisite."));

        if (!await _db.Courses.AnyAsync(c => c.Id == id))
            return NotFound(ApiResponse<string>.Fail("Course not found."));

        if (!await _db.Courses.AnyAsync(c => c.Id == dto.PrerequisiteId))
            return NotFound(ApiResponse<string>.Fail("Prerequisite course not found."));

        if (await _db.CoursePrerequisites.AnyAsync(cp => cp.CourseId == id && cp.PrerequisiteId == dto.PrerequisiteId))
            return Conflict(ApiResponse<string>.Fail("This prerequisite is already assigned."));

        _db.CoursePrerequisites.Add(new CoursePrerequisite
        {
            CourseId = id,
            PrerequisiteId = dto.PrerequisiteId
        });

        await _db.SaveChangesAsync();
        return Ok(ApiResponse<string>.Ok("Added", "Prerequisite added successfully."));
    }

    [HttpDelete("courses/{id}/prerequisites/{prereqId}")]
    public async Task<IActionResult> RemovePrerequisite(int id, int prereqId)
    {
        var entry = await _db.CoursePrerequisites
            .FirstOrDefaultAsync(cp => cp.CourseId == id && cp.PrerequisiteId == prereqId);

        if (entry == null)
            return NotFound(ApiResponse<string>.Fail("Prerequisite relationship not found."));

        _db.CoursePrerequisites.Remove(entry);
        await _db.SaveChangesAsync();
        return Ok(ApiResponse<string>.Ok("Removed", "Prerequisite removed successfully."));
    }

    // ─────────────────────────────────────────────────────────────────────────
    // 5. Mark a course as completed for a student
    // ─────────────────────────────────────────────────────────────────────────
    [HttpPost("student-completed-course")]
    public async Task<IActionResult> MarkCompletedCourse([FromBody] MarkCompletedCourseDto dto)
    {
        if (!await _db.Users.AnyAsync(u => u.Id == dto.StudentId))
            return NotFound(ApiResponse<string>.Fail("Student not found."));

        if (!await _db.Courses.AnyAsync(c => c.Id == dto.CourseId))
            return NotFound(ApiResponse<string>.Fail("Course not found."));

        if (await _db.StudentCompletedCourses.AnyAsync(s => s.StudentId == dto.StudentId && s.CourseId == dto.CourseId))
            return Conflict(ApiResponse<string>.Fail("This course is already marked as completed for this student."));

        _db.StudentCompletedCourses.Add(new StudentCompletedCourse
        {
            StudentId = dto.StudentId,
            CourseId = dto.CourseId,
            CompletedAt = DateTime.UtcNow
        });

        await _db.SaveChangesAsync();
        return Ok(ApiResponse<string>.Ok("Marked", "Course marked as completed for the student."));
    }

    // ─────────────────────────────────────────────────────────────────────────
    // Helper
    // ─────────────────────────────────────────────────────────────────────────
    private static CourseDto MapCourseDto(Course c) => new()
    {
        Id = c.Id,
        Code = c.Code,
        Name = c.Name,
        CreditHours = c.CreditHours,
        IsOpen = c.IsOpen,
        Prerequisites = c.Prerequisites
            .Where(p => p.Prerequisite != null)
            .Select(p => new CourseDto
            {
                Id = p.Prerequisite!.Id,
                Code = p.Prerequisite.Code,
                Name = p.Prerequisite.Name,
                CreditHours = p.Prerequisite.CreditHours,
                IsOpen = p.Prerequisite.IsOpen
            }).ToList()
    };
}
