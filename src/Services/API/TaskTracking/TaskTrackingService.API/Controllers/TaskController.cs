using TaskTrackingService.Contract.DataTransfer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using EnterpriseAssistant.DataAccess;
using Microsoft.EntityFrameworkCore;
using EnterpriseAssistant.DataAccess.Entities;
using Mapster;

namespace TaskTrackingService.API.Controllers;

[AllowAnonymous]
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

    [HttpGet("user/{id}")]
    [SwaggerOperation(Summary = "Read all user task", Description = "User task")]
    public async Task<ActionResult<IEnumerable<EnterpriseAssistant.DataAccess.Entities.Task>>> GetUserTasks(long id)
    {
        return NotFound();
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Update task", Description = "update task")]
    public async Task<ActionResult<EnterpriseAssistant.DataAccess.Entities.Task>> UpdateTask(long id, TaskCreateDto task)
    {
        return Ok();
    }

    [HttpDelete]
    [SwaggerOperation(Summary = "Delete task", Description = "delete task")]
    public async Task<ActionResult<EnterpriseAssistant.DataAccess.Entities.Task>> TaskDelete(long id)
    {
        return Ok();
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "task by id", Description = "Read task by id")]
    public async Task<ActionResult<EnterpriseAssistant.DataAccess.Entities.Task>> GetTaskDetail(long id)
    {
        return NotFound();
    }
}
