using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UserService.API.Commands;
using UserService.Contract.ViewModels;

namespace UserService.API;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<ManagedUserViewModel>> CreateUser([FromBody] ManagedUserCreateViewModel model)
    {
        var result = await _mediator.Send(new CreateManagedUserCommand(model));
        return result.Match<ActionResult>(Ok,e => BadRequest($"Email already taken, email: {e.Email}"));
    }

    [HttpGet("emailAvailability")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Check if email is available")]
    public async Task<ActionResult<bool>> GetManagedUserEmailAvailability([FromQuery] string email)
    {
        if (email.Contains('@') == false)
        {
            // TODO: wrap error in a generic response
            return BadRequest("Email is not valid");
        }

        var result = await _mediator.Send(new GetManagedUserEmailAvailability(email));
        return Ok(result);
    }
    
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<ManagedUserViewModel>>> GetAllUsers()
    {
        var result = await _mediator.Send(new GetAllManagedUsersCommand());
        return Ok(result);
    }
}