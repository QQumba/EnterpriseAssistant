using System.ComponentModel.DataAnnotations;
using EnterpriseAssistant.DataAccess;
using EnterpriseAssistant.Web.Features.Tasks.DataTransfer;
using EnterpriseAssistant.Web.Features.Tasks.Repositories;
using EnterpriseAssistant.Web.Features.Tasks.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using Task = EnterpriseAssistant.DataAccess.Entities.Tasks.Task;

namespace EnterpriseAssistant.Web.Features.Tasks.Controllers;

[ApiController]
[Route("api/task")]
public class TaskController : ControllerBase
{
    private readonly EnterpriseAssistantDbContext _context;
    private readonly ITaskService _taskService;
    private readonly ITaskRepository _repository;

    public TaskController(EnterpriseAssistantDbContext context, ITaskService taskService, ITaskRepository repository)
    {
        _context = context;
        _taskService = taskService;
        _repository = repository;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a new task", Description = "Create a new task")]
    public async Task<ActionResult<TaskDto>> CreateTask([FromBody] TaskCreateDto task)
    {
        var createdTask = await _repository.CreateTask(task.Adapt<Task>());
        return CreatedAtAction(nameof(CreateTask), createdTask.Adapt<TaskDto>());
    }

    [HttpGet("{taskId:long}")]
    [SwaggerOperation(Summary = "Read task by id")]
    public async Task<ActionResult<TaskDto>> ReadTaskById(long taskId)
    {
        var task = await _repository.ReadTasksById(taskId);
        if (task is null)
        {
            return NotFound($"Task with id {taskId} not found");
        }

        return Ok(task.Adapt<TaskDto>());
    }
    
    [HttpGet]
    [SwaggerOperation(Summary = "Read tasks by user id")]
    public async Task<ActionResult<IEnumerable<TaskDto>>> ReadTasksByUserId([FromQuery] long userId)
    {
        var task = await _repository.ReadTasksByUserId(userId);
        return Ok(task.Adapt<IEnumerable<TaskDto>>());
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Update a task", Description = "Update a task")]
    public async Task<ActionResult<TaskDto>> UpdateTask([FromBody] TaskUpdateDto task)
    {
        var taskToUpdate = await _repository.ReadTasksById(task.TaskId);
        if (taskToUpdate is null)
        {
            return NotFound("Task not found");
        }

        var updatedTask = await _repository.UpdateTask(task.Adapt(taskToUpdate));
        return Ok(updatedTask.Adapt<TaskDto>());
    }

    [HttpPut("{taskId:long}/users")]
    [SwaggerOperation(Summary = "Assigns users to task", Description = "Assigns users to task")]
    public async Task<ActionResult<UserDto>> AssignUsers([FromRoute, Range(1, long.MaxValue)] long taskId,
        [FromBody, MinLength(1)] IEnumerable<long> userIds)
    {
        var assignedUsers = userIds.Select(x => new UserDto { UserId = x });
        return Ok(assignedUsers);
    }

    [HttpPut("{taskId:long}/tags")]
    public async Task<ActionResult<IEnumerable<TagDto>>> AddTags([FromRoute, Range(1, long.MaxValue)] long taskId,
        [FromBody, MinLength(1)] IEnumerable<long> tagsId)
    {
        var addedTags = tagsId.Select(x => new TagDto { TagId = x });
        return Ok(addedTags);
    }

    [HttpPut("{taskId:long}/burn-hours")]
    [SwaggerOperation(Summary = "Burn hours to task", Description = "Burn hours to task")]
    public async Task<ActionResult<double>> BurnHours([FromRoute, Range(1, long.MaxValue)] long taskId,
        [FromBody, Range(0, double.MaxValue)] double hours)
    {
        var task = await _context.Tasks.FirstOrDefaultAsync(x => x.TaskId == taskId);
        if (task is null)
        {
            return NotFound("Task not found");
        }

        var hoursBurnt = await _taskService.BurnHours(task, hours);

        return Ok(hours);
    }
}