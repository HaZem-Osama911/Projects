using System.ComponentModel.DataAnnotations;

namespace MegaSoft.ViewModels.TeamViewModels
{
    public class EditTeamViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Team name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Team name must be between 3 and 100 characters")]
        [Display(Name = "Team Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Team Members")]
        public List<TeamMemberCheckboxViewModel> Members { get; set; } = new();
    }
}