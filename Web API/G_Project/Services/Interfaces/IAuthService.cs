using G_Project.Common;
using G_Project.Models;

namespace G_Project.Services.Interfaces;

public interface IAuthService
{
    Task<ApiResponse<string>> RegisterAsync(RegisterModel model);
    Task<ApiResponse<string>> LoginAsync(LoginModel model);
}
