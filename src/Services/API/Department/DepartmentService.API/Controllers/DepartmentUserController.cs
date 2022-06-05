using System.ComponentModel.DataAnnotations;
using EnterpriseAssistant.Application.Shared;
using EnterpriseAssistant.DataAccess;
using EnterpriseAssistant.DataAccess.Entities;
using EnterpriseAssistant.DataAccess.Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using UserService.Contract.DataTransfer;

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
        [Range(1, long.MaxValue), FromBody] long userId)
    {
        var authContext = User.GetAuthContext();
        var readonlyContext = _factory.CreateReadOnlyContext(authContext);

        var department = await readonlyContext.DepartmentUsers
            .Where(du => du.UserId == authContext.UserId
                         && (du.DepartmentUserRole == DepartmentUserRole.Chief ||
                             du.DepartmentUserRole == DepartmentUserRole.Admin))
            .Include(du => du.Department)
            .Select(du => du.Department)
            .FirstOrDefaultAsync(d => d.IsSoftDeleted == false);

        if (department is null)
        {
            return NotFound($"Department with id {departmentId} not found");
        }

        var user = await readonlyContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user is null)
        {
            return NotFound($"User with id {userId} not found");
        }

        var departmentUser = new DepartmentUser
        {
            DepartmentId = department.Id,
            UserId = user.Id,
            DepartmentUserRole = DepartmentUserRole.User,
            EnterpriseId = authContext.EnterpriseId!
        };
        _db.DepartmentUsers.Add(departmentUser);
        await _db.SaveChangesAsync();
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers(
        [Range(1, long.MaxValue), FromRoute] long departmentId)
    {
        var readonlyContext = _factory.CreateReadOnlyContext(User.GetAuthContext());

        var users = await readonlyContext.DepartmentUsers
            .Where(du => du.DepartmentId == departmentId)
            .Include(du => du.User)
            .Select(du => du.User)
            .ToListAsync();

        return Ok(users);
    }
}