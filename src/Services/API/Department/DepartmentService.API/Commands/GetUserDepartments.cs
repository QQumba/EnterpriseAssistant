using DepartmentService.Contract.DataTransfer;
using EnterpriseAssistant.Application.Errors;
using EnterpriseAssistant.Application.Shared;
using EnterpriseAssistant.DataAccess;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;

namespace DepartmentService.API.Commands;

public class GetUserDepartments : IRequest<OneOf<IEnumerable<DepartmentDto>, INotFoundError>>
{
    public GetUserDepartments(AuthContext authContext)
    {
        AuthContext = authContext;
    }

    public AuthContext AuthContext { get; }
}

public class GetUserDepartmentsHandler
    : IRequestHandler<GetUserDepartments, OneOf<IEnumerable<DepartmentDto>, INotFoundError>>
{
    private readonly DbContextFactory _factory;

    public GetUserDepartmentsHandler(DbContextFactory factory)
    {
        _factory = factory;
    }

    public async Task<OneOf<IEnumerable<DepartmentDto>, INotFoundError>> Handle(GetUserDepartments request,
        CancellationToken cancellationToken)
    {
        var db = _factory.CreateReadOnlyContext(request.AuthContext);
        var user = await db.Users.SingleAsync(u => u.Email.Equals(request.AuthContext.Email), cancellationToken);

        var departments = await db.DepartmentUsers
            .Where(du => du.UserId == user.Id)
            .Include(du => du.Department)
            .Select(du => du.Department)
            .Where(d => d.IsSoftDeleted == false)
            .ToListAsync(cancellationToken);

        if (departments.Any() == false)
        {
            return new NotFoundError("Departments not found");
        }
        
        return departments.Adapt<List<DepartmentDto>>();
    }
}