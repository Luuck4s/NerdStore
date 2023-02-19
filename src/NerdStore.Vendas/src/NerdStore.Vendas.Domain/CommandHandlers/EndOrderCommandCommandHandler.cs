using MediatR;
using NerdStore.Core.Events.IntegrationEvents.Order;
using NerdStore.Vendas.Domain.Commands;
using NerdStore.Vendas.Domain.Repository;

namespace NerdStore.Vendas.Domain.CommandHandlers;

public class EndOrderCommandCommandHandler: IRequestHandler<EndOrderCommand, bool>
{
    private readonly IOrderRepository _repository;

    public EndOrderCommandCommandHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(EndOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _repository.GetOrder(request.OrderId);
        order!.Paid();

        order.AddEvent(new OrderPaymentSuccessful(order.Id, order.ClientId));

        _repository.Update(order);
        return await _repository.UnitOfWork.Commit();
    }
}