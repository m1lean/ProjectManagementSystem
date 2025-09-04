using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Data;
using ProjectManagementSystem.Models;
using System.Collections.Generic;
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
            return await _context.Users
                .Include(u => u.ProjectUsers).ThenInclude(pu => pu.Project)
                .Include(u => u.AssignedTasks)
                .ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id) // Changed to User?
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

        public async Task AddUserToProjectAsync(int userId, int projectId)
        {
            _context.ProjectUsers.Add(new ProjectUser { UserId = userId, ProjectId = projectId });
            await _context.SaveChangesAsync();
        }

        public async Task RemoveUserFromProjectAsync(int userId, int projectId)
        {
            var projectUser = await _context.ProjectUsers
                .FirstOrDefaultAsync(pu => pu.UserId == userId && pu.ProjectId == projectId);
            if (projectUser != null)
            {
                _context.ProjectUsers.Remove(projectUser);
                await _context.SaveChangesAsync();
            }
        }
    }
}