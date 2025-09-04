using ProjectManagementSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetAllProjectsAsync(string statusFilter = null);
        Task<Project> GetProjectByIdAsync(int id);
        Task CreateProjectAsync(Project project);
        Task UpdateProjectAsync(Project project);
        Task DeleteProjectAsync(int id);
    }
}