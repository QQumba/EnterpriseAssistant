using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EnterpriseAssistant.Application.Shared;
using EnterpriseAssistant.DataAccess;
using EnterpriseService.API.Commands;
using EnterpriseService.Contract.DataTransfer;
using MediatR;
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
    private readonly IMediator _mediator;

    public EnterpriseUserController(DbContextFactory factory, IMediator mediator)
    {
        _factory = factory;
        _mediator = mediator;
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
                UserId = eu.UserId,
                Login = eu.Login,
                Email = eu.User.Email,
                FirstName = eu.User.FirstName,
                LastName = eu.User.FirstName
            })
            .ToListAsync();

        return Ok(users);
    }
    
    [HttpGet("exists")]
    public async Task<ActionResult<bool>> IsUserExists([Required] [FromQuery] string login)
    {
        var enterpriseId = User.GetEnterpriseId();
        var result = await _mediator.Send(new CheckIfEnterpriseUserExists(enterpriseId!, login));
        return Ok(result);
    }
    
}