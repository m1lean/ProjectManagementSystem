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

        public async Task<List<ProjectTask>> GetTasksByProjectIdAsync(int projectId)
        {
            return await _context.ProjectTasks
                .Include(t => t.AssignedUser)
                .Where(t => t.ProjectId == projectId)
                .ToListAsync();
        }

        public async Task<ProjectTask> GetTaskByIdAsync(int id)
        {
            var task = await _context.ProjectTasks
                .Include(t => t.Project)
                .Include(t => t.AssignedUser)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (task == null)
            {
                throw new KeyNotFoundException($"Task with ID {id} not found");
            }

            return task;
        }

        public async Task CreateTaskAsync(ProjectTask task)
        {
            _context.ProjectTasks.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(ProjectTask task)
        {
            _context.ProjectTasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = await _context.ProjectTasks.FindAsync(id);
            if (task != null)
            {
                _context.ProjectTasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}