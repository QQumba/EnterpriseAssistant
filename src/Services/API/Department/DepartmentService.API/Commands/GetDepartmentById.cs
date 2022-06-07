using DepartmentService.Contract.DataTransfer;
using EnterpriseAssistant.Application.Errors;
using EnterpriseAssistant.Application.Shared;
using EnterpriseAssistant.DataAccess;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;

namespace DepartmentService.API.Commands;

public class GetDepartmentById : IRequest<OneOf<DepartmentDto, INotFoundError>>
{
    public GetDepartmentById(long id, AuthContext authContext, bool includeChild)
    {
        Id = id;
        AuthContext = authContext;
        IncludeChild = includeChild;
    }

    public long Id { get; }

    public AuthContext AuthContext { get; }

    public bool IncludeChild { get; }
}

public class GetDepartmentByIdHandler
    : IRequestHandler<GetDepartmentById, OneOf<DepartmentDto, INotFoundError>>
{
    private readonly DbContextFactory _factory;

    public GetDepartmentByIdHandler(DbContextFactory factory)
    {
        _factory = factory;
    }

    public async Task<OneOf<DepartmentDto, INotFoundError>> Handle(GetDepartmentById request,
        CancellationToken cancellationToken)
    {
        var readOnlyContext = _factory.CreateReadOnlyContext(request.AuthContext);
        var department = await readOnlyContext.DepartmentUsers
            .Where(du => du.UserId == request.AuthContext.UserId
                         && du.DepartmentId == request.Id)
            .Include(du => du.Department)
            .Select(du => du.Department)
            .FirstOrDefaultAsync(d => d.IsSoftDeleted == false, cancellationToken);

        if (department is null)
        {
            return new NotFoundError($"Department with id {request.Id} not found");
        }

        return department.Adapt<DepartmentDto>();
    }
}