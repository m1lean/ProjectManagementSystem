using System.ComponentModel.DataAnnotations;

namespace ProjectManagementSystem.Models
{
    public class ProjectTask
    {
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        public string? Description { get; set; }

        public int ProjectId { get; set; }
        public Project? Project { get; set; }

        public int? AssignedUserId { get; set; }
        public User? AssignedUser { get; set; }
    }
}