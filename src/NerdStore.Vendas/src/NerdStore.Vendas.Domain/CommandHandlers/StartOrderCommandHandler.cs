using MediatR;
using NerdStore.Core.Dtos;
using NerdStore.Core.EventHandler;
using NerdStore.Core.Events.IntegrationEvents.Order;
using NerdStore.Core.Notification;
using NerdStore.Vendas.Domain.Commands;
using NerdStore.Vendas.Domain.Exceptions;
using NerdStore.Vendas.Domain.Repository;

namespace NerdStore.Vendas.Domain.CommandHandlers;

public class StartOrderCommandHandler : IRequestHandler<StartOrderCommand, bool>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMediatRHandler _mediatRHandler;

    public StartOrderCommandHandler(IOrderRepository orderRepository, IMediatRHandler mediatRHandler)
    {
        _orderRepository = orderRepository;
        _mediatRHandler = mediatRHandler;
    }

    public async Task<bool> Handle(StartOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrder(request.OrderId);

        if (order is null)
        {
            await _mediatRHandler.PublishNotification(new DomainNotification(request.MessageType, "Order not found",
                request.AggregateId));
            throw new OrderNotFoundException("Not found Order");
        }

        order.Start();

        var itemsList = order.ItemOrders
            .Select(i => new ItemOrderDto(i.ProductId, i.Quantity))
            .ToList();
        var itemListOrder = new ItemOrderListDto(order.Id, itemsList);

        var @event = new OrderStarted(
            order.Id,
            order.ClientId,
            order.TotalAmount,
            itemListOrder,
            request.CarNumber,
            request.CardExpiration,
            request.CardCvv
        );
        order.AddEvent(@event);

        _orderRepository.Update(order);
        return await _orderRepository.UnitOfWork.Commit();
    }
}