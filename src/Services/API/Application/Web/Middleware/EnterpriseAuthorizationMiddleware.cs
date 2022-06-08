using System.Security.Claims;

namespace EnterpriseAssistant.Web.Middleware;

public class EnterpriseAuthorizationMiddleware
{
    private readonly RequestDelegate _next;

    public EnterpriseAuthorizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public Task Invoke(HttpContext context)
    {
        var enterpriseId = context.Request.Headers["auth-enterprise"].FirstOrDefault();
        var userEnterpriseIds = context.User.FindFirst("enterprise_ids")?.Value.Split(' ');
        if (string.IsNullOrEmpty(enterpriseId) == false
            && userEnterpriseIds is not null
            && userEnterpriseIds.Contains(enterpriseId))
        {
            var claims = new List<Claim> { new("enterprise_id", enterpriseId) };
            context.User.AddIdentity(new ClaimsIdentity(claims));
        }

        return _next(context);
    }
}

public static class EnterpriseAuthorizationMiddlewareExtensions
{
    public static IApplicationBuilder UseEnterpriseAuthorization(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<EnterpriseAuthorizationMiddleware>();
    }
}