using EnterpriseAssistant.Application.Shared;
using EnterpriseAssistant.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Task = EnterpriseAssistant.DataAccess.Entities.Tasks.Task;

namespace EnterpriseAssistant.DataAccess;

public class DbContextFactory
{
    // private readonly EnterpriseAssistantDbContext _context;
    //
    // public DbContextFactory(EnterpriseAssistantDbContext context)
    // {
    //     _context = context;
    // }
    //
    // public AuthenticatedDbContext CreateReadOnlyContext(AuthContext authContext, bool readDeleted = false)
    // {
    //     return new AuthenticatedDbContext(_context, authContext, readDeleted);
    // }
    //
    // public EnterpriseAssistantDbContext Create()
    // {
    //     return _context;
    // }
}

public class AuthenticatedDbContext
{
    // private readonly EnterpriseAssistantDbContext _context;
    // private readonly AuthContext _authContext;
    // private readonly bool _readDeleted;
    //
    // public AuthenticatedDbContext(EnterpriseAssistantDbContext context, AuthContext authContext,
    //     bool readDeleted = false)
    // {
    //     _context = context;
    //     _authContext = authContext;
    //     _readDeleted = readDeleted;
    // }
    //
    // public IQueryable<Department> Departments =>
    //     _context.Departments.Where(d =>
    //         d.EnterpriseId.Equals(_authContext.EnterpriseId) && d.IsSoftDeleted == _readDeleted);
    //
    // public IQueryable<Enterprise> Enterprises =>
    //     _context.Enterprises.Where(e => e.Id.Equals(_authContext.EnterpriseId) && e.IsSoftDeleted == _readDeleted);
    //
    // public IQueryable<DepartmentUser> DepartmentUsers =>
    //     _context.DepartmentUsers.Where(du =>
    //         du.EnterpriseId.Equals(_authContext.EnterpriseId) && du.IsSoftDeleted == _readDeleted);
    //
    // public IQueryable<EnterpriseUser> EnterpriseUsers =>
    //     _context.EnterpriseUsers.Where(eu =>
    //         eu.EnterpriseId.Equals(_authContext.EnterpriseId) && eu.IsSoftDeleted == _readDeleted);
    //
    // public IQueryable<User> Users =>
    //     _context.EnterpriseUsers.Include(eu => eu.User)
    //         .Where(eu => eu.EnterpriseId.Equals(_authContext.EnterpriseId) && eu.IsSoftDeleted == _readDeleted)
    //         .Select(eu => eu.User);
    //
    // public IQueryable<Project> Projects =>
    //     _context.Projects.Where(
    //         p => p.EnterpriseId.Equals(_authContext.EnterpriseId) && p.IsSoftDeleted == _readDeleted);
    //
    // public IQueryable<TaskUser> TaskUsers =>
    //     _context.TaskUsers.Where(tu =>
    //         tu.EnterpriseId.Equals(_authContext.EnterpriseId) && tu.IsSoftDeleted == _readDeleted);
}