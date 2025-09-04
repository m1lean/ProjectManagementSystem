using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Data;
using ProjectManagementSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.ProjectUsers).ThenInclude(pu => pu.Project)
                .Include(u => u.AssignedTasks)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<User>> GetParticipantsByProjectIdAsync(int projectId)
        {
            return await _context.ProjectUsers
                .Where(pu => pu.ProjectId == projectId)
                .Select(pu => pu.User)
                .ToListAsync();
        }

        public async Task AddParticipantToProjectAsync(int projectId, int userId)
        {
            if (!await _context.ProjectUsers.AnyAsync(pu => pu.ProjectId == projectId && pu.UserId == userId))
            {
                _context.ProjectUsers.Add(new ProjectUser { ProjectId = projectId, UserId = userId });
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveParticipantFromProjectAsync(int projectId, int userId)
        {
            var pu = await _context.ProjectUsers.FirstOrDefaultAsync(pu => pu.ProjectId == projectId && pu.UserId == userId);
            if (pu != null)
            {
                _context.ProjectUsers.Remove(pu);
                await _context.SaveChangesAsync();
            }
        }
    }
}