using DepartmentService.Contract.DataTransfer;
using EnterpriseAssistant.Application.Errors;
using EnterpriseAssistant.Application.Shared;
using MediatR;
using OneOf;

namespace DepartmentService.Contract.Commands;

public class CreateDepartment : IRequest<OneOf<DepartmentDto, IBadRequestError>>
{
    public CreateDepartment(DepartmentCreateDto departmentCreate, AuthContext authContext)
    {
        DepartmentCreate = departmentCreate;
        AuthContext = authContext;
    }

    public DepartmentCreateDto DepartmentCreate { get; }

    public AuthContext AuthContext { get; }
}