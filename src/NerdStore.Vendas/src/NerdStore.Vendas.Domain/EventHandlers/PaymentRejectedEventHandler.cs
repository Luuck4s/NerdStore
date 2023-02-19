using MediatR;
using NerdStore.Core.EventHandler;
using NerdStore.Core.Events.IntegrationEvents.Payment;
using NerdStore.Vendas.Domain.Commands;

namespace NerdStore.Vendas.Domain.EventHandlers;

public class PaymentRejectedEventHandler: INotificationHandler<PaymentRejected>
{
    private readonly IMediatRHandler _mediatRHandler;

    public PaymentRejectedEventHandler(IMediatRHandler mediatRHandler)
    {
        _mediatRHandler = mediatRHandler;
    }

    public async Task Handle(PaymentRejected notification, CancellationToken cancellationToken)
    {
        await _mediatRHandler.PublishCommand(new CancelOrderReverseStockCommand(notification.OrderId));
    }
}