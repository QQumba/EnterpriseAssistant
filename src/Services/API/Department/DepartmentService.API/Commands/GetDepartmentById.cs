using EnterpriseAssistant.Application.Features.DepartmentFeatures.ViewModels;
using EnterpriseAssistant.DataAccess;
using MediatR;
using OneOf;
using OneOf.Types;

namespace DepartmentService.API.Commands;

public class GetDepartmentById : IRequest<OneOf<IEnumerable<DepartmentViewModel>, NotFound>>
{
    public GetDepartmentById(long id, bool includeChild)
    {
        Id = id;
        IncludeChild = includeChild;
    }

    public long Id { get; }

    public bool IncludeChild { get; }
}

public class
    GetDepartmentByIdHandler : IRequestHandler<GetDepartmentById, OneOf<IEnumerable<DepartmentViewModel>, NotFound>>
{
    private readonly EnterpriseAssistantDbContext _db;

    public GetDepartmentByIdHandler(EnterpriseAssistantDbContext db)
    {
        _db = db;
    }

    public Task<OneOf<IEnumerable<DepartmentViewModel>, NotFound>> Handle(GetDepartmentById request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}