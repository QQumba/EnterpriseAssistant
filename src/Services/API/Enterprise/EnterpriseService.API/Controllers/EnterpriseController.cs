using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using EnterpriseAssistant.Application.Shared;
using EnterpriseService.API.Commands;
using EnterpriseService.Contract.DataTransfer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UserService.Contract.DataTransfer;

namespace EnterpriseService.API.Controllers;

// todo remove anonymous attribute
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
    public async Task<ActionResult<EnterpriseDto>> CreateEnterprise([FromBody] EnterpriseCreateDto model)
    {
        var email = User.GetEmail();
        var result = await _mediator.Send(new CreateEnterprise(model, email));
        return result.Match<ActionResult>(Ok, e => BadRequest($"Enterprise id: {e.TakenId} has taken already"));
    }

    [HttpPost("user")]
    [SwaggerOperation(Summary = "Create user for enterprise")]
    public async Task<ActionResult<UserDto>> CreateUser(
        [FromBody] UserCreateDto model)
    {
        var enterpriseId = User.GetEnterpriseId();
        var result = await _mediator.Send(new CreateEnterpriseUser(model, enterpriseId));

        return result.Match<ActionResult>(Ok, e => NotFound(e.Message), e => BadRequest(e.Message));
    }

    [HttpGet("exists")]
    public async Task<ActionResult<bool>> GetEnterpriseIdAvailability([FromQuery] [Required, StringLength(50)] string id)
    {
        var result = await _mediator.Send(new GetEnterpriseIdAvailability(id));
        return Ok(result);
    }

    [HttpGet("user/exists")]
    public async Task<ActionResult<bool>> IsUserExists([Required] [FromQuery] string login)
    {
        var enterpriseId = User.GetEnterpriseId();
        var result = await _mediator.Send(new GetEnterpriseUserExistence(enterpriseId, login));
        return Ok(result);
    }
}