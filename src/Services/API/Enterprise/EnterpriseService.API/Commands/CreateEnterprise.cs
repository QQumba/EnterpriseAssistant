using System.Threading;
using System.Threading.Tasks;
using DepartmentService.Contract.DataTransfer;
using EnterpriseAssistant.Application.Shared;
using EnterpriseAssistant.DataAccess;
using EnterpriseAssistant.DataAccess.Entities;
using EnterpriseAssistant.DataAccess.Entities.Enums;
using EnterpriseService.API.Helpers;
using EnterpriseService.API.OneOfResponses;
using EnterpriseService.Contract.DataTransfer;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;

namespace EnterpriseService.API.Commands;

public class CreateEnterprise : IRequest<OneOf<EnterpriseDto, EnterpriseIdAlreadyTakenError>>
{
    public CreateEnterprise(EnterpriseCreateDto enterpriseCreate, AuthContext authContext)
    {
        AuthContext = authContext;
        EnterpriseCreate = enterpriseCreate;
    }

    public EnterpriseCreateDto EnterpriseCreate { get; }

    public AuthContext AuthContext { get; }
}

public class CreateEnterpriseHandler
    : IRequestHandler<CreateEnterprise, OneOf<EnterpriseDto, EnterpriseIdAlreadyTakenError>>
{
    private readonly EnterpriseAssistantDbContext _db;

    public CreateEnterpriseHandler(EnterpriseAssistantDbContext db)
    {
        _db = db;
    }

    public async Task<OneOf<EnterpriseDto, EnterpriseIdAlreadyTakenError>> Handle(CreateEnterprise request,
        CancellationToken cancellationToken)
    {
        if (await _db.Enterprises.IsIdTaken(request.EnterpriseCreate.Id, cancellationToken))
        {
            return new EnterpriseIdAlreadyTakenError(request.EnterpriseCreate.Id);
        }

        var enterprise = CreateEnterprise(request.EnterpriseCreate);

        var department = CreateDepartment(request.EnterpriseCreate.DepartmentCreate);
        department.Enterprise = enterprise;

        var user = await _db.Users.SingleAsync(u => u.Email.Equals(request.AuthContext.Email), cancellationToken);
        AddUserToEnterprise(user, enterprise, request.EnterpriseCreate.UserLogin);
        AddUserToDepartment(user, department);

        await _db.SaveChangesAsync(cancellationToken);
        return enterprise.Adapt<EnterpriseDto>();
    }

    private Enterprise CreateEnterprise(EnterpriseCreateDto enterpriseCreate)
    {
        var enterpriseToCreate = enterpriseCreate.Adapt<Enterprise>();

        var enterprise = _db.Enterprises.Add(enterpriseToCreate).Entity;

        return enterprise;
    }

    private Department CreateDepartment(DepartmentCreateDto departmentCreate)
    {
        var departmentToCreate = departmentCreate.Adapt<Department>();
        departmentToCreate.DepartmentType = DepartmentType.Root;
        return _db.Departments.Add(departmentToCreate).Entity;
    }

    private void AddUserToEnterprise(User user, Enterprise enterprise, string userLogin)
    {
        var enterpriseUser = new EnterpriseUser
        {
            Enterprise = enterprise,
            User = user,
            Login = userLogin
        };
        _db.EnterpriseUsers.Add(enterpriseUser);
    }

    private void AddUserToDepartment(User user, Department department)
    {
        var enterprise = department.Enterprise;
        var departmentUser = new DepartmentUser
        {
            Department = department,
            User = user,
            DepartmentUserRole = DepartmentUserRole.Admin,
            EnterpriseId = enterprise.Id
        };
        _db.DepartmentUsers.Add(departmentUser);
    }
}