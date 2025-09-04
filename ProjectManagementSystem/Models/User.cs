using System.Collections.Generic;

namespace ProjectManagementSystem.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; } // Made nullable
        public string? Email { get; set; } // Made nullable

        public ICollection<ProjectUser> ProjectUsers { get; set; } = new List<ProjectUser>();
        public ICollection<ProjectTask> AssignedTasks { get; set; } = new List<ProjectTask>();
    }
}