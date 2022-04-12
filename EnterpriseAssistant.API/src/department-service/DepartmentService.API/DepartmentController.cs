using System.ComponentModel.DataAnnotations;
using DepartmentService.API.Commands;
using EnterpriseAssistant.Application.Features.DepartmentFeatures.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UserService.Contract.ViewModels;

namespace DepartmentService.API;

[Authorize]
[ApiController]
[Route("api/department")]
public class DepartmentController : ControllerBase
{
    private readonly IMediator _mediator;

    public DepartmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{departmentId:long}")]
    public async Task<ActionResult<IEnumerable<DepartmentViewModel>>> GetDepartmentById(
        [Range(1, long.MaxValue), FromRoute] long departmentId,
        [FromQuery] bool includeChild = false)
    {
        var result = await _mediator.Send(new GetDepartmentById(departmentId, includeChild));

        return result.Match<ActionResult>(Ok, x => NotFound());
    }

    // doesn't seem useful as a user can be part of multiple departments
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

    [HttpPost]
    public async Task<ActionResult<DepartmentViewModel>> CreateDepartment([FromBody] DepartmentCreateViewModel model)
    {
        var result = await _mediator.Send(new CreateDepartment(model));
        return result.Match(d => CreatedAtAction(nameof(CreateDepartment), d));
    }

    [HttpGet("users/{departmentId:long}")]
    public async Task<ActionResult<IEnumerable<UserViewModel>>> GetDepartmentUsers(
        [Range(1, long.MaxValue), FromRoute] long departmentId)
    {
        throw new NotImplementedException();
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