using System.ComponentModel.DataAnnotations;

namespace Task_Management.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        [Required, MaxLength(200)]
        public string Title { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        public DateTime DueDate { get; set; } = DateTime.UtcNow;
        public int UserId { get; set; } // Foreign key to User
            public User User { get; set; } // Navigation property to User
        public TaskItemStatus Status { get; set; } = TaskItemStatus.ToDo;

    }
}
