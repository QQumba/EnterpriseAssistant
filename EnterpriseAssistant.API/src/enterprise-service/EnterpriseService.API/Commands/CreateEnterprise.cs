using EnterpriseAssistant.DataAccess;
using EnterpriseAssistant.DataAccess.Entities;
using EnterpriseService.Contract.ViewModels;
using Mapster;
using MediatR;
using OneOf;

namespace EnterpriseService.API.Commands;

public class CreateEnterprise : IRequest<OneOf<EnterpriseViewModel>>
{
    public CreateEnterprise(EnterpriseCreateViewModel enterpriseCreate)
    {
        EnterpriseCreate = enterpriseCreate;
    }

    public EnterpriseCreateViewModel EnterpriseCreate { get; }
}

public class CreateEnterpriseHandler : IRequestHandler<CreateEnterprise, OneOf<EnterpriseViewModel>>
{
    private readonly EnterpriseAssistantDbContext _db;

    public CreateEnterpriseHandler(EnterpriseAssistantDbContext db)
    {
        _db = db;
    }

    public async Task<OneOf<EnterpriseViewModel>> Handle(CreateEnterprise request,
        CancellationToken cancellationToken)
    {
        _db.Users.Add(request.EnterpriseCreate.UserCreate.Adapt<User>());
        _db.Departments.Add(request.EnterpriseCreate.DepartmentCreate.Adapt<Department>());

        var enterprise = _db.Enterprises.Add(request.EnterpriseCreate.Adapt<Enterprise>()).Entity;
        await _db.SaveChangesAsync(cancellationToken);
        return enterprise.Adapt<EnterpriseViewModel>();
    }
}