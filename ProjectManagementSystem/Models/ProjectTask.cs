using System.ComponentModel.DataAnnotations;

namespace ProjectManagementSystem.Models
{
    public class ProjectTask
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название задачи обязательно")]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;

        public int? AssignedUserId { get; set; }
        public User? AssignedUser { get; set; }
    }
}