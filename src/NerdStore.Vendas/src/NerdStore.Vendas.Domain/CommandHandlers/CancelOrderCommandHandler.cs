using MediatR;
using NerdStore.Vendas.Domain.Commands;
using NerdStore.Vendas.Domain.Repository;

namespace NerdStore.Vendas.Domain.CommandHandlers;

public class CancelOrderCommandHandler: IRequestHandler<CancelOrderCommand, bool>
{
    private readonly IOrderRepository _orderRepository;

    public CancelOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<bool> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrder(request.OrderId);
        order!.Cancel();
        
        _orderRepository.Update(order);
        return await _orderRepository.UnitOfWork.Commit();
    }
}