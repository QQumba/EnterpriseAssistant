using EnterpriseAssistant.Application.Shared;
using TaskTrackingService.Contract.DataTransfer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using EnterpriseAssistant.DataAccess;
using EnterpriseAssistant.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Mapster;
using UserService.Contract.DataTransfer;
using TaskEntity = EnterpriseAssistant.DataAccess.Entities.Task;

namespace TaskTrackingService.API.Controllers;

[Authorize(Policy = "EnterpriseUser")]
[ApiController]
[Route("api/task")]
public class TaskController : ControllerBase
{
    private readonly DbContextFactory _factory;
    private readonly EnterpriseAssistantDbContext _context;

    public TaskController(DbContextFactory factory)
    {
        _factory = factory;
        _context = factory.Create();
    }

    private AuthContext AuthContext => User.GetAuthContext();

    // need to add project IsSoftDelete check
    [HttpPost]
    [SwaggerOperation(Summary = "Create task", Description = "create task")]
    public async Task<ActionResult<TaskDto>> CreateTask(TaskCreateDto taskCreate)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(x => x.Id == taskCreate.ProjectId);

        if (project is null)
        {
            return NotFound($"Project with id {taskCreate.ProjectId} not found");
        }

        var task = taskCreate.Adapt<TaskEntity>();
        task.EnterpriseId = AuthContext.EnterpriseId!;

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(CreateTask), new { id = task.Id }, task.Adapt<TaskDto>());
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

    [HttpGet("by-project/{projectId:long}")]
    [SwaggerOperation(Summary = "Read all project tasks")]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasksByProjectId(long projectId)
    {
        var authContext = User.GetAuthContext();
        var tasks = await _context.Tasks
            .Where(t => t.IsSoftDeleted == false &&
                        t.EnterpriseId == authContext.EnterpriseId &&
                        t.ProjectId == projectId)
            .ToListAsync();

        return Ok(tasks.Adapt<IEnumerable<TaskDto>>());
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Update task", Description = "Update task")]
    public async Task<ActionResult<EnterpriseAssistant.DataAccess.Entities.Task>> UpdateTask(long id,
        TaskCreateDto taskToUpdate)
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
    public async Task<ActionResult<EnterpriseAssistant.DataAccess.Entities.Task>> DeleteTask(long id)
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
    public async Task<ActionResult<TaskDto>> GetTaskById(long id)
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

    [HttpPut("{id:long}/assign")]
    [SwaggerOperation(Summary = "Assign user", Description = "Assign user to task")]
    public async Task<ActionResult<UserDto>> AssignUser([FromRoute] long id, [FromBody] AssigneeDto assignee)
    {
        var readOnlyContext = _factory.CreateReadOnlyContext(AuthContext);
        var user = (await readOnlyContext.EnterpriseUsers
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Login == assignee.Login))?.User;
        if (user == null)
        {
            return NotFound($"User with login {assignee.Login} not found");
        }

        var task = await readOnlyContext.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        if (task == null)
        {
            return NotFound($"Task with id {id} not found");
        }

        var taskUser = new TaskUser
        {
            UserId = user.Id,
            TaskId = id,
            EnterpriseId = AuthContext.EnterpriseId!
        };
        _context.TaskUsers.Add(taskUser);
        await _context.SaveChangesAsync();

        return Ok(user.Adapt<UserDto>());
    }

    [HttpPut("{id:long}/track")]
    [SwaggerOperation(Summary = "Track time to task", Description = "Track time to task")]
    public async Task<ActionResult> TrackTime([FromRoute] long id, [FromBody] TrackTimeDto trackTimeDto)
    {
        var readOnlyContext = _factory.CreateReadOnlyContext(AuthContext);
        var task = await readOnlyContext.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        if (task == null)
        {
            return NotFound($"Task with id {id} not found");
        }

        var user = await readOnlyContext.Users.FirstOrDefaultAsync(u => u.Id == AuthContext.UserId);

        var taskUser = await readOnlyContext.TaskUsers
            .FirstOrDefaultAsync(x => x.UserId == AuthContext.UserId && x.TaskId == id);

        if (taskUser == null)
        {
            return NotFound($"User with id {AuthContext.UserId} not assigned to task with id {id}");
        }

        taskUser.HoursSpent += trackTimeDto.HoursSpent;
        _context.TaskUsers.Update(taskUser);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpGet("{id}/users")]
    [SwaggerOperation(Summary = "Read all users assigned to task", Description = "Read all users assigned to task")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersAssignedToTask(long id)
    {
        var readOnlyContext = _factory.CreateReadOnlyContext(AuthContext);
        var task = await readOnlyContext.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        if (task == null)
        {
            return NotFound($"Task with id {id} not found");
        }

        var taskUsers = await readOnlyContext.TaskUsers
            .Where(x => x.TaskId == id)
            .Include(x => x.User)
            .Select(x => x.User)
            .ToListAsync();

        return Ok(taskUsers.Adapt<List<UserDto>>());
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