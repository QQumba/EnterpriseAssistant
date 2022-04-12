using System.Threading;
using System.Threading.Tasks;
using EnterpriseAssistant.DataAccess;
using EnterpriseAssistant.DataAccess.Entities;
using EnterpriseService.API.OneOfResponses;
using EnterpriseService.Contract.ViewModels;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;

namespace EnterpriseService.API.Commands;

public class CreateEnterprise : IRequest<OneOf<EnterpriseViewModel,EnterpriseIdAlreadyTakenError>>
{
    public CreateEnterprise(EnterpriseCreateViewModel enterpriseCreate)
    {
        EnterpriseCreate = enterpriseCreate;
    }

    public EnterpriseCreateViewModel EnterpriseCreate { get; }
}

public class CreateEnterpriseHandler
    : IRequestHandler<CreateEnterprise,OneOf<EnterpriseViewModel,EnterpriseIdAlreadyTakenError>>
{
    private readonly EnterpriseAssistantDbContext _db;

    public CreateEnterpriseHandler(EnterpriseAssistantDbContext db)
    {
        _db = db;
    }

    public async Task<OneOf<EnterpriseViewModel,EnterpriseIdAlreadyTakenError>> Handle(CreateEnterprise request,
        CancellationToken cancellationToken)
    {
        var enterpriseToCreate = request.EnterpriseCreate.Adapt<Enterprise>();
        var isEnterpriseIdTaken =
        await _db.Enterprises.AnyAsync(e => e.Id.Equals(enterpriseToCreate.Id), cancellationToken);
        if (isEnterpriseIdTaken)
        {
            return new EnterpriseIdAlreadyTakenError(enterpriseToCreate.Id);
        }

        var enterprise = _db.Enterprises.Add(enterpriseToCreate).Entity;

        var userToCreate = request.EnterpriseCreate.UserCreate.Adapt<User>();
        userToCreate.Enterprise = enterprise;
        _db.Users.Add(userToCreate);

        var departmentToCreate = request.EnterpriseCreate.DepartmentCreate.Adapt<Department>();
        departmentToCreate.Enterprise = enterpriseToCreate;
        _db.Departments.Add(departmentToCreate);

        await _db.SaveChangesAsync(cancellationToken);
        return enterprise.Adapt<EnterpriseViewModel>();
    }
}