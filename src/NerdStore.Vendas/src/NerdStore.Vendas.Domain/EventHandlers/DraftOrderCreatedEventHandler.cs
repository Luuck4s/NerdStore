using MediatR;
using Microsoft.Extensions.Logging;
using NerdStore.Vendas.Domain.Events;

namespace NerdStore.Vendas.Domain.EventHandlers;

public class DraftOrderCreatedEventHandler: INotificationHandler<DraftOrderCreated>
{
    private readonly ILogger<DraftOrderCreatedEventHandler> _logger;

    public DraftOrderCreatedEventHandler(ILogger<DraftOrderCreatedEventHandler> logger)
    {
        _logger = logger;
    }
    
    public Task Handle(DraftOrderCreated notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Novo pedido iniciado {notification.AggregateId} - Cliente: {notification.ClientId}");
        return Task.CompletedTask;
    }
}