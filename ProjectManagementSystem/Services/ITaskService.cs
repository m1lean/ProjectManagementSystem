using ProjectManagementSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Services
{
    public interface IProjectTaskService // Optional: Rename interface for clarity
    {
        Task<IEnumerable<ProjectTask>> GetTasksByProjectIdAsync(int projectId);
        Task<ProjectTask> GetTaskByIdAsync(int id);
        Task CreateTaskAsync(ProjectTask task);
        Task UpdateTaskAsync(ProjectTask task);
        Task DeleteTaskAsync(int id);
    }
}