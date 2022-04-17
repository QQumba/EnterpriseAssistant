using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using EnterpriseAssistant.Application.Shared;
using EnterpriseService.API.Commands;
using EnterpriseService.Contract.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UserService.Contract.ViewModels;

namespace EnterpriseService.API;

[AllowAnonymous]
[ApiController]
[Route("api/enterprise")]
public class EnterpriseController : ControllerBase
{
    private readonly IMediator _mediator;

    public EnterpriseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create an enterprise",
        Description = "Create an enterprise with root department and admin user")]
    public async Task<ActionResult<EnterpriseViewModel>> CreateEnterprise(
        [FromBody] EnterpriseCreateViewModel model)
    {
        var email = User.GetEmail();
        var result = await _mediator.Send(new CreateEnterprise(model, email));
        return result.Match<ActionResult>(Ok, e => BadRequest($"Enterprise id: {e.TakenId} has taken already"));
    }

    [HttpPost("{enterpriseId}/user")]
    [SwaggerOperation(Summary = "Create user for enterprise")]
    public async Task<ActionResult<UserViewModel>> CreateUser([FromRoute] [StringLength(50)] string enterpriseId,
        [FromBody] UserCreateViewModel model)
    {
        var result = await _mediator.Send(new CreateUser(model));

        return result.Match(Ok);
    }

    [HttpGet("{id}/idAvailability")]
    public async Task<ActionResult<bool>> GetEnterpriseIdAvailability([FromRoute] [StringLength(50)] string id)
    {
        var result = await _mediator.Send(new GetEnterpriseIdAvailability(id));
        return Ok(result);
    }
}