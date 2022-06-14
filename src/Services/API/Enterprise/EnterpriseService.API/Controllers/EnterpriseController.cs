using System.ComponentModel.DataAnnotations;
using System.Threading;
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

[Authorize]
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
        var context = User.GetAuthContext();
        var result = await _mediator.Send(new CreateEnterprise(model, context));
        return result.Match<ActionResult>(Ok, e => BadRequest($"Enterprise id: {e.TakenId} has taken already"));
    }

    [HttpGet("exists")]
    public async Task<ActionResult<bool>> IsEnterpriseExists([FromQuery] [Required, StringLength(50)] string id)
    {
        var result = await _mediator.Send(new IsEnterpriseExists(id));
        return Ok(result);
    }

    [HttpGet("{enterpriseId}/user/exists")]
    public async Task<ActionResult<bool>> IsUserExists([FromRoute, Required] string enterpriseId,
        [Required, FromQuery] string login)
    {
        var userInvited = true;
        if (userInvited == false)
        {
            return NotFound($"Enterprise with id {enterpriseId} not found");
        }

        var result = await _mediator.Send(new CheckIfEnterpriseUserExists(enterpriseId, login));
        return Ok(result);
    }
}