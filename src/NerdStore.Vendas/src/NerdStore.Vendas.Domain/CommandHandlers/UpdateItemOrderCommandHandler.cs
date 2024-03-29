using MediatR;
using NerdStore.Core.EventHandler;
using NerdStore.Core.Notification;
using NerdStore.Vendas.Domain.Commands;
using NerdStore.Vendas.Domain.Exceptions;
using NerdStore.Vendas.Domain.Repository;

namespace NerdStore.Vendas.Domain.CommandHandlers;

public class UpdateItemOrderCommandHandler: IRequestHandler<UpdateItemOrderCommand, bool>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMediatRHandler _mediatRHandler;

    public UpdateItemOrderCommandHandler(IOrderRepository orderRepository, IMediatRHandler mediatRHandler)
    {
        _orderRepository = orderRepository;
        _mediatRHandler = mediatRHandler;
    }

    public async Task<bool> Handle(UpdateItemOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrder(request.OrderId);

        if (order is null)
        {
           await _mediatRHandler.PublishNotification(new DomainNotification(request.MessageType, "Order not found", request.AggregateId));
           throw new OrderNotFoundException("Not found Order");
        }

        var itemOrder = await _orderRepository.GetItemOrderByOrderAndProduct(order.Id, request.ProductId);
        order.UpdateItem(itemOrder!, request.Quantity);

        _orderRepository.UpdateItemOrder(itemOrder!);
        _orderRepository.Update(order);

        return await _orderRepository.UnitOfWork.Commit();
    }
}