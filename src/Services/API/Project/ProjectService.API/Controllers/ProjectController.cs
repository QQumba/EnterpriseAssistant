using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using EnterpriseAssistant.Application.Shared;
using ProjectService.API.Commands;
using ProjectService.Contract.DataTransfer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ProjectService.API.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/project")]
public class ProjectController : ControllerBase
{
    private readonly IMediator _mediator;

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
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjectById(
        [Range(1, long.MaxValue), FromRoute] long projectId)
    {
        var result = await _mediator.Send(new GetProjectById(projectId));

        return result.Match<ActionResult>(Ok, x => NotFound());
    }
}
