using System.Text.Json;
using NerdStore.Core.Exceptions;

namespace NerdStore.Vendas.Api.Middleware;

public class ExceptionHandlingMiddleware: IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) => _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            await HandleExceptionAsync(context, e);
        }
    }
    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var statusCode = StatusCodes.Status400BadRequest;
        var response = new
        {
            status = statusCode,
            errors = GetErrors(exception)
        };
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private static Dictionary<string, string[]>? GetErrors(Exception exception)
    {
        return exception is not ValidatorException convertedException 
            ? null 
            : convertedException.Errors;
    }
}