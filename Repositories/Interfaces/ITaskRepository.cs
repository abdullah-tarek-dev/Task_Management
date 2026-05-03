using Task_Management.Queries;

public interface ITaskRepository
{
    Task<TaskItem?> GetByIdAsync(int id);
    Task AddAsync(TaskItem task);
    void Update(TaskItem task);
    void Delete(TaskItem task);
    Task<List<TaskItem>> GetFilteredTasks(TaskQueryParams query);
    Task SaveChangesAsync();
}