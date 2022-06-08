using System.Security.Claims;

namespace EnterpriseAssistant.Application.Shared;

public static class ClaimUtilities
{
    public const string EnterpriseId = "enterprise_id";
    public const string Email = "email";
    public const string UserId = "user_id";

    public static long GetUserId(this ClaimsPrincipal principal)
    {
        return long.Parse(principal.FindFirst(UserId)?.Value ?? "-1");
    }

    public static string GetEmail(this ClaimsPrincipal principal)
    {
        return principal.FindFirst(Email)!.Value;
    }

    public static string? GetEnterpriseId(this ClaimsPrincipal principal)
    {
        return principal.FindFirst(EnterpriseId)?.Value;
    }

    public static AuthContext GetAuthContext(this ClaimsPrincipal principal)
    {
        return new AuthContext
        {
            UserId = principal.GetUserId(),
            Email = principal.GetEmail(),
            EnterpriseId = principal.GetEnterpriseId(),
        };
    }
}