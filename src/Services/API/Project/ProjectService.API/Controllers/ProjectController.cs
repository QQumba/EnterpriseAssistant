using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using EnterpriseAssistant.Application.Shared;
using ProjectService.API.Commands;
using ProjectService.Contract.DataTransfer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using EnterpriseAssistant.DataAccess;
using Microsoft.EntityFrameworkCore;
using EnterpriseAssistant.DataAccess.Entities;

namespace ProjectService.API.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/project")]
public class ProjectController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly EnterpriseAssistantDbContext _context;

    public ProjectController(IMediator mediator)
    {
        _mediator = mediator;
    }

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

    /* [HttpDelete("{projectId:long}")]
     [SwaggerOperation(Summary = "Delete project by id", Description = "delete project")]
      public async Task<IActionResult> ProjectDelete(long id)
     {

     }
    */

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Delete project by id", Description = "delete project")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null)
        {
            return NotFound();
        }

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet] // Can be upgr to get by user
    [SwaggerOperation(Summary = "get projets", Description = "get project")] 
    public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
    {
        if (_context == null)
        {
            return NotFound();
        }
        return await _context.Projects.ToListAsync();
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "get projets by id", Description = "get project by id")]
    public async Task<ActionResult<Project>> GetBikeDetail(int id)
    {
        var project = await _context.Projects.FindAsync(id);

        if (project == null)
        {
            return NotFound();
        }

        return project;
    }
}
