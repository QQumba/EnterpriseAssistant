using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EnterpriseAssistant.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseService.API.Helpers;

public static class EnterpriseDbSetExtensions
{
    public static async Task<bool> IsIdTaken(this IQueryable<Enterprise> enterprises, string id,
        CancellationToken cancellationToken = default)
    {
        return await enterprises.AnyAsync(e => e.Id.Equals(id), cancellationToken);
    }

    public static async Task<bool> IsEnterpriseUserExists(this IQueryable<EnterpriseUser> enterpriseUsers, string enterpriseId,
        string login, CancellationToken cancellationToken = default)
    {
        return await enterpriseUsers.AnyAsync(u => u.EnterpriseId.Equals(enterpriseId) && u.Login.Equals(login),
            cancellationToken);
    }
}