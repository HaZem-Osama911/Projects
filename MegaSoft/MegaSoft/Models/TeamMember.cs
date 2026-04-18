namespace MegaSoft.Models
{
    public class TeamMember
    {
        public int Id { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; } = default!;

        public string UserId { get; set; } = default!;
        public ApplicationUser User { get; set; } = default!;

        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    }
}