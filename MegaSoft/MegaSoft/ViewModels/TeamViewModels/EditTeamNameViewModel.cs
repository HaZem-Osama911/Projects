using System.ComponentModel.DataAnnotations;

namespace MegaSoft.ViewModels.TeamViewModels
{
    public class EditTeamNameViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Team name is required")]
        [StringLength(100, ErrorMessage = "Maximum length is 100 characters")]
        [Display(Name = "Team Name")]
        public string Name { get; set; } = string.Empty;
    }
}