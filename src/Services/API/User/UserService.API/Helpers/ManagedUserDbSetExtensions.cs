using EnterpriseAssistant.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace UserService.API.Helpers;

public static class ManagedUserDbSetExtensions
{
    public static Task<bool> IsEmailTaken(this IQueryable<ManagedUser> managedUsers, string email,
        CancellationToken cancellationToken = default)

    {
        return managedUsers.AnyAsync(x => x.Email.Equals(email), cancellationToken);
    }
}