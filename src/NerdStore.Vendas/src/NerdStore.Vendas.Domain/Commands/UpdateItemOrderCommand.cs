using NerdStore.Core.Commands;

namespace NerdStore.Vendas.Domain.Commands;

public class UpdateItemOrderCommand : ICommand
{
    public Guid ClientId { get; private set; }
    public Guid OrderId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }

    public UpdateItemOrderCommand(Guid clientId, Guid orderId, Guid productId, int quantity)
    {
        ClientId = clientId;
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
    }
}