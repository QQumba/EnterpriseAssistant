using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnterpriseAssistant.Application.Shared;
using EnterpriseAssistant.DataAccess;
using EnterpriseService.Contract.DataTransfer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace EnterpriseService.API.Controllers;

[ApiController]
[Route("api/enterprise/user")]
[Authorize(Policy = "EnterpriseUser")]
public class EnterpriseUserController : ControllerBase
{
    private readonly DbContextFactory _factory;

    public EnterpriseUserController(DbContextFactory factory)
    {
        _factory = factory;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all users of the enterprise")]
    public async Task<ActionResult<IEnumerable<EnterpriseUserDto>>> GetUsers()
    {
        var authContext = User.GetAuthContext();
        var readOnlyContext = _factory.CreateReadOnlyContext(authContext);
        var users = await readOnlyContext.EnterpriseUsers
            .Include(eu => eu.User)
            .Where(eu => eu.User.IsSoftDeleted == false)
            .Select(eu => new EnterpriseUserDto
            {
                Login = eu.Login,
                Email = eu.User.Email,
                FirstName = eu.User.FirstName,
                LastName = eu.User.FirstName
            })
            .ToListAsync();

        return Ok(users);
    }
}