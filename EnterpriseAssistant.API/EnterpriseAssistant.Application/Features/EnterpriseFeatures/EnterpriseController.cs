using EnterpriseAssistant.Application.Features.EnterpriseFeatures.Commands;
using EnterpriseAssistant.Application.Features.EnterpriseFeatures.ViewModels;
using EnterpriseAssistant.Application.Features.UserFeatures.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseAssistant.Application.Features.EnterpriseFeatures;

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
        [FromBody] EnterpriseInitializeViewModel model)
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
}