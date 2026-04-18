using G_Project.Common;
using G_Project.Services.Interfaces;

namespace G_Project.Services;

public class FileService : IFileService
{
    private readonly IConfiguration _config;
    private readonly ILogger<FileService> _logger;

    public FileService(IConfiguration config, ILogger<FileService> logger)
    {
        _config = config;
        _logger = logger;
    }

    public async Task<ApiResponse<string>> UploadFileAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return ApiResponse<string>.Fail("No file uploaded.");

        var fileSettings = _config.GetSection("FileSettings");
        var maxFileSizeInMB = fileSettings.GetValue<int>("MaxFileSizeInMB", 10);
        var allowedExtensions = fileSettings.GetSection("AllowedExtensions").Get<string[]>() ?? Array.Empty<string>();

        if (file.Length > maxFileSizeInMB * 1024 * 1024)
            return ApiResponse<string>.Fail($"File size exceeds the allowed limit of {maxFileSizeInMB}MB.");

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (allowedExtensions.Length > 0 && !allowedExtensions.Contains(extension))
            return ApiResponse<string>.Fail("File type is not allowed.");

        var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
        if (!Directory.Exists(uploadsPath))
            Directory.CreateDirectory(uploadsPath);

        var safeFileName = Guid.NewGuid().ToString() + extension;
        var path = Path.Combine(uploadsPath, safeFileName);

        try
        {
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            _logger.LogInformation("File uploaded successfully: {FilePath}", path);
            return ApiResponse<string>.Ok(path, "File uploaded successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while saving file to disk.");
            return ApiResponse<string>.Fail("An internal error occurred while saving the file.");
        }
    }
}
