using EnterpriseAssistant.Application.Features.DepartmentFeatures.ViewModels;
using EnterpriseAssistant.DataAccess;
using EnterpriseAssistant.DataAccess.Entities;
using Mapster;
using MediatR;
using OneOf;

namespace EnterpriseAssistant.Application.Features.DepartmentFeatures.Commands;

public class CreateDepartment : IRequest<OneOf<DepartmentViewModel>>
{
    public CreateDepartment(DepartmentCreateViewModel model)
    {
        Model = model;
    }

    public DepartmentCreateViewModel Model { get; }
}

public class CreateDepartmentHandler : IRequestHandler<CreateDepartment, OneOf<DepartmentViewModel>>
{
    private readonly EnterpriseAssistantDbContext _db;

    public CreateDepartmentHandler(EnterpriseAssistantDbContext db)
    {
        _db = db;
    }

    public async Task<OneOf<DepartmentViewModel>> Handle(CreateDepartment request, CancellationToken cancellationToken)
    {
        var department = _db.Departments.Add(request.Model.Adapt<Department>()).Entity;
        await _db.SaveChangesAsync(cancellationToken);
        return department.Adapt<DepartmentViewModel>();
    }
}