using DepartmentService.Contract.DataTransfer;
using EnterpriseAssistant.Application.Shared;
using EnterpriseAssistant.DataAccess;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DepartmentService.API.Commands;

public class GetEnterpriseDepartments : IRequest<IEnumerable<DepartmentDto>>
{
    public GetEnterpriseDepartments(AuthContext authContext)
    {
        AuthContext = authContext;
    }

    public AuthContext AuthContext { get; }
}

public class GetEnterpriseDepartmentsHandler : IRequestHandler<GetEnterpriseDepartments, IEnumerable<DepartmentDto>>
{
    private readonly DbContextFactory _factory;

    public GetEnterpriseDepartmentsHandler(DbContextFactory factory)
    {
        _factory = factory;
    }

    public async Task<IEnumerable<DepartmentDto>> Handle(GetEnterpriseDepartments request,
        CancellationToken cancellationToken)
    {
        var readOnlyContext = _factory.CreateReadOnlyContext(request.AuthContext);
        var departments = await readOnlyContext.Departments.ToListAsync(cancellationToken);
        return departments.Adapt<IEnumerable<DepartmentDto>>();
    }
}