using System.Collections;
using EnterpriseAssistant.DataAccess;
using Microsoft.EntityFrameworkCore;
using Task = EnterpriseAssistant.DataAccess.Entities.Tasks.Task;

namespace EnterpriseAssistant.Web.Features.Tasks.Repositories;

public interface ITaskRepository
{
    Task<Task> CreateTask(Task task);
    Task<Task?> ReadTasksById(long taskId);
    Task<IEnumerable<Task>> ReadTasksByUserId(long userId);
    Task<Task> UpdateTask(Task task);
}

internal class TaskRepository : ITaskRepository
{
    private readonly EnterpriseAssistantDbContext _context;

    public TaskRepository(EnterpriseAssistantDbContext context)
    {
        _context = context;
    }

    public async Task<Task> CreateTask(Task task)
    {
        task.CreatedByUserId = 1;
        var createdTask = _context.Tasks.Add(task).Entity;
        await _context.SaveChangesAsync();
        return createdTask;
    }

    public async Task<Task?> ReadTasksById(long taskId)
    {
        var task = await _context.Tasks.FirstOrDefaultAsync(t => t.TaskId == taskId);
        return task;
    }

    public async Task<IEnumerable<Task>> ReadTasksByUserId(long userId)
    {
        var userTasks = await _context.Tasks
            .Include(t => t.AssignedUsers)
            .Where(t => t.AssignedUsers.Any(au => au.Id == userId))
            .ToListAsync();
        return userTasks;
    }

    public async Task<Task> UpdateTask(Task task)
    {
        var updatedTask = _context.Tasks.Update(task).Entity;
        await _context.SaveChangesAsync();
        return updatedTask;
    }
}