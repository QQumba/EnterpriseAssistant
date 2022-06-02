using DepartmentService.Contract.DataTransfer;
using EnterpriseAssistant.Application.Errors;
using EnterpriseAssistant.DataAccess;
using MediatR;
using OneOf;
using OneOf.Types;

namespace DepartmentService.API.Commands;

public class GetDepartmentById : IRequest<OneOf<IEnumerable<DepartmentDto>, INotFoundError>>
{
    public GetDepartmentById(long id, bool includeChild)
    {
        Id = id;
        IncludeChild = includeChild;
    }

    public long Id { get; }

    public bool IncludeChild { get; }
}

public class GetDepartmentByIdHandler
    : IRequestHandler<GetDepartmentById, OneOf<IEnumerable<DepartmentDto>, INotFoundError>>
{
    private readonly EnterpriseAssistantDbContext _db;

    public GetDepartmentByIdHandler(EnterpriseAssistantDbContext db)
    {
        _db = db;
    }

    public Task<OneOf<IEnumerable<DepartmentDto>, INotFoundError>> Handle(GetDepartmentById request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}