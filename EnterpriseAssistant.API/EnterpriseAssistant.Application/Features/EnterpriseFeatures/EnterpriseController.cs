using EnterpriseAssistant.Application.Features.DepartmentFeatures.Commands;
using EnterpriseAssistant.Application.Features.DepartmentFeatures.ViewModels;
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

    [HttpPost("{enterpriseId:guid}/user")]
    public async Task<ActionResult<UserViewModel>> CreateUser([FromRoute] Guid enterpriseId,
        [FromBody] UserCreateViewModel model)
    {
        var result = await _mediator.Send(new CreateUser(model));

        return result.Match(Ok);
    }

    [HttpPost("transaction/init")]
    public async Task<ActionResult<IEnterpriseCreateTransaction>> InitiateEnterpriseCreateTransaction()
    {
        var result = await _mediator.Send(new InitiateEnterpriseCreateTransaction());

        return result.Match(Ok);
    }

    [HttpPost("transaction/{transactionId:guid}/user")]
    public async Task<ActionResult<EnterpriseCreateTransaction>> AddUserToTransaction(
        [FromRoute] Guid transactionId, [FromBody] UserCreateViewModel model)
    {
        var result = await _mediator.Send(new CreateUser(model));

        return result.Match(Ok);
    }

    [HttpPost("transaction/{transactionId:guid}/department")]
    public async Task<ActionResult<EnterpriseCreateTransaction>> AddDepartmentToTransaction(
        [FromBody] DepartmentCreateViewModel model)
    {
        var result = await _mediator.Send(new CreateDepartment(model));

        return result.Match(Ok);
    }
}