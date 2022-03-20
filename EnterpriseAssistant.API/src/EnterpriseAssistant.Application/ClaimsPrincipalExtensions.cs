using System.Security.Claims;

namespace EnterpriseAssistant.Application;

public static class ClaimsPrincipalExtensions
{
    public static string GetLogin(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.Claims.First(c => c.Type.Equals("login")).Value;
    }
}