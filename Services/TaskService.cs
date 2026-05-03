public class TaskService
{
    private readonly ITaskRepository _repo;

    public TaskService(ITaskRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<TaskDto>> GetTasks(TaskQueryParams query)
    {
        var tasks = await _repo.GetFilteredTasks(query);
        return tasks.Select(Map).ToList();
    }

    public async Task<TaskDto?> GetTaskById(int id)
    {
        var task = await _repo.GetByIdAsync(id);

        if (task == null) return null;

        return Map(task);
    }

    public async Task CreateTask(CreateTaskDto dto, int userId)
    {
        if (dto.DueDate < DateTime.UtcNow)
            throw new ArgumentException("Due date must be in the future");

        var task = new TaskItem
        {
            Title = dto.Title,
            Description = dto.Description,
            DueDate = dto.DueDate,
            Status = dto.Status,
            UserId = userId
        };

        await _repo.AddAsync(task);
        await _repo.SaveChangesAsync();
    }

    public async Task<bool> UpdateTask(int id, UpdateTaskDto dto)
    {
        var task = await _repo.GetByIdAsync(id);

        if (task == null) return false;

        task.Title = dto.Title;
        task.Description = dto.Description;
        task.Status = dto.Status;
        task.DueDate = dto.DueDate;

        _repo.Update(task);
        await _repo.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteTask(int id)
    {
        var task = await _repo.GetByIdAsync(id);

        if (task == null) return false;

        _repo.Delete(task);
        await _repo.SaveChangesAsync();

        return true;
    }

    private TaskDto Map(TaskItem t)
    {
        return new TaskDto
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            Status = t.Status,
            DueDate = t.DueDate
        };
    }
}