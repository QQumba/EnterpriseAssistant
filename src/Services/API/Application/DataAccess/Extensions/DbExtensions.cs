using EnterpriseAssistant.Application.Shared;
using EnterpriseAssistant.DataAccess.Entities;
using EnterpriseAssistant.DataAccess.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseAssistant.DataAccess.Extensions;

public static class DbExtensions
{
    public static async Task<User?> GetDepartmentChiefUser(this EnterpriseAssistantDbContext context,
        AuthContext authContext)
    {
        return await context.DepartmentUsers
            .Where(du => du.EnterpriseId.Equals(authContext.EnterpriseId)
                         && du.IsSoftDeleted == false
                         && du.UserId == authContext.UserId
                         && (du.DepartmentUserRole == DepartmentUserRole.Admin))
            .Include(du => du.User)
            .Select(du => du.User)
            .FirstOrDefaultAsync(d => d.IsSoftDeleted == false);
    }

    public static async Task<bool> IsUserDepartmentAdmin(this EnterpriseAssistantDbContext context,
        AuthContext authContext, long departmentId)
    {
        return await context.DepartmentUsers
            .AnyAsync(du => du.IsSoftDeleted == false &&
                            du.DepartmentId == departmentId &&
                            du.UserId == authContext.UserId &&
                            du.EnterpriseId.Equals(authContext.EnterpriseId) &&
                            du.DepartmentUserRole == DepartmentUserRole.Admin);
    }
}