using MediatR;
using NerdStore.Catalogo.Domain.EventHandlers;
using NerdStore.Catalogo.Domain.Events;
using NerdStore.Core.Events.IntegrationEvents.Order;
using NerdStore.Core.Events.IntegrationEvents.Payment;
using NerdStore.Core.Notification;
using NerdStore.Pagamentos.Business.EventHandler;
using NerdStore.Vendas.Domain.EventHandlers;
using NerdStore.Vendas.Domain.Events;

namespace NerdStore.Api.Configuration;

public static class EventsServiceCollectionExtensions
{
    public static void AddEventsService(this IServiceCollection services)
    {
        services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
        
        RegisterEvents(services);
        RegisterEventsHandlers(services);
    }
    
    private static void RegisterEventsHandlers(IServiceCollection service)
    {
        service.AddTransient<DraftOrderCreatedEventHandler>();
        service.AddTransient<ItemOrderAddedEventHandler>();

        service.AddTransient<OrderStockConfirmedEventHandler>();
        service.AddTransient<OrderStockRejectedEventHandler>();
        
        service.AddTransient<PaymentRejectedEventHandler>();
        

        service.AddTransient<PaymentSuccessfulEventHandler>();

        service.AddTransient<ProductEventHandler>();
        service.AddTransient<OrderEventHandler>();
    }

    private static void RegisterEvents(IServiceCollection service)
    {
        service.AddScoped<INotificationHandler<DraftOrderCreated>, DraftOrderCreatedEventHandler>();
        service.AddScoped<INotificationHandler<ItemOrderAdded>, ItemOrderAddedEventHandler>();
        service.AddScoped<INotificationHandler<OrderStockConfirmed>, OrderStockConfirmedEventHandler>();
        service.AddScoped<INotificationHandler<OrderStockRejected>, OrderStockRejectedEventHandler>();
        
        service.AddScoped<INotificationHandler<PaymentSuccessful>, PaymentSuccessfulEventHandler>();

        service.AddScoped<INotificationHandler<LowStockProductEvent>, ProductEventHandler>();
        service.AddScoped<INotificationHandler<OrderPaymentRejected>, ProductEventHandler>();
        
        service.AddScoped<INotificationHandler<PaymentRejected>, PaymentRejectedEventHandler>();

        service.AddScoped<INotificationHandler<OrderStarted>, OrderEventHandler>();
    }
}