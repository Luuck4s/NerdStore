using MediatR;
using NerdStore.Core.Events.IntegrationEvents.Order;

namespace NerdStore.Vendas.Domain.EventHandlers;

public class OrderStockConfirmedEventHandler: INotificationHandler<OrderStockConfirmed>
{
    public Task Handle(OrderStockConfirmed notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}