using System.Collections.Generic;

namespace ProjectManagementSystem.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public ICollection<ProjectUser> ProjectUsers { get; set; } = new List<ProjectUser>();
        public ICollection<Task> AssignedTasks { get; set; } = new List<Task>();
    }
}