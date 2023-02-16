using MediatR;
using NerdStore.Core.Events.IntegrationEvents.Order;
using NerdStore.Vendas.Domain.Repository;

namespace NerdStore.Vendas.Domain.EventHandlers;

public class OrderStockRejectedEventHandler: INotificationHandler<OrderStockRejected>
{
    private readonly IOrderRepository _orderRepository;

    public OrderStockRejectedEventHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task Handle(OrderStockRejected notification, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrder(notification.AggregateId);
        order!.Cancel();
        
        _orderRepository.Update(order);
        await _orderRepository.UnitOfWork.Commit();
    }
}