using G_Project.Common;
using G_Project.Services.Interfaces;
using System.Text.Json;

namespace G_Project.Services;

public class AIService : IAIService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<AIService> _logger;

    public AIService(HttpClient httpClient, ILogger<AIService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<ApiResponse<object>> AnalyzeStudentAsync(
        string transcriptPath, string regulationPath, string studentId)
    {
        try
        {
            // Build a multipart form request containing both files + studentId
            using var form = new MultipartFormDataContent();
            form.Add(new StringContent(studentId), "studentId");

            if (File.Exists(transcriptPath))
            {
                var transcriptBytes = await File.ReadAllBytesAsync(transcriptPath);
                form.Add(new ByteArrayContent(transcriptBytes), "transcript", Path.GetFileName(transcriptPath));
            }

            if (File.Exists(regulationPath))
            {
                var regulationBytes = await File.ReadAllBytesAsync(regulationPath);
                form.Add(new ByteArrayContent(regulationBytes), "regulation", Path.GetFileName(regulationPath));
            }

            var response = await _httpClient.PostAsync("analyze", form);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("AI Service returned non-success: {StatusCode}", response.StatusCode);
                return ApiResponse<object>.Fail("AI Service failed to analyze data.");
            }

            var resultString = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<object>(resultString);

            return ApiResponse<object>.Ok(responseObject ?? new object(), "Analysis successful.");
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error communicating with AI Service.");
            return ApiResponse<object>.Fail("Could not connect to AI Service. Ensure the Python backend is running.");
        }
    }
}
