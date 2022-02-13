using EnterpriseAssistant.Application.Features.EnterpriseFeatures.ViewModels;
using EnterpriseAssistant.DataAccess;
using EnterpriseAssistant.DataAccess.Entities;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseAssistant.Application.Features.EnterpriseFeatures;

[ApiController]
[Route("enterprise")]
[ApiExplorerSettings(GroupName = "enterprise")]
public class CreateEnterpriseController : ControllerBase
{
    private readonly EnterpriseAssistantDbContext _db;

    public CreateEnterpriseController(EnterpriseAssistantDbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public async Task<ActionResult<EnterpriseViewModel>> CreateEnterprise([FromBody] EnterpriseCreateViewModel model)
    {
        var enterprise = _db.Enterprises.Add(model.Adapt<Enterprise>()).Entity;
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(CreateEnterprise), enterprise.Adapt<EnterpriseViewModel>());
    }
}