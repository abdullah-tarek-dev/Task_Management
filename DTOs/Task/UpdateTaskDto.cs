using System.ComponentModel.DataAnnotations;
using Task_Management.Models;

public class UpdateTaskDto
{
    [Required, MaxLength(100)]
    public string Title { get; set; }

    [MaxLength(1000)]
    public string Description { get; set; }

    [Required]
    public TaskItemStatus Status { get; set; }

    public DateTime DueDate { get; set; }
}