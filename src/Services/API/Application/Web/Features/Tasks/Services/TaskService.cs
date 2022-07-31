using EnterpriseAssistant.DataAccess;
using EnterpriseAssistant.Web.Features.Tasks.DataTransfer;
using Task = EnterpriseAssistant.DataAccess.Entities.Tasks.Task;

namespace EnterpriseAssistant.Web.Features.Tasks.Services;

public interface ITaskService
{
    Task<(string?, Task?)> UpdateTask(Task taskToUpdate, TaskUpdateDto task);
    Task<double> BurnHours(Task task, double hours);
}

internal class TaskService : ITaskService
{
    private readonly EnterpriseAssistantDbContext _context;

    public TaskService(EnterpriseAssistantDbContext context)
    {
        _context = context;
    }

    public async Task<(string?, Task?)> UpdateTask(Task taskToUpdate, TaskUpdateDto task)
    {
        return ("error", null);
    }

    public async Task<double> BurnHours(Task task, double hours)
    {
        throw new NotImplementedException();
    }
}