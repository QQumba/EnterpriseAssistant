using System.ComponentModel.DataAnnotations;
using DepartmentService.Contract.DataTransfer;
using EnterpriseAssistant.Application.Shared;
using EnterpriseAssistant.DataAccess;
using EnterpriseAssistant.DataAccess.Entities;
using EnterpriseAssistant.DataAccess.Entities.Enums;
using EnterpriseAssistant.DataAccess.Extensions;
using EnterpriseService.Contract.DataTransfer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using TaskTrackingService.API.Controllers;
using UserService.Contract.DataTransfer;
using Task = System.Threading.Tasks.Task;

namespace DepartmentService.API.Controllers;

[ApiController]
[Route("api/department/{departmentId:long}/user")]
[Authorize]
public class DepartmentUserController : ControllerBase
{
    private readonly DbContextFactory _factory;
    private readonly EnterpriseAssistantDbContext _db;

    public DepartmentUserController(DbContextFactory factory)
    {
        _factory = factory;
        _db = factory.Create();
    }

    [HttpPost]
    [SwaggerResponse(204, "User added to department")]
    [SwaggerResponse(404, "Department or user not found")]
    [SwaggerOperation(Summary = "Add user to department")]
    public async Task<ActionResult> AddUser([Range(1, long.MaxValue), FromRoute] long departmentId,
        [FromBody] AssigneeDto user)
    {
        var authContext = User.GetAuthContext();
        var readonlyContext = _factory.CreateReadOnlyContext(authContext);

        var isUserDepartmentAdmin = await _db.IsUserDepartmentAdmin(authContext, departmentId);

        if (isUserDepartmentAdmin == false)
        {
            return NotFound($"Department with id {departmentId} not found");
        }

        var foundUser = await readonlyContext.EnterpriseUsers
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Login == user.Login);

        if (foundUser is null)
        {
            return NotFound($"User with login {user.Login} not found");
        }

        // var isUserMember = await readonlyContext.DepartmentUsers
        //     .AnyAsync(du => du.UserId == foundUser.Id &&
        //                     du.DepartmentId == departmentId);

        var departmentUser = new DepartmentUser
        {
            DepartmentId = departmentId,
            UserId = foundUser.Id,
            DepartmentUserRole = DepartmentUserRole.Admin,
            EnterpriseId = authContext.EnterpriseId!,
            DisplayAsMember = true
        };
        _db.DepartmentUsers.Add(departmentUser);
        await _db.SaveChangesAsync();
        return Ok();
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Read all enterprise users")]
    public async Task<ActionResult<IEnumerable<EnterpriseUserDto>>> GetUsers(
        [Range(1, long.MaxValue), FromRoute] long departmentId)
    {
        var readonlyContext = _factory.CreateReadOnlyContext(User.GetAuthContext());
        var authContext = User.GetAuthContext();
        var canViewHiddenUsers = await _db.IsUserDepartmentAdmin(authContext, departmentId);

        var users = await (from user in readonlyContext.Users
            join enterpriseUser in readonlyContext.EnterpriseUsers on user.Id equals enterpriseUser.UserId
            join departmentUser in readonlyContext.DepartmentUsers on user.Id equals departmentUser.UserId
            where departmentUser.DepartmentId == departmentId &&
                  enterpriseUser.EnterpriseId == authContext.EnterpriseId &&
                  (departmentUser.DisplayAsMember || canViewHiddenUsers)
            select new EnterpriseUserDto
            {
                Id = user.Id,
                Login = enterpriseUser.Login,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            }).ToListAsync();

        return Ok(users);
    }

    [HttpDelete]
    [SwaggerOperation(Summary = "Exclude user from department")]
    public async Task<ActionResult> ExcludeUser([FromRoute] long departmentId, [FromBody] long userId)
    {
        var authContext = User.GetAuthContext();
        var isUserAdmin = await _db.IsUserDepartmentAdmin(authContext, departmentId);
        if (isUserAdmin == false)
        {
            return NotFound($"Department with id {departmentId} not found");
        }

        if (userId == authContext.UserId)
        {
            var hasDepartmentAdminsExceptUser = await _db.DepartmentUsers
                .Where(du => du.EnterpriseId.Equals(authContext.EnterpriseId) &&
                             du.DepartmentId == departmentId &&
                             du.DepartmentUserRole == DepartmentUserRole.Admin &&
                             du.UserId != authContext.UserId)
                .AnyAsync();

            if (hasDepartmentAdminsExceptUser == false)
            {
                return BadRequest("No other administrators assigned to department");
            }
        }

        await _db.DepartmentUsers
            .Where(du => du.EnterpriseId == authContext.EnterpriseId &&
                         du.DepartmentId == departmentId &&
                         du.UserId == userId)
            .DeleteFromQueryAsync();

        return NoContent();
    }

    [HttpPut("role")]
    [SwaggerOperation(Summary = "Update department user role")]
    public async Task<ActionResult> UpdateRole([FromRoute] long departmentId, DepartmentUserUpdateDto departmentUser)
    {
        var authContext = User.GetAuthContext();
        if (departmentUser.UserId == authContext.UserId)
        {
            return BadRequest("You can't update your role");
        }

        var isUserAdmin = await _db.IsUserDepartmentAdmin(authContext, departmentId);
        if (isUserAdmin == false)
        {
            return NotFound($"Department with id {departmentId} not found");
        }

        var user = await _db.DepartmentUsers
            .FirstOrDefaultAsync(du => du.EnterpriseId.Equals(authContext.EnterpriseId) &&
                                       du.DepartmentId == departmentId &&
                                       du.UserId == departmentUser.UserId);

        if (user is null)
        {
            return NotFound($"User with id {departmentUser.UserId} not found");
        }

        user.DepartmentUserRole = departmentUser.Role;
        _db.Entry(user).Property(du => du.DepartmentUserRole).IsModified = true;
        await _db.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<EnterpriseUserDto>>> SearchUsers([FromRoute] long departmentId,
        [Required, FromQuery] string query)
    {
        var authContext = User.GetAuthContext();
        var readOnlyContext = _factory.CreateReadOnlyContext(authContext);

        var users = await readOnlyContext.DepartmentUsers
            .Include(x => x.User)
            .Where(x => x.User.IsSoftDeleted == false &&
                        x.DepartmentId == departmentId &&
                        x.User.FirstName.ToLower().StartsWith(query) ||
                        (x.User.LastName != null && x.User.LastName.ToLower().StartsWith(query)))
            .Select(eu => new EnterpriseUserDto
            {
                Id = eu.UserId,
                Email = eu.User.Email,
                FirstName = eu.User.FirstName,
                LastName = eu.User.LastName
            })
            .ToListAsync();

        return users;
    }
}