using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EnterpriseAssistant.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ProjectService.API.Helpers;

public static class ProjectDbSetExtensions
{
    public static async Task<bool> IsIdTaken(this IQueryable<Project> projects, string id, CancellationToken cancellationToken = default)
    {
        return await projects.AnyAsync(p => p.Equals(id), cancellationToken);
    }

    public static async Task<bool> IsProjectNameExist(this IQueryable<Project> projects, string id, string name, CancellationToken cancellationToken = default)
    {
        return await projects.AnyAsync(p => p.Name.Equals(name), cancellationToken);
    }
}
