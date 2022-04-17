using System.Security.Claims;

namespace EnterpriseAssistant.Application.Shared;

public static class ClaimPrincipalExtensions
{
    public static string GetEmail(this ClaimsPrincipal principal)
    {
        return "dummy@e.mail";
        // todo: use when auth is done
        return principal.FindFirst("email")!.Value;
    }
}