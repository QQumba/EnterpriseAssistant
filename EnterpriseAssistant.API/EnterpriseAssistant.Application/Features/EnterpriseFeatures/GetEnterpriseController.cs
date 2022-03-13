using EnterpriseAssistant.Application.Features.EnterpriseFeatures.ViewModels;
using EnterpriseAssistant.DataAccess;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseAssistant.Application.Features.EnterpriseFeatures;

[ApiController]
[Route("enterprise")]
[ApiExplorerSettings(GroupName = "enterprise")]
public class GetEnterpriseController : ControllerBase
{
    private readonly EnterpriseAssistantDbContext _db;

    public GetEnterpriseController(EnterpriseAssistantDbContext db)
    {
        _db = db;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<EnterpriseViewModel>>> GetAllEnterprises()
    {
        var enterprises = await _db.Enterprises.ToListAsync();
        return Ok(enterprises.Select(e => e.Adapt<EnterpriseViewModel>()));
    }
}