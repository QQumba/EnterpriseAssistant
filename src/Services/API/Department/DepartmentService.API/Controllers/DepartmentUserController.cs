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
        [FromBody] DepartmentUserCreateDto user)
    {
        var authContext = User.GetAuthContext();
        var readonlyContext = _factory.CreateReadOnlyContext(authContext);

        var isUserDepartmentAdmin = await _db.IsUserDepartmentAdmin(authContext, departmentId);

        if (isUserDepartmentAdmin == false)
        {
            return NotFound($"Department with id {departmentId} not found");
        }

        var isUserExists = await readonlyContext.EnterpriseUsers
            .AnyAsync(eu => eu.Id == user.UserId &&
                            eu.EnterpriseId.Equals(authContext.EnterpriseId));
        if (isUserExists == false)
        {
            return NotFound($"User with id {user.UserId} not found");
        }

        var isUserMember = await readonlyContext.DepartmentUsers
            .AnyAsync(du => du.UserId == user.UserId &&
                            du.DepartmentId == departmentId);

        var departmentUser = new DepartmentUser
        {
            DepartmentId = departmentId,
            UserId = user.UserId,
            DepartmentUserRole = DepartmentUserRole.User,
            EnterpriseId = authContext.EnterpriseId!
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

        var users = await (from user in readonlyContext.Users
            join enterpriseUser in readonlyContext.EnterpriseUsers on user.Id equals enterpriseUser.UserId
            join departmentUser in readonlyContext.DepartmentUsers on user.Id equals departmentUser.UserId
            where departmentUser.DepartmentId == departmentId &&
                  enterpriseUser.EnterpriseId == authContext.EnterpriseId &&
                  departmentUser.DisplayAsMember
            select new EnterpriseUserDto
            {
                UserId = user.Id,
                Login = enterpriseUser.Login,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            }).ToListAsync();

        return Ok(users);
    }

    [HttpPut("exclude")]
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

    [HttpPut]
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
}