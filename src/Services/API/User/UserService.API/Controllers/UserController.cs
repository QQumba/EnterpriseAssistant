using EnterpriseAssistant.Application.Shared;
using EnterpriseAssistant.DataAccess;
using EnterpriseAssistant.DataAccess.Entities;
using EnterpriseService.Contract.DataTransfer;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using UserService.API.Commands;
using UserService.Contract.DataTransfer;

namespace UserService.API.Controllers;

[ApiController]
[Route("api/user")]
[AllowAnonymous]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly EnterpriseAssistantDbContext _dbContext;

    public UserController(IMediator mediator, EnterpriseAssistantDbContext dbContext)
    {
        _mediator = mediator;
        _dbContext = dbContext;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a new user")]
    public async Task<ActionResult<UserDto>> CreateUser([FromBody] UserCreateDto model)
    {
        var result = await _mediator.Send(new CreateUser(model));
        return result.Match<ActionResult>(Ok, e => BadRequest($"Email already taken, email: {e.Email}"));
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

    [Authorize]
    [HttpGet("details")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUserDetails()
    {
        var userId = User.GetUserId();
        var user = await _dbContext.Users.FirstAsync(u => u.Id == userId);
        return Ok(user.Adapt<UserDto>());
    }

    [HttpGet("enterprise-invites")]
    [Authorize]
    [SwaggerOperation(Summary = "Get pending invites", Description = "Get pending invites for current user")]
    public async Task<ActionResult<IEnumerable<InviteDto>>> GetPendingInvites()
    {
        var userId = User.GetUserId();
        var invites = await _dbContext.Invites
            .Where(i => i.UserId == userId &&
                        i.Status == InviteStatus.Pending &&
                        i.IsSoftDeleted == false)
            .Include(i => i.Enterprise)
            .Select(i => new InviteDto(i)
            {
                EnterpriseDisplayedName = i.Enterprise!.DisplayedName
            })
            .ToListAsync();

        return Ok(invites);
    }
}