using NerdStore.Core.Entities;
using NerdStore.Core.Interfaces;
using NerdStore.Vendas.Domain.Entities.ValueObject;
using NerdStore.Vendas.Domain.Enums;
using NerdStore.Vendas.Domain.Exceptions;

namespace NerdStore.Vendas.Domain.Entities;

public class Order : Entity, IAggregateRoot
{
    public int Code { get; private set; }
    public Guid ClientId { get; private set; }
    public Guid? VoucherId { get; private set; }
    public bool IsVoucherActived { get; private set; }
    public decimal Discount { get; private set; }
    public decimal TotalAmount { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public OrderStatus OrderStatus { get; private set; }
    private readonly List<ItemOrder> _itemOrders;
    public IReadOnlyCollection<ItemOrder> ItemOrders => _itemOrders;

    public Voucher Voucher { get; set; }

    public Order(Guid clientId, Guid? voucherId, decimal discount, decimal totalAmount)
    {
        ClientId = clientId;
        VoucherId = voucherId;
        Discount = discount;
        TotalAmount = totalAmount;
        _itemOrders = new();
    }

    protected Order()
    {
        _itemOrders = new();
    }

    public void ApplyDiscount(Voucher voucher)
    {
        Voucher = voucher;
        IsVoucherActived = true;
        CalculateTotalAmountOrder();
    }

    public void CalculateTotalAmountOrder()
    {
        TotalAmount = _itemOrders.Sum(o => o.CalculateAmount());
        CalculateTotalDiscount();
    }

    public void CalculateTotalDiscount()
    {
        if (IsVoucherActived is false) return;

        TotalAmount = Voucher.CalculateDiscount(TotalAmount);
    }

    public bool AlreadyExists(ItemOrder itemOrder)
    {
        return _itemOrders.Any(o => o.ProductId == itemOrder.ProductId);
    }

    public void AddItem(ItemOrder itemOrder)
    {
        if (AlreadyExists(itemOrder))
        {
            var item = _itemOrders.FirstOrDefault(o => o.ProductId == itemOrder.ProductId);
            item?.AddUnits(itemOrder.Quantity);
        }
        else
        {
            itemOrder.AssociateOrder(Id);
            _itemOrders.Add(itemOrder);
        }

        CalculateTotalAmountOrder();
    }

    public void RemoveItem(ItemOrder itemOrder)
    {
        var item = _itemOrders.FirstOrDefault(o => o.ProductId == itemOrder.ProductId);

        if (item is null)
        {
            throw new InvalidRemoveItemOrder($"Item not found in order {itemOrder.ProductName}");
        }

        _itemOrders.Remove(item!);
        CalculateTotalAmountOrder();
    }
}