using MediatR;
using NerdStore.Core.EventHandler;
using NerdStore.Core.Events.IntegrationEvents.Order;
using NerdStore.Vendas.Domain.Commands;
using NerdStore.Vendas.Domain.Repository;

namespace NerdStore.Vendas.Domain.EventHandlers;

public class OrderStockRejectedEventHandler: INotificationHandler<OrderStockRejected>
{
    private readonly IMediatRHandler _mediatRHandler;

    public OrderStockRejectedEventHandler(IMediatRHandler mediatRHandler)
    {
        _mediatRHandler = mediatRHandler;
    }

    public async Task Handle(OrderStockRejected notification, CancellationToken cancellationToken)
    {
        await _mediatRHandler.PublishCommand(new CancelOrderCommand(notification.AggregateId));
    }
}