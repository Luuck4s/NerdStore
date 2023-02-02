using NerdStore.Core.Commands;

namespace NerdStore.Vendas.Domain.Commands;

public class AddItemOrderCommand : ICommand
{
    public Guid ClientId { get; private set; }
    public Guid ProductId { get; private set; }
    public string Name { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitAmount { get; private set; }

    public AddItemOrderCommand(Guid clientId, Guid productId, string name, int quantity, decimal unitAmount)
    {
        ClientId = clientId;
        ProductId = productId;
        Name = name;
        Quantity = quantity;
        UnitAmount = unitAmount;
    }
}