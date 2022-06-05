using DepartmentService.Contract.DataTransfer;
using EnterpriseAssistant.Application.Errors;
using EnterpriseAssistant.Application.Shared;
using MediatR;
using OneOf;

namespace DepartmentService.Contract.Commands;

public class CreateDepartment : IRequest<OneOf<DepartmentDto, IBadRequestError>>
{
    public CreateDepartment(DepartmentCreateDto model, AuthContext authContext)
    {
        Model = model;
        AuthContext = authContext;
    }

    public DepartmentCreateDto Model { get; }

    public AuthContext AuthContext { get; }
}