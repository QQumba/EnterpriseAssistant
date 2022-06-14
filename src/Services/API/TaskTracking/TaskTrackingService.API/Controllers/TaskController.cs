using EnterpriseAssistant.Application.Shared;
using TaskTrackingService.Contract.DataTransfer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using EnterpriseAssistant.DataAccess;
using EnterpriseAssistant.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Mapster;
using TaskEntity = EnterpriseAssistant.DataAccess.Entities.Task;

namespace TaskTrackingService.API.Controllers;

[Authorize(Policy = "EnterpriseUser")]
[ApiController]
[Route("api/task")]
public class TaskController : ControllerBase
{
    private readonly EnterpriseAssistantDbContext _context;

    public TaskController(EnterpriseAssistantDbContext context)
    {
        _context = context;
    }

    // need to add project IsSoftDelete check
    [HttpPost] 
    [SwaggerOperation(Summary = "Create task", Description = "create task")]
    public async Task<ActionResult<TaskDto>> PostTask(TaskCreateDto taskCreate)
    {
        var project = await GetUserProjectAsync(taskCreate.ProjectId);

        if (project is null)
        {
            return NotFound($"Project with id {taskCreate.ProjectId} not found");
        }
        
        var task = taskCreate.Adapt<TaskEntity>();
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(PostTask), new {id = task.Id}, task.Adapt<TaskDto>());
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Read all user tasks", Description = "Read all user tasks")]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasks()
    {
        var authContext = User.GetAuthContext();
        var tasks = await _context.Tasks
            .Where(t => t.IsSoftDeleted == false &&
                        t.UserId == authContext.UserId &&
                        t.EnterpriseId == authContext.EnterpriseId)
            .ToListAsync();

        return Ok(tasks.Adapt<IEnumerable<TaskDto>>());
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Update task", Description = "Update task")]
    public async Task<ActionResult<EnterpriseAssistant.DataAccess.Entities.Task>> UpdateTask(long id, TaskCreateDto taskToUpdate)
    {
        var project = await GetUserProjectAsync(taskToUpdate.ProjectId);

        if (project is null)
        {
            return NotFound($"Project with id {taskToUpdate.ProjectId} not found");
        }
        
        var task = _context.Tasks.FirstOrDefault(task => task.Id == id); 
        if (task == null)
        {
            return NotFound();
        }
        task = taskToUpdate.Adapt(task);
        await _context.SaveChangesAsync();
        return Ok(task);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Delete task", Description = "Delete task")]
    public async Task<ActionResult<EnterpriseAssistant.DataAccess.Entities.Task>> TaskDelete(long id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
        {
            return NotFound();
        }

        task.IsSoftDeleted = true;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Read task by id", Description = "Read task by id")]
    public async Task<ActionResult<TaskDto>> GetTaskDetail(long id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
        {
            return NotFound($"Task with id {id} not found");
        }
        
        var project = await GetUserProjectAsync(task.ProjectId);
        if (project is null)
        {
            return NotFound($"Task with id {id} not found");
        }

        return task.Adapt<TaskDto>();
    }

    private Task<Project?> GetUserProjectAsync(long projectId)
    {
        var authContext = User.GetAuthContext();
        var project = _context.DepartmentUsers
            .Include(du => du.Department)
            .ThenInclude(d => d.Project)
            .Where(du => du.UserId == authContext.UserId &&
                         du.EnterpriseId.Equals(authContext.EnterpriseId) &&
                         du.Department.ProjectId == projectId
            )
            .Select(du => du.Department.Project)
            .FirstOrDefaultAsync();

        return project;
    }
}
