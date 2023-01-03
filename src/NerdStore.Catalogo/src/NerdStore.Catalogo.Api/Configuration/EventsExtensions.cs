using MediatR;
using NerdStore.Catalogo.Domain.EventHandlers;
using NerdStore.Catalogo.Domain.Events;
using NerdStore.Core.EventHandler;

namespace NerdStore.Catalogo.Api.Configuration;

public static class EventsExtensions
{
    public static void AddEventsServices(this IServiceCollection service)
    {
        service.AddMediatR(typeof(Program));
        service.AddScoped<IMediatRHandler, MediatRHandler>();

        RegisterEventsHandlers(service);
        RegisterEvents(service);
    }

    private static void RegisterEventsHandlers(IServiceCollection service)
    {
        service.AddTransient<ProductEventHandler>();
    }

    private static void RegisterEvents(IServiceCollection service)
    {
        service.AddScoped<INotificationHandler<LowStockProductEvent>, ProductEventHandler>();
    }
}