using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UserService.API.Commands;
using UserService.Contract.DataTransfer;

namespace UserService.API;

[ApiController]
[Route("api/user")]
[AllowAnonymous]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a new user")]
    public async Task<ActionResult<UserDto>> CreateUser([FromBody] UserCreateDto model)
    {
        var result = await _mediator.Send(new CreateUser(model));
        return result.Match<ActionResult>(Ok,e => BadRequest($"Email already taken, email: {e.Email}"));
    }

    [HttpGet("exists")]
    [SwaggerOperation(Summary = "Check if user already exists")]
    public async Task<ActionResult<bool>> GetUserEmailAvailability([FromQuery] string email)
    {
        if (email.Contains('@') == false)
        {
            return BadRequest("Email is not valid");
        }

        var result = await _mediator.Send(new CheckIfUserExists(email));
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
    {
        var result = await _mediator.Send(new GetAllUsersCommand());
        return Ok(result);
    }
}