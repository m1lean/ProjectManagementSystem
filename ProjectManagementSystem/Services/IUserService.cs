using ProjectManagementSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);

        // For participants
        Task<IEnumerable<User>> GetParticipantsByProjectIdAsync(int projectId);
        Task AddParticipantToProjectAsync(int projectId, int userId);
        Task RemoveParticipantFromProjectAsync(int projectId, int userId);
    }
}