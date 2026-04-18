using System.Security.Claims;
using System.Text.Json;
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
[Route("api/ai")]
[Authorize(Roles = "Admin")]
public class AIController : ControllerBase
{
    private readonly IAIService _aiService;
    private readonly ApplicationDbContext _db;

    public AIController(IAIService aiService, ApplicationDbContext db)
    {
        _aiService = aiService;
        _db = db;
    }

    // ─────────────────────────────────────────────────────────────────────────
    // POST /api/ai/analyze/{studentId}
    // Admin triggers AI analysis for a specific student
    // ─────────────────────────────────────────────────────────────────────────
    [HttpPost("analyze/{studentId}")]
    public async Task<IActionResult> Analyze(string studentId)
    {
        var student = await _db.Users.FindAsync(studentId);
        if (student == null)
            return NotFound(ApiResponse<object>.Fail($"Student with Id '{studentId}' not found."));

        var transcript = await _db.StudentTranscripts
            .FirstOrDefaultAsync(t => t.StudentId == studentId);

        if (transcript == null)
            return BadRequest(ApiResponse<object>.Fail("No transcript uploaded for this student yet."));

        var regulation = await _db.SystemRegulations
            .OrderByDescending(r => r.CreatedAt)
            .FirstOrDefaultAsync(r => r.IsActive);

        if (regulation == null)
            return BadRequest(ApiResponse<object>.Fail("No active university regulation file found. Admin must upload one first."));

        var response = await _aiService.AnalyzeStudentAsync(
            transcript.FilePath,
            regulation.FilePath,
            studentId);

        if (!response.Success)
            return BadRequest(response);

        // Serialize response
        string resultJson = "{}";
        if (response.Data is JsonElement jsonElement)
            resultJson = jsonElement.GetRawText();
        else if (response.Data != null)
            resultJson = JsonSerializer.Serialize(response.Data);

        // Save result (replacing any previous one for this student)
        var existing = await _db.AiAnalysisResults
            .FirstOrDefaultAsync(r => r.StudentId == studentId);
        if (existing != null)
            _db.AiAnalysisResults.Remove(existing);

        _db.AiAnalysisResults.Add(new AiAnalysisResult
        {
            StudentId = studentId,
            ResultJson = resultJson,
            CreatedAt = DateTime.UtcNow
        });

        await _db.SaveChangesAsync();

        return Ok(ApiResponse<object>.Ok(response.Data ?? new object(), "AI analysis complete and result saved."));
    }

    // ─────────────────────────────────────────────────────────────────────────
    // GET /api/ai/result/{studentId}
    // ─────────────────────────────────────────────────────────────────────────
    [HttpGet("result/{studentId}")]
    public async Task<IActionResult> GetResult(string studentId)
    {
        var result = await _db.AiAnalysisResults
            .Where(r => r.StudentId == studentId)
            .OrderByDescending(r => r.CreatedAt)
            .FirstOrDefaultAsync();

        if (result == null)
            return NotFound(ApiResponse<object>.Fail("No analysis result found for this student."));

        return Ok(ApiResponse<object>.Ok(result.ResultJson, "Result retrieved successfully."));
    }
}