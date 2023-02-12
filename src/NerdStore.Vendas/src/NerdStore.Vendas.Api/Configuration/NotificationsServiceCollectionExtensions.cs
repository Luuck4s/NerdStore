using MediatR;
using NerdStore.Core.Notification;
using NerdStore.Vendas.Domain.EventHandlers;
using NerdStore.Vendas.Domain.Events;

namespace NerdStore.Vendas.Api.Configuration;

public static class NotificationsServiceCollectionExtensions
{
    public static void AddNotificationsService(this IServiceCollection services)
    {
        services.AddSingleton<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
        
        RegisterEvents(services);
        RegisterEventsHandlers(services);
    }
    
    private static void RegisterEventsHandlers(IServiceCollection service)
    {
        service.AddTransient<DraftOrderCreatedEventHandler>();
        service.AddTransient<ItemOrderAddedEventHandler>();
    }

    private static void RegisterEvents(IServiceCollection service)
    {
        service.AddScoped<INotificationHandler<DraftOrderCreated>, DraftOrderCreatedEventHandler>();
        service.AddScoped<INotificationHandler<ItemOrderAdded>, ItemOrderAddedEventHandler>();
    }
}