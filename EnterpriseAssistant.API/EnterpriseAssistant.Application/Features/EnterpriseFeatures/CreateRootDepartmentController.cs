using EnterpriseAssistant.Application.Features.DepartmentFeatures.ViewModels;
using EnterpriseAssistant.DataAccess;
using EnterpriseAssistant.DataAccess.Entities;
using EnterpriseAssistant.DataAccess.Entities.Enums;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseAssistant.Application.Features.EnterpriseFeatures;

[ApiController]
[Route("enterprise")]
[ApiExplorerSettings(GroupName = "enterprise")]
public class CreateRootDepartmentController : ControllerBase
{
    private readonly EnterpriseAssistantDbContext _db;

    public CreateRootDepartmentController(EnterpriseAssistantDbContext db)
    {
        _db = db;
    }
    
    // todo: ftw this should be created with enterprise
    [HttpPost("{enterpriseId:guid}/department/root/")]
    public async Task<ActionResult<DepartmentViewModel>> CreateDepartment([FromRoute] Guid enterpriseId,
        [FromBody] DepartmentCreateViewModel model)
    {
        var enterprise = await _db.Enterprises.FirstOrDefaultAsync(e => e.Id == enterpriseId);
        if (enterprise is null)
        {
            return NotFound("Enterprise not found");
        }

        var existedRootDepartment = await _db.Departments.Where(d =>
            d.EnterpriseId.Equals(enterpriseId) && d.DepartmentType == DepartmentType.Root).FirstOrDefaultAsync();
        if (existedRootDepartment is not null)
        {
            return BadRequest("Root department for current enterprise already exists");
        }

        var department = model.Adapt<Department>();
        department.DepartmentType = DepartmentType.Root;

        var rootDepartment = _db.Departments.Add(department).Entity;
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(CreateDepartment), rootDepartment);
    }
}