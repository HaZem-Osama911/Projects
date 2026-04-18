using G_Project.Common;

namespace G_Project.Services.Interfaces;

public interface IFileService
{
    Task<ApiResponse<string>> UploadFileAsync(IFormFile file);
}
