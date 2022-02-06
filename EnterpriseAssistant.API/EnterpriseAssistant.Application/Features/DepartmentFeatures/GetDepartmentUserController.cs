using EnterpriseAssistant.Application.Features.UserFeatures.ViewModels;
using EnterpriseAssistant.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseAssistant.Application.Features.DepartmentFeatures;

[ApiController]
[Route("api/department")]
public class GetDepartmentUserController : ControllerBase
{
    private readonly EnterpriseAssistantDbContext _db;

    public GetDepartmentUserController(EnterpriseAssistantDbContext db)
    {
        _db = db;
    }

    [HttpGet("users/{departmentId:long}")]
    public async Task<ActionResult<IEnumerable<UserViewModel>>> GetDepartmentUsers([FromRoute] long departmentId)
    {
        throw new NotImplementedException();
    }
}