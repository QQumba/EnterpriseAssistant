using EnterpriseAssistant.Application.Shared;
using EnterpriseAssistant.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DepartmentService.API.Commands;

public class CheckIfDepartmentExists : IRequest<bool>
{
    public CheckIfDepartmentExists(string name, AuthContext authContext)
    {
        Name = name;
        AuthContext = authContext;
    }

    public string Name { get; }

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
        return await context.Departments.AnyAsync(d => d.Name.Equals(request.Name), cancellationToken);
    }
}