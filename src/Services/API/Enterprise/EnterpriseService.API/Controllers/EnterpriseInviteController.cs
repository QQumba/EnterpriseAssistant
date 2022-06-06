using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnterpriseAssistant.Application.Shared;
using EnterpriseAssistant.DataAccess;
using EnterpriseAssistant.DataAccess.Entities;
using EnterpriseService.API.Helpers;
using EnterpriseService.Contract.DataTransfer;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using UserService.Contract.DataTransfer;

namespace EnterpriseService.API.Controllers;

[ApiController]
[Authorize]
[Route("api/enterprise/invite")]
public class EnterpriseInviteController : ControllerBase
{
    private readonly EnterpriseAssistantDbContext _dbContext;

    public EnterpriseInviteController(EnterpriseAssistantDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // todo: check if the user is admin
    [HttpPost]
    [Authorize(Policy = "EnterpriseUser")]
    [SwaggerOperation(Summary = "Invite a user to join an enterprise",
        Description = "Invite a user to join an enterprise")]
    public async Task<ActionResult<InviteDto>> InviteUser([FromBody] InviteCreateDto inviteCreate)
    {
        var enterpriseId = User.GetEnterpriseId()!;
        var enterpriseUserAlreadyExists = await _dbContext.EnterpriseUsers
            .Include(eu => eu.Enterprise)
            .AnyAsync(eu => eu.EnterpriseId == enterpriseId &&
                            eu.IsSoftDeleted == false &&
                            eu.User.Email.Equals(inviteCreate));
        if (enterpriseUserAlreadyExists)
        {
            return BadRequest(
                $"User with email {inviteCreate.UserEmail} is already in the enterprise with id {enterpriseId}");
        }

        var inviteAlreadyExists = await _dbContext.Invites.AnyAsync(i =>
            i.UserEmail.Equals(inviteCreate.UserEmail) &&
            i.IsSoftDeleted == false &&
            i.Status == InviteStatus.Pending);
        if (inviteAlreadyExists)
        {
            return BadRequest($"User with email {inviteCreate.UserEmail} already invited");
        }

        var user = await _dbContext.Users.FirstOrDefaultAsync(u =>
            u.IsSoftDeleted == false && u.Email.Equals(inviteCreate.UserEmail));

        var invite = inviteCreate.Adapt<Invite>();
        if (user is not null)
        {
            invite.UserId = user.Id;
        }

        invite.EnterpriseId = enterpriseId;
        var createdInvite = _dbContext.Invites.Add(invite).Entity;
        await _dbContext.SaveChangesAsync();
        return Ok(createdInvite.Adapt<InviteDto>());
    }

    // todo: check if the user is admin
    [Authorize(Policy = "EnterpriseUser")]
    [HttpGet]
    [SwaggerOperation(Summary = "Get all invites for current enterprise")]
    public async Task<ActionResult<IEnumerable<InviteDto>>> GetInvites()
    {
        var invites = await _dbContext.Invites
            .Include(i => i.Enterprise)
            .Where(i => i.EnterpriseId == User.GetEnterpriseId() &&
                        i.IsSoftDeleted == false)
            .Select(i => new InviteDto(i)
            {
                EnterpriseDisplayedName = i.Enterprise!.DisplayedName
            })
            .ToListAsync();

        return Ok(invites.Adapt<List<InviteDto>>());
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Accept an invite")]
    public async Task<ActionResult> SubmitInvite([FromBody] InviteSubmitDto inviteSubmit)
    {
        var authContext = User.GetAuthContext();
        var invite = await _dbContext.Invites
            .FirstOrDefaultAsync(i =>
                i.UserId == authContext.UserId &&
                i.EnterpriseId.Equals(inviteSubmit.EnterpriseId) &&
                i.Status == InviteStatus.Pending &&
                i.IsSoftDeleted == false);
        if (invite is null)
        {
            return NotFound($"Invite to enterprise with id {inviteSubmit.EnterpriseId} not found");
        }

        var loginAlreadyTaken = await _dbContext.EnterpriseUsers
            .IsEnterpriseUserExists(invite.EnterpriseId, inviteSubmit.Login);
        if (loginAlreadyTaken)
        {
            return BadRequest($"Login {inviteSubmit.Login} is already taken");
        }

        var enterpriseUser = invite.Accept(inviteSubmit.Login);
        _dbContext.EnterpriseUsers.Add(enterpriseUser!);
        _dbContext.Entry(invite).Property(i => i.Status).IsModified = true;
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
}