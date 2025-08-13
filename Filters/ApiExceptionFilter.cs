using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RequestLifecycleDemo.Services;

namespace RequestLifecycleDemo.Filters;

public class ApiExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ApiExceptionFilter> _logger;
    public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger) => _logger = logger;

    public void OnException(ExceptionContext context)
    {
        (int status, string title) = context.Exception switch
        {
            DomainNotFoundException => (StatusCodes.Status404NotFound, "Not Found"),
            DomainValidationException => (StatusCodes.Status400BadRequest, "Validation Error"),
            _ => (StatusCodes.Status500InternalServerError, "Server Error")
        };

        var problem = new ProblemDetails
        {
            Status = status,
            Title = title,
            Detail = context.Exception.Message,
            Instance = context.HttpContext.TraceIdentifier
        };

        _logger.LogWarning(context.Exception, "Handled by ApiExceptionFilter (TraceId={TraceId})",
            context.HttpContext.TraceIdentifier);

        context.Result = new ObjectResult(problem) { StatusCode = status };
        context.ExceptionHandled = true;
    }
}
