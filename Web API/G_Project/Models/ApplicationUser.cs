using Microsoft.AspNetCore.Identity;

namespace G_Project.Models;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
}