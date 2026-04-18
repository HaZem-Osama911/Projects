using G_Project.Common;
using G_Project.Models;
using G_Project.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace G_Project.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _config;
    private readonly ILogger<AuthService> _logger;

    public AuthService(UserManager<ApplicationUser> userManager, IConfiguration config, ILogger<AuthService> logger)
    {
        _userManager = userManager;
        _config = config;
        _logger = logger;
    }

    public async Task<ApiResponse<string>> RegisterAsync(RegisterModel model)
    {
        var user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            FullName = model.FullName
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            return ApiResponse<string>.Fail("Registration failed.", errors);
        }

        // Assign role — only allow Admin or Student
        var role = (model.Role == "Admin") ? "Admin" : "Student";
        await _userManager.AddToRoleAsync(user, role);

        return ApiResponse<string>.Ok("User Created Successfully", $"Registration successful as {role}.");
    }

    public async Task<ApiResponse<string>> LoginAsync(LoginModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null)
            return ApiResponse<string>.Fail("Invalid Email or Password");

        var validPassword = await _userManager.CheckPasswordAsync(user, model.Password);

        if (!validPassword)
            return ApiResponse<string>.Fail("Invalid Email or Password");

        var token = await GenerateJwtTokenAsync(user);

        return ApiResponse<string>.Ok(token, "Login Successful");
    }

    private async Task<string> GenerateJwtTokenAsync(ApplicationUser user)
    {
        var jwtSettings = _config.GetSection("JWT");

        // Fetch roles and add them as claims
        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email ?? ""),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.FullName ?? ""),
            new Claim(ClaimTypes.Email, user.Email ?? "")
        };

        // Add each role as a Role claim so [Authorize(Roles="Admin")] works
        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["DurationInMinutes"]!)),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
