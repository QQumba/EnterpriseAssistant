using System.Threading;
using System.Threading.Tasks;
using DepartmentService.Contract.ViewModels;
using EnterpriseAssistant.DataAccess;
using EnterpriseAssistant.DataAccess.Entities;
using EnterpriseAssistant.DataAccess.Entities.Enums;
using EnterpriseService.API.Helpers;
using EnterpriseService.API.OneOfResponses;
using EnterpriseService.Contract.ViewModels;
using Mapster;
using MediatR;
using OneOf;

namespace EnterpriseService.API.Commands;

public class CreateEnterprise : IRequest<OneOf<EnterpriseViewModel, EnterpriseIdAlreadyTakenError>>
{
    public CreateEnterprise(EnterpriseCreateViewModel enterpriseCreate, string ownerEmail)
    {
        EnterpriseCreate = enterpriseCreate;
        OwnerEmail = ownerEmail;
    }

    public EnterpriseCreateViewModel EnterpriseCreate { get; }

    public string OwnerEmail { get; }
}

public class CreateEnterpriseHandler
    : IRequestHandler<CreateEnterprise, OneOf<EnterpriseViewModel, EnterpriseIdAlreadyTakenError>>
{
    private readonly EnterpriseAssistantDbContext _db;

    public CreateEnterpriseHandler(EnterpriseAssistantDbContext db)
    {
        _db = db;
    }

    public async Task<OneOf<EnterpriseViewModel, EnterpriseIdAlreadyTakenError>> Handle(CreateEnterprise request,
        CancellationToken cancellationToken)
    {
        if (await _db.Enterprises.IsIdTaken(request.EnterpriseCreate.Id, cancellationToken))
        {
            return new EnterpriseIdAlreadyTakenError(request.EnterpriseCreate.Id);
        }

        var enterprise = CreateEnterprise(request.EnterpriseCreate, request.OwnerEmail);

        var department = CreateDepartment(request.EnterpriseCreate.DepartmentCreate);
        department.Enterprise = enterprise;

        CreateUserForDepartment(request.EnterpriseCreate.UserCreate.Adapt<User>(), department);

        await _db.SaveChangesAsync(cancellationToken);
        return enterprise.Adapt<EnterpriseViewModel>();
    }

    private Enterprise CreateEnterprise(
        EnterpriseCreateViewModel enterpriseCreate, string email)
    {
        var enterpriseToCreate = enterpriseCreate.Adapt<Enterprise>();

        enterpriseToCreate.OwnerEmail = email;
        var enterprise = _db.Enterprises.Add(enterpriseToCreate).Entity;

        return enterprise;
    }

    private Department CreateDepartment(DepartmentCreateViewModel departmentCreate)
    {
        var departmentToCreate = departmentCreate.Adapt<Department>();
        departmentToCreate.DepartmentType = DepartmentType.Root;
        return _db.Departments.Add(departmentToCreate).Entity;
    }

    private void CreateUserForDepartment(User user, Department department)
    {
        var enterprise = department.Enterprise!;
        // todo: add password secret handling
        user.Salt = "default_salt";
        user.ManagedUserEmail = enterprise.OwnerEmail;
        user.Enterprise = enterprise;
        _db.Users.Add(user);
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