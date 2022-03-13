using EnterpriseAssistant.Application.Features.UserFeatures.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseAssistant.Application.Features.UserFeatures;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    
    
}