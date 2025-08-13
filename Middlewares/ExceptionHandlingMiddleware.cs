using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace RequestLifecycleDemo.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    { _next = next; _logger = logger; }

    public async Task InvokeAsync(HttpContext ctx)
    {
        try { await _next(ctx); }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception: {TraceId}", ctx.TraceIdentifier);
            ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            ctx.Response.ContentType = "application/problem+json";

            var problem = new ProblemDetails
            {
                Status = ctx.Response.StatusCode,
                Title = "Unexpected error",
                Detail = appDetailed(ex),
                Instance = ctx.TraceIdentifier
            };
            await ctx.Response.WriteAsync(JsonSerializer.Serialize(problem));
        }

        static string? appDetailed(Exception ex) => ex.Message; // có thể ẩn ở Production
    }
}
