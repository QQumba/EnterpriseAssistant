using EnterpriseAssistant.DataAccess;
using EnterpriseAssistant.DataAccess.Entities;
using EnterpriseService.Contranct.ViewModels;
using Mapster;
using MediatR;
using OneOf;

namespace EnterpriseService.API.Commands;

public class InitializeEnterprise : IRequest<OneOf<EnterpriseViewModel>>
{
    public InitializeEnterprise(EnterpriseInitializeViewModel model)
    {
        Model = model;
    }

    public EnterpriseInitializeViewModel Model { get; }
}

public class InitializeEnterpriseHandler : IRequestHandler<InitializeEnterprise, OneOf<EnterpriseViewModel>>
{
    private readonly EnterpriseAssistantDbContext _db;

    public InitializeEnterpriseHandler(EnterpriseAssistantDbContext db)
    {
        _db = db;
    }

    public async Task<OneOf<EnterpriseViewModel>> Handle(InitializeEnterprise request,
        CancellationToken cancellationToken)
    {
        _db.Users.Add(request.Model.UserCreate.Adapt<User>());
        _db.Departments.Add(request.Model.DepartmentCreate.Adapt<Department>());

        var enterprise = _db.Enterprises.Add(request.Model.Adapt<Enterprise>()).Entity;
        await _db.SaveChangesAsync(cancellationToken);
        return enterprise.Adapt<EnterpriseViewModel>();
    }
}