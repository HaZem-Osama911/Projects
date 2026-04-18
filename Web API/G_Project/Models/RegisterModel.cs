using System.ComponentModel.DataAnnotations;

namespace G_Project.Models
{
    public class RegisterModel
    {
        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        /// <summary>Role to assign: "Admin" or "Student" (defaults to Student)</summary>
        public string Role { get; set; } = "Student";
    }
}