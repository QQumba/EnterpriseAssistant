using EnterpriseAssistant.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace UserService.API.Helpers;

public static class UserDbSetExtensions
{
    public static Task<bool> IsEmailTaken(this IQueryable<User> users, string email,
        CancellationToken cancellationToken = default)

    {
        return users.AnyAsync(x => x.Email.Equals(email), cancellationToken);
    }
}