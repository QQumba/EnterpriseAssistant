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
    private readonly EnterpriseAssistantDbContext _db;

    public GetUserDepartmentsHandler(EnterpriseAssistantDbContext db)
    {
        _db = db;
    }

    public async Task<OneOf<IEnumerable<DepartmentDto>, INotFoundError>> Handle(GetUserDepartments request,
        CancellationToken cancellationToken)
    {
        var user = await _db.Users.FirstAsync(
            u => u.Login == request.AuthContext.Login && u.EnterpriseId == request.AuthContext.EnterpriseId,
            cancellationToken);

        var departments = await _db.DepartmentUsers
            .Where(du => du.UserId == user.Id)
            .Include(du => du.Department)
            .Select(du => du.Department)
            .ToListAsync(cancellationToken: cancellationToken);

        if (departments.Any() == false)
        {
            return new NotFoundError("Departments not found");
        }
        
        return departments.Adapt<List<DepartmentDto>>();
    }
}