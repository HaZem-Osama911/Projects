using G_Project.Common;

namespace G_Project.Services.Interfaces;

public interface IAIService
{
    Task<ApiResponse<object>> AnalyzeStudentAsync(string transcriptPath, string regulationPath, string studentId);
}
