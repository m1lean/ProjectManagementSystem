using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagementSystem.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Status { get; set; } // e.g., "Open", "In Progress", "Closed"

        public ICollection<Task> Tasks { get; set; } = new List<Task>();
        public ICollection<ProjectUser> ProjectUsers { get; set; } = new List<ProjectUser>();
    }
}