using System.Reflection.Metadata;
using EnterpriseAssistant.Application.Features.EnterpriseFeatures.Commands;
using EnterpriseAssistant.Application.Features.EnterpriseFeatures.ViewModels;
using EnterpriseAssistant.Application.Features.UserFeatures.ViewModels;
using EnterpriseAssistant.DataAccess.Entities;
using Mapster;
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

    [HttpPost("{enterpriseId:guid}/user")]
    public async Task<ActionResult<UserViewModel>> CreateUser([FromRoute] Guid enterpriseId,
        [FromBody] UserCreateViewModel model)
    {
        var result = await _mediator.Send(new CreateEnterpriseUser());

        return Ok();
    }
    
    [HttpPost]
    public async Task<ActionResult<EnterpriseViewModel>> CreateEnterprise([FromBody] EnterpriseCreateViewModel model)
    {
        var result = await _mediator.Send(new CreateEnterprise(model));


        return result.Match(Ok);
    }
}