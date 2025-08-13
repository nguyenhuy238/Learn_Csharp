using Microsoft.AspNetCore.Mvc.Filters;

namespace RequestLifecycleDemo.Filters;

public class LogActionFilter : IActionFilter
{
    private const string Key = "__action_start";
    private readonly ILogger<LogActionFilter> _logger;
    public LogActionFilter(ILogger<LogActionFilter> logger) => _logger = logger;

    public void OnActionExecuting(ActionExecutingContext context)
        => context.HttpContext.Items[Key] = DateTime.UtcNow;

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.HttpContext.Items[Key] is DateTime start)
        {
            var ms = (DateTime.UtcNow - start).TotalMilliseconds;
            _logger.LogInformation("Action {Action} took {Elapsed}ms",
                context.ActionDescriptor.DisplayName, ms);
        }
    }
}
