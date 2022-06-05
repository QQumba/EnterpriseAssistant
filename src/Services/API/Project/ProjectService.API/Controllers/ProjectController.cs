using ProjectService.Contract.DataTransfer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using EnterpriseAssistant.DataAccess;
using Microsoft.EntityFrameworkCore;
using EnterpriseAssistant.DataAccess.Entities;
using Mapster;

namespace ProjectService.API.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/project")]
public class ProjectController : ControllerBase
{
    private readonly EnterpriseAssistantDbContext _context;

    public ProjectController(EnterpriseAssistantDbContext context)
    {
        _context = context;
    }

    /*
    [HttpPost]
    [SwaggerOperation(Summary ="Create project", Description = "Create a project")]

    public async Task<ActionResult<ProjectCreateDto>> CreateProject([FromBody] ProjectCreateDto project)
    {
        var result = await _mediator.Send(new CreateProject(project));
        return Ok(result);
    }


    [HttpGet("{projectId:long}")]
    [SwaggerOperation(Summary = "Get prject by Id", Description = "Get project")]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjectById(
        [Range(1, long.MaxValue), FromRoute] long projectId)
    {
        var result = await _mediator.Send(new GetProjectById(projectId));

        return result.Match<ActionResult>(Ok, x => NotFound());
    }

    [HttpDelete("{projectId:long}")]
     [SwaggerOperation(Summary = "Delete project by id", Description = "delete project")]
      public async Task<IActionResult> ProjectDelete(long id)
     {

     }
    */

    [HttpPost]
    [SwaggerOperation(Summary = "Add project", Description = "add project")]
    public async Task<ActionResult<Project>> PostProject(ProjectCreateDto request)
    {

            var project = request.Adapt<Project>();
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(PostProject), new { id = project.Id }, project);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Delete project by id", Description = "delete project")]
    public async Task<IActionResult> DeleteProject(long id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null)
        {
            return NotFound();
        }

        project.IsSoftDeleted = true;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet] 
    [SwaggerOperation(Summary = "get all projects", Description = "get all projects")] 
    public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
    {
        if (_context == null)
        {
            return NotFound();
        }
        return await _context.Projects.ToListAsync();
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "get project by id", Description = "get project by id")]
    public async Task<ActionResult<Project>> GetProjectDetail(long id)
    {
        var project = await _context.Projects.FindAsync(id);

        if (project == null)
        {
            return NotFound();
        }

        return project;
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "update project by id", Description = "update project by id")]
    public async Task<ActionResult<Project>> UpdateProject(long id, ProjectCreateDto project)
    {
        var _update =  _context.Projects.FirstOrDefault(p => p.Id == id);
        if (_update != null)
        {
            _update = project.Adapt(_update);
            _context.SaveChanges();
        }
        else
        {
            return NotFound();
        }
        return Ok(_update);
    }
}
