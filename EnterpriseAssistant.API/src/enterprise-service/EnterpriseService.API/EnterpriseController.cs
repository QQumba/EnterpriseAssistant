using System.ComponentModel.DataAnnotations;
using EnterpriseService.API.Commands;
using EnterpriseService.Contract.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<ActionResult<EnterpriseViewModel>> CreateEnterprise(
        [FromBody] EnterpriseCreateViewModel model)
    {
        var result = await _mediator.Send(new CreateEnterprise(model));
        return result.Match(Ok);
    }

    [HttpPost("{enterpriseId}/user")]
    public async Task<ActionResult<UserViewModel>> CreateUser([FromRoute] [StringLength(50)] string enterpriseId,
        [FromBody] UserCreateViewModel model)
    {
        var result = await _mediator.Send(new CreateUser(model));

        return result.Match(Ok);
    }

    [HttpGet("idAvailability/{id}")]
    public async Task<ActionResult<bool>> GetEnterpriseIdAvailability([FromRoute] [StringLength(50)] string id)
    {
        var result = await _mediator.Send(new GetEnterpriseIdAvailability(id));
        return Ok(result);
    }
}