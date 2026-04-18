namespace MyApp.ViewModels
{
    public class RoleFormViewModel
    {
        [Required, MaxLength(255)]
        public string RoleName { get; set; } = string.Empty;
    }
}
