using MediatR;
using Microsoft.Extensions.Logging;
using NerdStore.Vendas.Domain.Events;

namespace NerdStore.Vendas.Domain.EventHandlers;

public class ItemOrderAddedEventHandler: INotificationHandler<ItemOrderAdded>
{
    private readonly ILogger<ItemOrderAddedEventHandler> _logger;

    public ItemOrderAddedEventHandler(ILogger<ItemOrderAddedEventHandler> logger)
    {
        _logger = logger;
    }
    
    public Task Handle(ItemOrderAdded notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Novo produto ${notification.ProductId} adicionado ao pedido {notification.AggregateId}");
        return Task.CompletedTask;
    }
}