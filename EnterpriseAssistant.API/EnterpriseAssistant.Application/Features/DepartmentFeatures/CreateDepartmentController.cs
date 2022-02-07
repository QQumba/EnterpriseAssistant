using EnterpriseAssistant.Application.Features.DepartmentFeatures.ViewModels;
using EnterpriseAssistant.DataAccess.Entities;
using EnterpriseAssistant.DataAccess;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseAssistant.Application.Features.DepartmentFeatures;

[ApiController]
[Route("api/department/create")]
[ApiExplorerSettings(GroupName = "department")]
public class CreateDepartmentController : ControllerBase
{
    private readonly EnterpriseAssistantDbContext _db;

    public CreateDepartmentController(EnterpriseAssistantDbContext db)
    {
        _db = db;
    }
    
    [HttpPost]
    public async Task<ActionResult<DepartmentViewModel>> CreateDepartment([FromBody] DepartmentCreateViewModel model)
    {
        var department = _db.Departments.Add(model.Adapt<Department>()).Entity;
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(CreateDepartment), department.Adapt<DepartmentViewModel>());
    }
}