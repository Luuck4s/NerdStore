using NerdStore.Core.Commands;

namespace NerdStore.Vendas.Domain.Commands;

public class DeleteItemOrderCommand : ICommand
{
    public Guid ClientId { get; private set; }
    public Guid OrderId { get; private set; }
    public Guid ProductId { get; private set; }

    public DeleteItemOrderCommand(Guid clientId, Guid orderId, Guid productId)
    {
        ClientId = clientId;
        OrderId = orderId;
        ProductId = productId;
    }
}