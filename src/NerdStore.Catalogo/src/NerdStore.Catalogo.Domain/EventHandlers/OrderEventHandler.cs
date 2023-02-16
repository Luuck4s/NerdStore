using MediatR;
using NerdStore.Catalogo.Domain.Services;
using NerdStore.Core.EventHandler;
using NerdStore.Core.Events.IntegrationEvents.Order;

namespace NerdStore.Catalogo.Domain.EventHandlers;

public class OrderEventHandler: INotificationHandler<OrderStarted>
{
    private readonly IStockService _stockService;
    private readonly IMediatRHandler _mediatRHandler;

    public OrderEventHandler(IStockService stockService, IMediatRHandler mediatRHandler)
    {
        _stockService = stockService;
        _mediatRHandler = mediatRHandler;
    }

    public async Task Handle(OrderStarted notification, CancellationToken cancellationToken)
    {
        var result = await _stockService.DebitListItemStock(notification.ItemOrderList);

        if (result)
        {
            var @event = new OrderStockConfirmed(
                notification.AggregateId,
                notification.ClientId,
                notification.Total,
                notification.CarNumber,
                notification.CardExpiration,
                notification.CardCvv);
            await _mediatRHandler.PublishEvent(@event);
        }
        else
        {
            var @event = new OrderStockRejected(notification.AggregateId, notification.ClientId);
            await _mediatRHandler.PublishEvent(@event);
        }
    }
}