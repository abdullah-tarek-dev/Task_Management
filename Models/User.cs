using System.ComponentModel.DataAnnotations;

namespace Task_Management.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        public string Role { get; set; } = "User"; // Default role is "User"
        public List<TaskItem> Tasks { get; set; } = new List<TaskItem>(); // Navigation property for related tasks

    }
}
