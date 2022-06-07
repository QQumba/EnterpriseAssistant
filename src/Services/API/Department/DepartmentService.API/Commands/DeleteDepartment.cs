using EnterpriseAssistant.Application.Errors;
using EnterpriseAssistant.Application.Shared;
using EnterpriseAssistant.DataAccess;
using EnterpriseAssistant.DataAccess.Entities.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;

namespace DepartmentService.API.Commands;

public class DeleteDepartment : IRequest<OneOf<Unit, INotFoundError>>
{
    public DeleteDepartment(long departmentId, AuthContext authContext)
    {
        DepartmentId = departmentId;
        AuthContext = authContext;
    }

    public long DepartmentId { get; }

    public AuthContext AuthContext { get; }
}

public class DeleteDepartmentHandler : IRequestHandler<DeleteDepartment, OneOf<Unit, INotFoundError>>
{
    private readonly DbContextFactory _factory;
    private readonly EnterpriseAssistantDbContext _context;

    public DeleteDepartmentHandler(DbContextFactory factory)
    {
        _factory = factory;
        _context = factory.Create();
    }

    public async Task<OneOf<Unit, INotFoundError>> Handle(DeleteDepartment request, CancellationToken cancellationToken)
    {
        var readOnlyContext = _factory.CreateReadOnlyContext(request.AuthContext);
        var department = await readOnlyContext.DepartmentUsers
            .Where(du => du.DepartmentId == request.DepartmentId
                         && du.UserId == request.AuthContext.UserId
                         && du.DepartmentUserRole == DepartmentUserRole.Admin)
            .Include(du => du.Department)
            .Select(du => du.Department)
            .FirstOrDefaultAsync(d => d.IsSoftDeleted == false
                                      && d.DepartmentType != DepartmentType.Root, cancellationToken);

        if (department is null)
        {
            return new NotFoundError($"Department with id {request.DepartmentId} not found");
        }

        department.IsSoftDeleted = true;
        _context.Entry(department).Property(d => d.IsSoftDeleted).IsModified = true;
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}