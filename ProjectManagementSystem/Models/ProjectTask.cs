using System.ComponentModel.DataAnnotations;

namespace ProjectManagementSystem.Models
{
    public class ProjectTask
    {
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; } // Nullable but required (enforced by validation)

        public string? Description { get; set; } // Made nullable

        public int ProjectId { get; set; }
        public Project? Project { get; set; } // Made nullable

        public int? AssignedUserId { get; set; }
        public User? AssignedUser { get; set; } // Made nullable
    }
}