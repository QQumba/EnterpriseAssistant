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
    private readonly DbContextFactory _factory;
    private readonly EnterpriseAssistantDbContext _db;

    public CreateDepartmentHandler(DbContextFactory factory)
    {
        _factory = factory;
        _db = factory.Create();
    }

    public async Task<OneOf<DepartmentDto, IBadRequestError>> Handle(CreateDepartment request,
        CancellationToken cancellationToken)
    {
        var department = request.DepartmentCreate.Adapt<Department>();
        department.EnterpriseId = request.AuthContext.EnterpriseId!;
        var readonlyContext = _factory.CreateReadOnlyContext(request.AuthContext);

        if (request.DepartmentCreate.ParentDepartmentId is null)
        {
            var rootDepartment = await readonlyContext.Departments
                .FirstAsync(d => d.DepartmentType == DepartmentType.Root, cancellationToken);
            department.ParentDepartmentId = rootDepartment.Id;
        }

        var codeAlreadyInUse = await readonlyContext.Departments
            .AnyAsync(d => d.Code == department.Code, cancellationToken);
        if (codeAlreadyInUse)
        {
            return new BadRequestError($"Department code {department.Code} already in use");
        }

        var createdDepartment = _db.Departments.Add(department).Entity;

        if (request.DepartmentCreate.DoNotJoin == false)
        {
            var departmentUser = new DepartmentUser
            {
                Department = createdDepartment,
                UserId = request.AuthContext.UserId,
                DepartmentUserRole = DepartmentUserRole.Admin,
                EnterpriseId = request.AuthContext.EnterpriseId!,
                DisplayAsMember = request.DepartmentCreate.DisplayAsMember
            };
            _db.DepartmentUsers.Add(departmentUser);
        }

        if (request.DepartmentCreate.Admins is not null)
        {
            foreach (var admin in request.DepartmentCreate.Admins)
            {
                var departmentUser = new DepartmentUser()
                {
                    Department = createdDepartment,
                    UserId = admin.Id,
                    DepartmentUserRole = DepartmentUserRole.Admin,
                    EnterpriseId = request.AuthContext.EnterpriseId!,
                    DisplayAsMember = admin.DisplayAsMember
                };
                _db.DepartmentUsers.Add(departmentUser);
            }
        }

        await _db.SaveChangesAsync(cancellationToken);
        return createdDepartment.Adapt<DepartmentDto>();
    }
}