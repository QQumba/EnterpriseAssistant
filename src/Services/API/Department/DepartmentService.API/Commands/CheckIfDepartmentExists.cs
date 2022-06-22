using EnterpriseAssistant.Application.Shared;
using EnterpriseAssistant.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DepartmentService.API.Commands;

public class CheckIfDepartmentExists : IRequest<bool>
{
    public CheckIfDepartmentExists(string code, AuthContext authContext)
    {
        Code = code;
        AuthContext = authContext;
    }

    public string Code { get; }

    public AuthContext AuthContext { get; }
}

public class CheckIfDepartmentExistsHandler : IRequestHandler<CheckIfDepartmentExists, bool>
{
    private readonly DbContextFactory _factory;

    public CheckIfDepartmentExistsHandler(DbContextFactory factory)
    {
        _factory = factory;
    }

    public async Task<bool> Handle(CheckIfDepartmentExists request, CancellationToken cancellationToken)
    {
        var context = _factory.CreateReadOnlyContext(request.AuthContext);
        return await context.Departments.AnyAsync(d => d.Code.Equals(request.Code), cancellationToken);
    }
}