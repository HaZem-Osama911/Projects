using G_Project.Common;
using G_Project.Models;
using G_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace G_Project.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return BadRequest(ApiResponse<string>.Fail("Validation failed.", errors));
        }

        var response = await _authService.RegisterAsync(model);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginModel model)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return BadRequest(ApiResponse<string>.Fail("Validation failed.", errors));
        }

        var response = await _authService.LoginAsync(model);
        return response.Success ? Ok(response) : Unauthorized(response);
    }
}