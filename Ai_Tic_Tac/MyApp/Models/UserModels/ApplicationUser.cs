using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Models.UserModels
{
    public class ApplicationUser : IdentityUser
    {
        // Add custom properties for the application user here
        [Required, MaxLength(100)]
        public string? FirstName { get; set; }

        [Required, MaxLength(100)]
        public string? LastName { get; set; }

        public byte[]? ProfilePicture { get; set; }
    }
}