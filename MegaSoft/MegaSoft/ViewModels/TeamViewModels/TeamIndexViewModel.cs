namespace MegaSoft.ViewModels.TeamViewModels
{
    public class TeamIndexViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int MembersCount { get; set; }
        public int TasksCount { get; set; }
    }
}