using MediatR;
using NerdStore.Vendas.Domain.Commands;
using NerdStore.Vendas.Domain.Entities.ValueObject;
using NerdStore.Vendas.Domain.Exceptions;
using NerdStore.Vendas.Domain.Repository;

namespace NerdStore.Vendas.Domain.CommandHandlers;

public class AddItemOrderCommandHandler: IRequestHandler<AddItemOrderCommand, bool>
{
    private readonly IOrderRepository _orderRepository;

    public AddItemOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<bool> Handle(AddItemOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrder(request.OrderId);
        if (order is null)
        {
            throw new OrderNotFoundException("Not found Order");
        }

        var itemOrder = new ItemOrder(request.ProductId, request.Name, request.Quantity, request.UnitAmount, request.AggregateId);
        order.AddItem(itemOrder);

        _orderRepository.AddItemOrder(itemOrder);
        _orderRepository.Update(order);
        
        return await _orderRepository.UnitOfWork.Commit();
    }
}