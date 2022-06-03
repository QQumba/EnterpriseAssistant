using System.Security.Claims;

namespace EnterpriseAssistant.Application.Shared;

public static class ClaimPrincipalExtensions
{
    public static string? GetEmail(this ClaimsPrincipal principal)
    {
        return "test@mail.com";
        // todo: use when auth is done
        return principal.FindFirst("email")?.Value;
    }

    public static string? GetLogin(this ClaimsPrincipal principal)
    {
        return "test";
        // todo: use when auth is done
        return principal.FindFirst("login")?.Value;
    }

    public static string? GetEnterpriseId(this ClaimsPrincipal principal)
    {
        return "test";
        // todo: use when auth is done
        return principal.FindFirst("enterprise_id")?.Value;
    }
    
    public static AuthContext GetAuthContext(this ClaimsPrincipal principal)
    {
        return new AuthContext
        {
            Email = principal.GetEmail(),
            EnterpriseId = principal.GetEnterpriseId(),
            Login = principal.GetLogin()
        };
    }
}