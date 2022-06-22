using System.ComponentModel.DataAnnotations;
using ProjectService.Contract.DataTransfer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using EnterpriseAssistant.DataAccess;
using Microsoft.EntityFrameworkCore;
using EnterpriseAssistant.DataAccess.Entities;
using MediatR;
using Mapster;
using DepartmentService.Contract.Commands;
using DepartmentService.Contract.DataTransfer;
using EnterpriseAssistant.Application.Shared;
using EnterpriseAssistant.DataAccess.Entities.Enums;

namespace ProjectService.API.Controllers;

[Authorize(Policy = "EnterpriseUser")]
[ApiController]
[Route("api/project")]
public class ProjectController : ControllerBase
{
    private readonly DbContextFactory _factory;
    private readonly IMediator _mediator;
    private readonly EnterpriseAssistantDbContext _context;

    public ProjectController(DbContextFactory factory, IMediator mediator)
    {
        _context = factory.Create();
        _factory = factory;
        _mediator = mediator;
    }

    private AuthContext AuthContext => User.GetAuthContext();

    [HttpPost]
    [SwaggerOperation(Summary = "Add project", Description = "add project")]
    public async Task<ActionResult<ProjectDto>> CreateProject(ProjectCreateDto request)
    {
        var response = await _mediator.Send(new CreateDepartment(request.DepartmentCreate, AuthContext));
        if (response.IsT1)
        {
            return BadRequest(response.AsT1.Message);
        }

        var department = response.AsT0;

        var project = request.Adapt<Project>();
        project.EnterpriseId = AuthContext.EnterpriseId!;
        project.DepartmentId = department.Id;

        var createdProject = _context.Projects.Add(project).Entity;
        var projectDto = createdProject.Adapt<ProjectDto>();
        projectDto.DepartmentCode = department.Code;

        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(CreateProject), new { id = createdProject.Id }, projectDto);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Delete project by id", Description = "delete project")]
    public async Task<IActionResult> DeleteProject(long id)
    {
        var authContext = User.GetAuthContext();
        var project = await _context.DepartmentUsers
            .Include(du => du.Department)
            .ThenInclude(d => d.Project)
            .Where(du => du.UserId == authContext.UserId &&
                         du.EnterpriseId == authContext.EnterpriseId &&
                         du.DepartmentUserRole == DepartmentUserRole.Admin &&
                         du.Department.ProjectId == id)
            .Select(du => du.Department.Project)
            .FirstOrDefaultAsync();

        if (project == null)
        {
            return NotFound();
        }

        project.IsSoftDeleted = true;
        _context.Entry(project).Property(p => p.IsSoftDeleted).IsModified = true;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get user projects")]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjects()
    {
        var readOnlyContext = _factory.CreateReadOnlyContext(AuthContext);

        var tuples = await
            (from du in readOnlyContext.DepartmentUsers
                join d in readOnlyContext.Departments on du.DepartmentId equals d.Id
                join p in readOnlyContext.Projects on d.Id equals p.DepartmentId
                where du.UserId == AuthContext.UserId && du.EnterpriseId == AuthContext.EnterpriseId
                select new { p, d })
            .ToListAsync();
        var projects = tuples.Select(x =>
        {
            var project = x.p.Adapt<ProjectDto>();
            project.DepartmentCode = x.d.Code;
            return project;
        });

        return Ok(projects);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "get project by id", Description = "get project by id")]
    public async Task<ActionResult<ProjectDto>> GetProjectDetail(long id)
    {
        var authContext = User.GetAuthContext();
        var project = await _context.DepartmentUsers
            .Include(du => du.Department)
            .ThenInclude(d => d.Project)
            .Where(du => du.UserId == authContext.UserId &&
                         du.EnterpriseId == authContext.EnterpriseId &&
                         du.Department.ProjectId == id)
            .Select(du => du.Department.Project)
            .FirstOrDefaultAsync();

        if (project is null)
        {
            return NotFound($"Project with id {id} not found");
        }

        return Ok(project.Adapt<ProjectDto>());
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "update project by id", Description = "update project by id")]
    public async Task<ActionResult<ProjectDto>> UpdateProject([FromRoute, Range(1, long.MaxValue)] long id,
        ProjectCreateDto projectToUpdate)
    {
        var project = _context.Projects.FirstOrDefault(p => p.Id == id);
        if (project == null)
        {
            return NotFound($"Project with id {id} not found");
        }

        project = projectToUpdate.Adapt(project);
        _context.Projects.Update(project);
        await _context.SaveChangesAsync();

        return Ok(project.Adapt<ProjectDto>());
    }
}