using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EnterpriseAssistant.Web.Filters;

public class AuditActionFilter : IActionFilter
{
    private readonly ILogger<AuditActionFilter> _logger;
    private readonly Stopwatch _stopwatch = new();

    public AuditActionFilter(ILogger<AuditActionFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _logger.LogInformation("Calling {Method}", GetActionName(context));
        _stopwatch.Start();
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _stopwatch.Stop();
        _logger.LogInformation("{Method} executed in {ElapsedMilliseconds}ms",
            GetActionName(context),
            _stopwatch.ElapsedMilliseconds);
    }

    private static string GetActionName(ActionContext context)
    {
        var controller = context.ActionDescriptor.RouteValues["controller"];
        var action = context.ActionDescriptor.RouteValues["action"];
        return $"{controller}Controller.{action}";
    }
}