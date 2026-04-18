using MegaSoft.Data;
using MegaSoft.Models;
using MegaSoft.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MegaSoft.Repositories.Implementations
{
    public class TeamRepository : ITeamRepository
    {
        private readonly ApplicationDbContext _context;

        public TeamRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Team>> GetAllAsync()
        {
            return await _context.Teams
                .Where(t => !t.IsDeleted)
                .Include(t => t.Members)
                    .ThenInclude(m => m.User)
                .Include(t => t.Tasks)
                .OrderBy(t => t.Name)
                .ToListAsync();
        }

        public async Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            return await _context.Users
                .OrderBy(u => u.UserName)
                .ToListAsync();
        }

        public async Task<Team?> GetByIdAsync(int id)
        {
            return await _context.Teams
                .Include(t => t.Members)
                    .ThenInclude(m => m.User)
                .Include(t => t.Tasks)
                .FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);
        }

        public async Task<Team> CreateAsync(Team team)
        {
            team.CreatedAt = DateTime.UtcNow;
            team.IsDeleted = false;
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();
            return team;
        }

        public async Task<bool> EditTeamNameAsync(int id, string teamName)
        {
            var team = await _context.Teams.FindAsync(id);

            if (team == null || team.IsDeleted)
                return false;

            team.Name = teamName.Trim();
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateAsync(Team team)
        {
            var existingTeam = await _context.Teams.FindAsync(team.Id);

            if (existingTeam == null || existingTeam.IsDeleted)
                return false;

            existingTeam.Name = team.Name;
            existingTeam.Description = team.Description;

            _context.Teams.Update(existingTeam);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var team = await _context.Teams
                .FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);

            if (team == null) return false;

            team.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateTeamMembersAsync(int teamId, IEnumerable<string> selectedUserIds)
        {
            var normalizedSelected = selectedUserIds
                .Where(id => !string.IsNullOrWhiteSpace(id))
                .Select(id => id.Trim())
                .Distinct()
                .ToList();

            var existingMemberUserIds = await _context.TeamMembers
                .Where(tm => tm.TeamId == teamId)
                .Select(tm => tm.UserId)
                .ToListAsync();

            var toRemove = existingMemberUserIds.Except(normalizedSelected).ToList();
            var toAdd = normalizedSelected.Except(existingMemberUserIds).ToList();

            if (toRemove.Count > 0)
            {
                var membersToRemove = await _context.TeamMembers
                    .Where(tm => tm.TeamId == teamId && toRemove.Contains(tm.UserId))
                    .ToListAsync();
                _context.TeamMembers.RemoveRange(membersToRemove);
            }

            if (toAdd.Count > 0)
            {
                var newMembers = toAdd.Select(userId => new TeamMember
                {
                    TeamId = teamId,
                    UserId = userId,
                    JoinedAt = DateTime.UtcNow
                });
                await _context.TeamMembers.AddRangeAsync(newMembers);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}