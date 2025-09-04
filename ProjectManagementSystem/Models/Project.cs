using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagementSystem.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; } // Nullable but required

        public string? Description { get; set; } // Made nullable

        public string? Status { get; set; } // Made nullable

        public ICollection<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();
        public ICollection<ProjectUser> ProjectUsers { get; set; } = new List<ProjectUser>();
    }
}