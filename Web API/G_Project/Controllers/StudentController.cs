using System.Security.Claims;
using G_Project.Common;
using G_Project.Data;
using G_Project.DTOs;
using G_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace G_Project.Controllers;

[ApiController]
[Route("api/student")]
[Authorize]
public class StudentController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public StudentController(ApplicationDbContext db)
    {
        _db = db;
    }

    private string GetStudentId() =>
        User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

    // ─────────────────────────────────────────────────────────────────────────
    // GET /api/student/my-profile
    // Returns: transcript info, latest AI result, selected courses
    // ─────────────────────────────────────────────────────────────────────────
    [HttpGet("my-profile")]
    public async Task<IActionResult> MyProfile()
    {
        var studentId = GetStudentId();
        if (string.IsNullOrEmpty(studentId))
            return Unauthorized(ApiResponse<string>.Fail("User not authorized."));

        var user = await _db.Users.FindAsync(studentId);
        if (user == null)
            return NotFound(ApiResponse<string>.Fail("Student not found."));

        var transcript = await _db.StudentTranscripts
            .FirstOrDefaultAsync(t => t.StudentId == studentId);

        var aiResult = await _db.AiAnalysisResults
            .Where(r => r.StudentId == studentId)
            .OrderByDescending(r => r.CreatedAt)
            .FirstOrDefaultAsync();

        var selectedCourses = await _db.StudentCourseSelections
            .Where(s => s.StudentId == studentId)
            .Include(s => s.Course)
            .Select(s => new CourseDto
            {
                Id = s.Course!.Id,
                Code = s.Course.Code,
                Name = s.Course.Name,
                CreditHours = s.Course.CreditHours,
                IsOpen = s.Course.IsOpen
            })
            .ToListAsync();

        var profile = new StudentProfileDto
        {
            StudentId = studentId,
            FullName = user.FullName,
            Email = user.Email ?? string.Empty,
            Transcript = transcript == null ? null : new TranscriptInfoDto
            {
                Id = transcript.Id,
                FilePath = Path.GetFileName(transcript.FilePath), // safe: only filename
                UploadDate = transcript.UploadDate
            },
            LatestAiResult = aiResult?.ResultJson,
            SelectedCourses = selectedCourses
        };

        return Ok(ApiResponse<StudentProfileDto>.Ok(profile, "Profile retrieved successfully."));
    }

    // ─────────────────────────────────────────────────────────────────────────
    // GET /api/student/available-courses
    // Returns only open courses where ALL prerequisites are completed by student
    // ─────────────────────────────────────────────────────────────────────────
    [HttpGet("available-courses")]
    public async Task<IActionResult> AvailableCourses()
    {
        var studentId = GetStudentId();
        if (string.IsNullOrEmpty(studentId))
            return Unauthorized(ApiResponse<string>.Fail("User not authorized."));

        // Get IDs of courses student has already completed
        var completedCourseIds = await _db.StudentCompletedCourses
            .Where(s => s.StudentId == studentId)
            .Select(s => s.CourseId)
            .ToHashSetAsync();

        // Get IDs of courses student already selected
        var selectedCourseIds = await _db.StudentCourseSelections
            .Where(s => s.StudentId == studentId)
            .Select(s => s.CourseId)
            .ToHashSetAsync();

        // Load open courses with their prerequisites
        var openCourses = await _db.Courses
            .Where(c => c.IsOpen)
            .Include(c => c.Prerequisites)
            .ToListAsync();

        var available = openCourses
            .Where(c =>
                // Not already completed
                !completedCourseIds.Contains(c.Id) &&
                // Not already selected
                !selectedCourseIds.Contains(c.Id) &&
                // All prerequisites are completed by this student
                c.Prerequisites.All(p => completedCourseIds.Contains(p.PrerequisiteId))
            )
            .Select(c => new CourseDto
            {
                Id = c.Id,
                Code = c.Code,
                Name = c.Name,
                CreditHours = c.CreditHours,
                IsOpen = c.IsOpen
            })
            .ToList();

        return Ok(ApiResponse<List<CourseDto>>.Ok(available, $"{available.Count} available course(s) found."));
    }

    // ─────────────────────────────────────────────────────────────────────────
    // POST /api/student/select-course/{courseId}
    // Student selects a course from their available list
    // ─────────────────────────────────────────────────────────────────────────
    [HttpPost("select-course/{courseId}")]
    public async Task<IActionResult> SelectCourse(int courseId)
    {
        var studentId = GetStudentId();
        if (string.IsNullOrEmpty(studentId))
            return Unauthorized(ApiResponse<string>.Fail("User not authorized."));

        var course = await _db.Courses
            .Include(c => c.Prerequisites)
            .FirstOrDefaultAsync(c => c.Id == courseId);

        if (course == null)
            return NotFound(ApiResponse<string>.Fail("Course not found."));

        if (!course.IsOpen)
            return BadRequest(ApiResponse<string>.Fail("This course is not open for registration."));

        // Verify student hasn't already selected it
        if (await _db.StudentCourseSelections.AnyAsync(s => s.StudentId == studentId && s.CourseId == courseId))
            return Conflict(ApiResponse<string>.Fail("You have already selected this course."));

        // Verify student hasn't already completed it
        if (await _db.StudentCompletedCourses.AnyAsync(s => s.StudentId == studentId && s.CourseId == courseId))
            return Conflict(ApiResponse<string>.Fail("You have already completed this course."));

        // Verify all prerequisites are met
        var completedCourseIds = await _db.StudentCompletedCourses
            .Where(s => s.StudentId == studentId)
            .Select(s => s.CourseId)
            .ToHashSetAsync();

        var unmetPrereqs = course.Prerequisites
            .Where(p => !completedCourseIds.Contains(p.PrerequisiteId))
            .ToList();

        if (unmetPrereqs.Any())
        {
            var prereqIds = string.Join(", ", unmetPrereqs.Select(p => p.PrerequisiteId));
            return BadRequest(ApiResponse<string>.Fail(
                $"You cannot register for this course. Missing prerequisites (course IDs): {prereqIds}."));
        }

        _db.StudentCourseSelections.Add(new StudentCourseSelection
        {
            StudentId = studentId,
            CourseId = courseId,
            SelectedAt = DateTime.UtcNow
        });

        await _db.SaveChangesAsync();
        return Ok(ApiResponse<string>.Ok(course.Name, $"Successfully registered for '{course.Name}'."));
    }

    // ─────────────────────────────────────────────────────────────────────────
    // GET /api/student/my-selections
    // ─────────────────────────────────────────────────────────────────────────
    [HttpGet("my-selections")]
    public async Task<IActionResult> MySelections()
    {
        var studentId = GetStudentId();
        if (string.IsNullOrEmpty(studentId))
            return Unauthorized(ApiResponse<string>.Fail("User not authorized."));

        var selections = await _db.StudentCourseSelections
            .Where(s => s.StudentId == studentId)
            .Include(s => s.Course)
            .Select(s => new CourseDto
            {
                Id = s.Course!.Id,
                Code = s.Course.Code,
                Name = s.Course.Name,
                CreditHours = s.Course.CreditHours,
                IsOpen = s.Course.IsOpen
            })
            .ToListAsync();

        return Ok(ApiResponse<List<CourseDto>>.Ok(selections, $"{selections.Count} course(s) selected."));
    }
}
