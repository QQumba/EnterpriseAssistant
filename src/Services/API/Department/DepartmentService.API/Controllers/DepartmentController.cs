using System.ComponentModel.DataAnnotations;
using DepartmentService.API.Commands;
using DepartmentService.Contract.Commands;
using DepartmentService.Contract.DataTransfer;
using EnterpriseAssistant.Application.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DepartmentService.API.Controllers;

[AllowAnonymous]
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
    public async Task<ActionResult<DepartmentDto>> GetDepartmentById(
        [Range(1, long.MaxValue), FromRoute] long departmentId,
        [FromQuery] bool includeChild = false)
    {
        var authContext = User.GetAuthContext();
        var result = await _mediator.Send(new GetDepartmentById(departmentId, authContext, includeChild));
        return result.Match<ActionResult>(Ok, e => NotFound(e.Message));
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get user departments", Description = "Get user departments")]
    public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetDepartments()
    {
        var authContext = User.GetAuthContext();
        var result = await _mediator.Send(new GetUserDepartments(authContext));
        return result.Match<ActionResult>(Ok, e => NotFound(e.Message));
    }

    [HttpGet("{departmentId:long}/child")]
    public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetChildDepartments(
        [Range(1, long.MaxValue), FromRoute] long departmentId)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create department", Description = "Create department")]
    public async Task<ActionResult<DepartmentDto>> CreateDepartment([FromBody] DepartmentCreateDto model)
    {
        var authContext = User.GetAuthContext();
        var departmentAlreadyExists = await _mediator.Send(new CheckIfDepartmentExists(model.Name, authContext));
        if (departmentAlreadyExists)
        {
            return BadRequest($"Department with name {model.Name} already exists");
        }

        var result = await _mediator.Send(new CreateDepartment(model, authContext));
        return result.Match<ActionResult>(
            d => CreatedAtAction(nameof(CreateDepartment), d),
            e => BadRequest(e.Message));
    }

    [HttpDelete("{departmentId:long}")]
    [SwaggerResponse(204, "Department deleted")]
    [SwaggerResponse(404, "Department not found")]
    [SwaggerOperation(Summary = "Delete department")]
    public async Task<ActionResult> DeleteDepartment([Range(1, long.MaxValue), FromRoute] long departmentId)
    {
        var authContext = User.GetAuthContext();
        var response = await _mediator.Send(new DeleteDepartment(departmentId, authContext));
        return response.Match<ActionResult>(x => NoContent(), e => NotFound(e.Message));
    }
}