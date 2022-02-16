using EnterpriseAssistant.Application.Features.EnterpriseFeatures.ViewModels;
using EnterpriseAssistant.DataAccess;
using EnterpriseAssistant.DataAccess.Entities;
using Mapster;
using MediatR;
using OneOf;

namespace EnterpriseAssistant.Application.Features.EnterpriseFeatures.Commands;

public class CreateEnterprise : IRequest<OneOf<EnterpriseViewModel>>
{
    public CreateEnterprise(EnterpriseCreateViewModel model)
    {
        Model = model;
    }

    public EnterpriseCreateViewModel Model { get; }
}

public class CreateEnterpriseHandler : IRequestHandler<CreateEnterprise, OneOf<EnterpriseViewModel>>
{
    private readonly EnterpriseAssistantDbContext _db;

    public CreateEnterpriseHandler(EnterpriseAssistantDbContext db)
    {
        _db = db;
    }

    public async Task<OneOf<EnterpriseViewModel>> Handle(CreateEnterprise request, CancellationToken cancellationToken)
    {
        var enterprise = _db.Enterprises.Add(request.Model.Adapt<Enterprise>()).Entity;
        await _db.SaveChangesAsync(cancellationToken);
        return enterprise.Adapt<EnterpriseViewModel>();
    }
}