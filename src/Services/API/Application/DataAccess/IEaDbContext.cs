using EnterpriseAssistant.DataAccess.Entities;

namespace EnterpriseAssistant.DataAccess;

public interface IEaDbContext
{
    IQueryable<User> Users { get; }
    IQueryable<Department> Departments { get; }
    IQueryable<Enterprise> Enterprises { get; }
}