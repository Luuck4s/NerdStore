using NerdStore.Core.Entities;

namespace NerdStore.Vendas.Domain.Entities.ValueObject;

public class ItemOrder : Entity
{
    public Guid OrderId { get; private set; }
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitAmount { get; private set; }

    public Order Order { get; set; }

    public ItemOrder(Guid productId, string productName, int quantity, decimal unitAmount)
    {
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitAmount = unitAmount;
    }

    protected ItemOrder()
    { }

    internal void AssociateOrder(Guid orderId)
    {
        OrderId = orderId;
    }

    public decimal CalculateAmount()
    {
        return Quantity * UnitAmount;
    }

    public void AddUnits(int units)
    {
        Quantity += units;
    }

    internal void UpdateUnits(int units)
    {
        Quantity = units;
    }
}