using System.ComponentModel.DataAnnotations;
using Task_Management.Models;

public class CreateTaskDto
{
    [Required, MaxLength(100)]
    public string Title { get; set; }

    [Required, MaxLength(1000)]
    public string Description { get; set; }

    public DateTime DueDate { get; set; }

    public TaskItemStatus Status { get; set; } = TaskItemStatus.ToDo;
}