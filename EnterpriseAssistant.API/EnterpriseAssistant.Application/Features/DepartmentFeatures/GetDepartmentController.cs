using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using System.Security.Cryptography.X509Certificates;
using EnterpriseAssistant.Application.Features.DepartmentFeatures.ViewModels;
using EnterpriseAssistant.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseAssistant.Application.Features.DepartmentFeatures;

[ApiController]
[Route("api/department/")]
[ApiExplorerSettings(GroupName = "department")]
public class GetDepartmentController : ControllerBase
{
    private readonly EnterpriseAssistantDbContext _db;

    public GetDepartmentController(EnterpriseAssistantDbContext db)
    {
        _db = db;
    }

    [HttpGet("{departmentId:long}")]
    public async Task<ActionResult<IEnumerable<DepartmentViewModel>>> GetDepartmentById(
        [Range(1, long.MaxValue), FromRoute] long departmentId,
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
        [Range(1, long.MaxValue), FromRoute] long departmentId)
    {
        throw new NotImplementedException();
    }

    [HttpGet("my/subordinate")]
    public async Task<ActionResult<IEnumerable<DepartmentViewModel>>> GetUserSubordinateDepartments()
    {
        throw new NotImplementedException();
    }

    [HttpGet("test/recursive")]
    public async Task<ActionResult<DepartmentViewModel>> GetRecursiveDepartments(
        [Range(0, int.MaxValue), FromQuery] int nestingLevel)
    {
        var root = new DepartmentViewModel()
        {
            Id = 0,
            Name = "root"
        };

        var lastChild = root;
        for (var i = 1; i < nestingLevel + 1; i++)
        {
            var department = new DepartmentViewModel()
            {
                Id = i,
                Name = $"name {i}",
                ParentDepartmentId = lastChild.Id
            };

            lastChild.ChildDepartments.Add(department);
            
            lastChild = department;
        }

        return Ok(root);
    }
}