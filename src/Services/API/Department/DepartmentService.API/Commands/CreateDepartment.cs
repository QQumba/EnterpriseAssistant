using DepartmentService.Contract.DataTransfer;
using EnterpriseAssistant.Application.Shared;
using EnterpriseAssistant.DataAccess;
using EnterpriseAssistant.DataAccess.Entities;
using EnterpriseAssistant.DataAccess.Entities.Enums;
using Mapster;
using MediatR;
using OneOf;

namespace DepartmentService.API.Commands;

public class CreateDepartment : IRequest<OneOf<DepartmentDto>>
{
    public CreateDepartment(DepartmentCreateDto model, AuthContext authContext)
    {
        Model = model;
        AuthContext = authContext;
    }

    public DepartmentCreateDto Model { get; }

    public AuthContext AuthContext { get; }
}

public class CreateDepartmentHandler : IRequestHandler<CreateDepartment, OneOf<DepartmentDto>>
{
    private readonly EnterpriseAssistantDbContext _db;

    public CreateDepartmentHandler(EnterpriseAssistantDbContext db)
    {
        _db = db;
    }

    public async Task<OneOf<DepartmentDto>> Handle(CreateDepartment request,
        CancellationToken cancellationToken)
    {
        var department = request.Model.Adapt<Department>();
        department.EnterpriseId = request.AuthContext.EnterpriseId!;

        var createdDepartment = _db.Departments.Add(department).Entity;
        var departmentUser = new DepartmentUser
        {
            Department = createdDepartment,
            UserId = request.AuthContext.UserId,
            DepartmentUserRole = DepartmentUserRole.Admin,
            EnterpriseId = request.AuthContext.EnterpriseId!
        };
        _db.DepartmentUsers.Add(departmentUser);
        await _db.SaveChangesAsync(cancellationToken);
        return createdDepartment.Adapt<DepartmentDto>();
    }
}