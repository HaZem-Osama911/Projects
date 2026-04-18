namespace MegaSoft.ViewModels.TeamViewModels
{
    public class TeamMemberCheckboxViewModel
    {
        public string UserId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public bool IsSelected { get; set; }
    }
}