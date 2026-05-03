using Microsoft.EntityFrameworkCore;
using Task_Management.Queries;
using Task_Management.Models;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TaskItem?> GetByIdAsync(int id)
        => await _context.Tasks
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id);

    public async Task AddAsync(TaskItem task)
        => await _context.Tasks.AddAsync(task);

    public void Update(TaskItem task)
        => _context.Tasks.Update(task);

    public void Delete(TaskItem task)
        => _context.Tasks.Remove(task);

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();

    public async Task<List<TaskItem>> GetFilteredTasks(TaskQueryParams query)
    {
        var tasks = _context.Tasks
            .AsNoTracking()
            .AsQueryable();

        // 🔍 Search
        if (!string.IsNullOrEmpty(query.Search))
        {
            var search = query.Search.ToLower();
            tasks = tasks.Where(t => t.Title.ToLower().Contains(search));
        }

        // 🎯 Filter by Status
        if (query.Status.HasValue)
        {
            tasks = tasks.Where(t => t.Status == query.Status);
        }

        // 📄 Pagination
        var skip = (query.PageNumber - 1) * query.PageSize;

        return await tasks
            .Skip(skip)
            .Take(query.PageSize)
            .ToListAsync();
    }
}