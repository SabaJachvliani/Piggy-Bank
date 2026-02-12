using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PiggyBank.Interfaces;

namespace PiggyBank.Servises.LoggerService;

public sealed class ErrorLoggingMiddleware : IMiddleware
{
    private readonly IErrorlogerInterface _logger; // use your interface name

    public ErrorLoggingMiddleware(IErrorlogerInterface logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            const int status = StatusCodes.Status500InternalServerError;

            try
            {
                await _logger.LogAsync(ex, context, status);
            }
            catch { }

            // if response already started, you can't rewrite it
            if (context.Response.HasStarted)
                throw;

            context.Response.Clear();
            context.Response.StatusCode = status;
            context.Response.ContentType = "application/problem+json";

            var problem = new ProblemDetails
            {
                Title = "Server error",
                Status = status,
                Detail = "Something went wrong.",
                Instance = context.Request.Path
            };

            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}
