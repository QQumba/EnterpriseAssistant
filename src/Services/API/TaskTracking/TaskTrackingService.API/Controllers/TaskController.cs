using TaskTrackingService.Contract.DataTransfer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using EnterpriseAssistant.DataAccess;
using Microsoft.EntityFrameworkCore;
using EnterpriseAssistant.DataAccess.Entities;
using Mapster;

namespace TaskTrackingService.API.Controllers;

[Authorize]
[ApiController]
[Route("api/task")]
public class TaskController : ControllerBase
{
    private readonly EnterpriseAssistantDbContext _context;

    public TaskController(EnterpriseAssistantDbContext context)
    {
        _context = context;
    }

    [HttpPost] //need to add project IsSoftDelete check
    [SwaggerOperation(Summary = "Create task", Description = "create task")]
    public async Task<ActionResult<EnterpriseAssistant.DataAccess.Entities.Task>> PostTask(TaskCreateDto request)
    {
        var task = request.Adapt<EnterpriseAssistant.DataAccess.Entities.Task>();
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(PostTask), new {id = task.Id}, task);
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Read all task", Description = "read all task")]
    public async Task<ActionResult<IEnumerable<EnterpriseAssistant.DataAccess.Entities.Task>>> GetTasks()
    {
        if (_context == null)
        {
            return NotFound();
        }

        return await _context.Tasks.ToListAsync();
    }

    [HttpGet("user/{id}")] //later
    [SwaggerOperation(Summary = "Read all user task", Description = "User task")]
    public async Task<ActionResult<IEnumerable<EnterpriseAssistant.DataAccess.Entities.Task>>> GetUserTasks(long id)
    {
        return NotFound();
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Update task", Description = "update task")]
    public async Task<ActionResult<EnterpriseAssistant.DataAccess.Entities.Task>> UpdateTask(long id, TaskCreateDto task)
    {
        var _update = _context.Tasks.FirstOrDefault(task => task.Id == id); 
        if (_update == null)
        {
            return NotFound();
        }
        _update = task.Adapt(_update);
        _context.SaveChanges();
        return Ok(_update);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Delete task", Description = "delete task")]
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
    [SwaggerOperation(Summary = "task by id", Description = "Read task by id")]
    public async Task<ActionResult<EnterpriseAssistant.DataAccess.Entities.Task>> GetTaskDetail(long id)
    {
        var task = await _context.Tasks.FindAsync(id);

        if (task == null)
        {
            return NotFound();
        }

        return task;
    }
}
