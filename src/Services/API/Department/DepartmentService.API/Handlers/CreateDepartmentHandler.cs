using DepartmentService.Contract.Commands;
using DepartmentService.Contract.DataTransfer;
using EnterpriseAssistant.Application.Errors;
using EnterpriseAssistant.DataAccess;
using EnterpriseAssistant.DataAccess.Entities;
using EnterpriseAssistant.DataAccess.Entities.Enums;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;

namespace DepartmentService.API.Handlers;

public class CreateDepartmentHandler : IRequestHandler<CreateDepartment, OneOf<DepartmentDto, IBadRequestError>>
{
    private readonly EnterpriseAssistantDbContext _db;

    public CreateDepartmentHandler(EnterpriseAssistantDbContext db)
    {
        _db = db;
    }

    public async Task<OneOf<DepartmentDto, IBadRequestError>> Handle(CreateDepartment request,
        CancellationToken cancellationToken)
    {
        var department = request.Model.Adapt<Department>();
        department.EnterpriseId = request.AuthContext.EnterpriseId!;
        var rootDepartment = await _db.Departments
            .FirstAsync(d => d.EnterpriseId == request.AuthContext.EnterpriseId
                             && d.DepartmentType == DepartmentType.Root
                             && d.IsSoftDeleted == false, cancellationToken);

        department.ParentDepartmentId = rootDepartment.Id;
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