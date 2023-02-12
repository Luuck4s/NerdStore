using MediatR;
using NerdStore.Core.EventHandler;
using NerdStore.Core.Notification;
using NerdStore.Vendas.Domain.Commands;
using NerdStore.Vendas.Domain.Entities.ValueObject;
using NerdStore.Vendas.Domain.Events;
using NerdStore.Vendas.Domain.Exceptions;
using NerdStore.Vendas.Domain.Repository;

namespace NerdStore.Vendas.Domain.CommandHandlers;

public class AddItemOrderCommandHandler: IRequestHandler<AddItemOrderCommand, bool>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMediatRHandler _mediatRHandler;

    public AddItemOrderCommandHandler(IOrderRepository orderRepository, IMediatRHandler mediatRHandler)
    {
        _orderRepository = orderRepository;
        _mediatRHandler = mediatRHandler;
    }

    public async Task<bool> Handle(AddItemOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrder(request.OrderId);
        if (order is null)
        {
            await _mediatRHandler.PublishNotification(new DomainNotification(request.MessageType, "Order not found", request.AggregateId));
            throw new OrderNotFoundException("Not found Order");
        }

        var itemOrder = new ItemOrder(request.ProductId, request.Name, request.Quantity, request.UnitAmount, request.AggregateId);
        order.AddItem(itemOrder);
        order.AddEvent(new ItemOrderAdded(request.ProductId, request.OrderId));

        _orderRepository.AddItemOrder(itemOrder);
        _orderRepository.Update(order);
        
        return await _orderRepository.UnitOfWork.Commit();
    }
}