namespace ProjectManagementSystem.Models
{
    public class ProjectUser
    {
        public int ProjectId { get; set; }
        public Project? Project { get; set; } // Made nullable

        public int UserId { get; set; }
        public User? User { get; set; } // Made nullable
    }
}