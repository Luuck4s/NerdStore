using MediatR;
using NerdStore.Vendas.Domain.Commands;
using NerdStore.Vendas.Domain.Entities;
using NerdStore.Vendas.Domain.Enums;
using NerdStore.Vendas.Domain.Events;
using NerdStore.Vendas.Domain.Repository;

namespace NerdStore.Vendas.Domain.CommandHandlers;

public class CreateOrderCommandHandler: IRequestHandler<CreateOrderCommand, bool>
{
    private readonly IOrderRepository _orderRepository;

    public CreateOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    
    public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order(request.ClientId, request.AggregateId)
        {
            OrderStatus = OrderStatus.Draft
        };
        order.AddEvent(new DraftOrderCreated(request.ClientId, request.AggregateId));

        _orderRepository.Add(order);
        return await _orderRepository.UnitOfWork.Commit();
    }
}