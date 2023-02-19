using MediatR;
using NerdStore.Core.Dtos;
using NerdStore.Core.Events.IntegrationEvents.Order;
using NerdStore.Pagamentos.Business.Services;

namespace NerdStore.Pagamentos.Business.EventHandler;

public class OrderStockConfirmedEventHandler : INotificationHandler<OrderStockConfirmed>
{
    private readonly IPaymentService _paymentService;

    public OrderStockConfirmedEventHandler(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    public async Task Handle(OrderStockConfirmed notification, CancellationToken cancellationToken)
    {
        var paymentOrder = new PaymentOrderDto()
        {
            OrderId = notification.AggregateId,
            ClientId = notification.ClientId,
            Amount = notification.Total,
            CardCvv = notification.CardCvv,
            CardExpiration = notification.CardExpiration,
            CarNumber = notification.CardNumber
        };

        await _paymentService.MakePaymentOrder(paymentOrder);
    }
}