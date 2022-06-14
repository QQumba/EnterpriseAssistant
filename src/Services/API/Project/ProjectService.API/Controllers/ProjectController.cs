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
    private readonly IMediator _mediator;
    private readonly EnterpriseAssistantDbContext _context;

    public ProjectController(EnterpriseAssistantDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Add project", Description = "add project")]
    public async Task<ActionResult<ProjectDto>> CreateProject(ProjectCreateDto request)
    {
        var response = await _mediator.Send(new CreateDepartment(request.DepartmentCreate, User.GetAuthContext()));
        if (response.IsT1)
        {
            return BadRequest(response.AsT1.Message);
        }

        var project = request.Adapt<Project>();
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(CreateProject), new { id = project.Id }, project.Adapt<ProjectDto>());
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
    [SwaggerOperation(Summary = "Get all projects", Description = "Get all projects by enterprise")]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjects()
    {
        var enterpriseId = User.GetEnterpriseId();
        var projects = await _context.Projects.Where(p => p.EnterpriseId.Equals(enterpriseId)).ToListAsync();

        return Ok(projects.Adapt<IEnumerable<ProjectDto>>());
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