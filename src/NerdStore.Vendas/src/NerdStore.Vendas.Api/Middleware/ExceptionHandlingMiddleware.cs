using System.Text.Json;
using MediatR;
using NerdStore.Core.Exceptions;
using NerdStore.Core.Notification;

namespace NerdStore.Vendas.Api.Middleware;

public class ExceptionHandlingMiddleware: IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private static DomainNotificationHandler? _notification;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger,
        INotificationHandler<DomainNotification> notification)
    {
        _logger = logger;
        _notification = (DomainNotificationHandler) notification;
    }

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
        if (exception is not ValidatorException convertedException)
        {
            return _notification!.GetNotifications().ToList().GroupBy(
                    x => x.Key,
                    x => x.Value,
                    (propertyName, errorMessages) => new
                    {
                        Key = propertyName,
                        Values = errorMessages.Distinct().ToArray()
                    })
                .ToDictionary(x => x.Key, x => x.Values);
        }

        return convertedException.Errors;
    }
}