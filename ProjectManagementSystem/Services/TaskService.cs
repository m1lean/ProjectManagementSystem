using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Data;
using ProjectManagementSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Services
{
    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext _context;

        public TaskService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Task>> GetTasksByProjectIdAsync(int projectId)
        {
            return await _context.Tasks
                .Where(t => t.ProjectId == projectId)
                .Include(t => t.AssignedUser)
                .ToListAsync();
        }

        public async Task<Task> GetTaskByIdAsync(int id)
        {
            return await _context.Tasks
                .Include(t => t.Project)
                .Include(t => t.AssignedUser)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task CreateTaskAsync(Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(Task task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}