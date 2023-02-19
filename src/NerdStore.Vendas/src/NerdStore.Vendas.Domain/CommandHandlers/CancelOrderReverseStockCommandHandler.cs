using MediatR;
using NerdStore.Core.Dtos;
using NerdStore.Core.Events.IntegrationEvents.Order;
using NerdStore.Vendas.Domain.Commands;
using NerdStore.Vendas.Domain.Repository;

namespace NerdStore.Vendas.Domain.CommandHandlers;

public class CancelOrderReverseStockCommandHandler: IRequestHandler<CancelOrderReverseStockCommand, bool>
{
    private readonly IOrderRepository _repository;

    public CancelOrderReverseStockCommandHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(CancelOrderReverseStockCommand request, CancellationToken cancellationToken)
    {
        var order = await _repository.GetOrder(request.OrderId);
        order!.Cancel();
        
        var itemsList = order.ItemOrders
            .Select(i => new ItemOrderDto(i.ProductId, i.Quantity))
            .ToList();
        var itemListOrder = new ItemOrderListDto(order.Id, itemsList);

        order.AddEvent(new OrderPaymentRejected(order.Id,  order.ClientId, itemListOrder));

        _repository.Update(order);
        return await _repository.UnitOfWork.Commit();
    }
}