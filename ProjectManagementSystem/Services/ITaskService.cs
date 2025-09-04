using ProjectManagementSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<Task>> GetTasksByProjectIdAsync(int projectId);
        Task<Task> GetTaskByIdAsync(int id);
        Task CreateTaskAsync(Task task);
        Task UpdateTaskAsync(Task task);
        Task DeleteTaskAsync(int id);
    }
}