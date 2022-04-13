using System.Threading;
using System.Threading.Tasks;
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
        // create new enterprise
        if (await _db.Enterprises.IsIdTaken(request.EnterpriseCreate.Id, cancellationToken))
        {
            return new EnterpriseIdAlreadyTakenError(request.EnterpriseCreate.Id);
        }
        var enterpriseToCreate = request.EnterpriseCreate.Adapt<Enterprise>();
        
        enterpriseToCreate.OwnerEmail = request.OwnerEmail;
        var enterprise = _db.Enterprises.Add(enterpriseToCreate).Entity;

        // create new department and add to enterprise
        var departmentToCreate = request.EnterpriseCreate.DepartmentCreate.Adapt<Department>();
        departmentToCreate.DepartmentType = DepartmentType.Root;
        // todo: check if department id set to departmentToCreate.Id
        departmentToCreate.Enterprise = enterpriseToCreate;
        _db.Departments.Add(departmentToCreate);

        // create new user and add to enterprise and to root department
        var userToCreate = request.EnterpriseCreate.UserCreate.Adapt<User>();
        userToCreate.Salt = "";
        userToCreate.ManagedUserEmail = request.OwnerEmail;
        userToCreate.Enterprise = enterpriseToCreate;
        _db.Users.Add(userToCreate);
        var departmentUser = new DepartmentUser
        {
            Department = departmentToCreate,
            User = userToCreate,
            DepartmentUserRole = DepartmentUserRole.Admin,
            EnterpriseId = enterpriseToCreate.Id
        };

        _db.DepartmentUsers.Add(departmentUser);

        await _db.SaveChangesAsync(cancellationToken);
        return enterprise.Adapt<EnterpriseViewModel>();
    }
}