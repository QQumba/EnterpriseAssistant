using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EnterpriseAssistant.Application.Features.DepartmentFeatures;

[ApiController]
[Route("api/department")]
[ApiExplorerSettings(GroupName = "department")]
public class AddUserController : ControllerBase
{
    public AddUserController()
    {
    }

    [HttpPost("{departmentId:long}/add/user")]
    [SwaggerResponse(204, "User added to department")]
    [SwaggerResponse(404, "Department or user not found")]
    [SwaggerOperation(Summary = "Add user to department")]
    public async Task<ActionResult> AddUser([Range(1, long.MaxValue), FromRoute] long departmentId,
        [Range(1, long.MaxValue), FromBody] long userId)
    {
        throw new NotImplementedException();
    }
}