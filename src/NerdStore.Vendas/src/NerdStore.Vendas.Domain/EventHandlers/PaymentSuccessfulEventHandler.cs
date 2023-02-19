using MediatR;
using NerdStore.Core.EventHandler;
using NerdStore.Core.Events.IntegrationEvents.Payment;
using NerdStore.Vendas.Domain.Commands;

namespace NerdStore.Vendas.Domain.EventHandlers;

public class PaymentSuccessfulEventHandler: INotificationHandler<PaymentSuccessful>
{
    private readonly IMediatRHandler _mediatRHandler;

    public PaymentSuccessfulEventHandler(IMediatRHandler mediatRHandler)
    {
        _mediatRHandler = mediatRHandler;
    }

    public async Task Handle(PaymentSuccessful notification, CancellationToken cancellationToken)
    {
        await _mediatRHandler.PublishCommand(new EndOrderCommand(notification.OrderId, notification.ClientId));
    }
}