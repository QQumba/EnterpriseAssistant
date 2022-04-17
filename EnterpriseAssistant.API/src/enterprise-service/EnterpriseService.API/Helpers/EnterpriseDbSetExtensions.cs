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
}