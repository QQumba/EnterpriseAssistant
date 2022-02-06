using EnterpriseAssistant.Application.Features.DepartmentFeatures.ViewModels;
using EnterpriseAssistant.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseAssistant.Application.Features.DepartmentFeatures;

[ApiController]
[Route("api/department/")]
public class GetDepartmentController : ControllerBase
{
    private readonly EnterpriseAssistantDbContext _db;

    public GetDepartmentController(EnterpriseAssistantDbContext db)
    {
        _db = db;
    }

    [HttpGet("{departmentId:long}")]
    public async Task<ActionResult<IEnumerable<DepartmentViewModel>>> GetDepartmentById([FromRoute] long departmentId,
        [FromQuery] bool includeChild = false)
    {
        throw new NotImplementedException();
    }

    [HttpGet("my")]
    public async Task<ActionResult<IEnumerable<DepartmentViewModel>>> GetUserDepartments(
        [FromQuery] bool includeChild = false)
    {
        throw new NotImplementedException();
    }

    [HttpGet("subordinate/{departmentId:long}")]
    public async Task<ActionResult<IEnumerable<DepartmentViewModel>>> GetSubordinateDepartments(
        [FromRoute] long departmentId)
    {
        throw new NotImplementedException();
    }

    [HttpGet("my/subordinate")]
    public async Task<ActionResult<IEnumerable<DepartmentViewModel>>> GetUserSubordinateDepartments()
    {
        throw new NotImplementedException();
    }
}