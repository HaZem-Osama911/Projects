namespace MegaSoft.ViewModels.TeamViewModels
{
    public class TeamDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<TeamMemberDetailViewModel> Members { get; set; } = new();
        public int TasksCount { get; set; }
    }

    public class TeamMemberDetailViewModel
    {
        public string UserId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? Email { get; set; }
    }
}