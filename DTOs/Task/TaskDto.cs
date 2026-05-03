using Task_Management.Models;

public class TaskDto
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public TaskItemStatus Status { get; set; }

    public DateTime DueDate { get; set; }
}