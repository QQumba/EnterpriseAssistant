using System.ComponentModel.DataAnnotations;
using EnterpriseService.API.Commands;
using EnterpriseService.Contract.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OneOf;
using UserService.Contract.ViewModels;

namespace EnterpriseService.API;

[ApiController]
[Route("api/enterprise")]
public class EnterpriseController : ControllerBase
{
    private readonly IMediator _mediator;

    public EnterpriseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<ActionResult<EnterpriseViewModel>> InitiateEnterpriseCreateTransaction(
        [FromBody] EnterpriseCreateViewModel model)
    {
        var result = await _mediator.Send(new InitializeEnterprise(model));
        return result.Match(Ok);
    }

    [HttpPost("{enterpriseId:guid}/user")]
    public async Task<ActionResult<UserViewModel>> CreateUser([FromRoute] Guid enterpriseId,
        [FromBody] UserCreateViewModel model)
    {
        var result = await _mediator.Send(new CreateUser(model));

        return result.Match(Ok);
    }

    [HttpGet("nameAvailability/{name}")]
    public async Task<ActionResult<bool>> GetEnterpriseNameAvailability([FromQuery] [StringLength(50)] string name)
    {
        var result = await _mediator.Send(new GetEnterpriseNameAvailability(name));
        return Ok(result);
    }
}