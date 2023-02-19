using NerdStore.Core.Entities;
using NerdStore.Core.Interfaces;
using NerdStore.Vendas.Domain.Entities.ValueObject;
using NerdStore.Vendas.Domain.Enums;
using NerdStore.Vendas.Domain.Exceptions;

namespace NerdStore.Vendas.Domain.Entities;

public class Order : Entity, IAggregateRoot
{
    public string Code { get; private set; }
    public Guid ClientId { get; set; }
    public Guid? VoucherId { get; set; }
    public bool IsVoucherActived { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; }
    public OrderStatus OrderStatus { get; set; }
    private readonly List<ItemOrder> _itemOrders;
    public IReadOnlyCollection<ItemOrder> ItemOrders => _itemOrders;

    public Voucher? Voucher { get; set; }

    public Order(Guid clientId, Guid aggregateId) : base(aggregateId)
    {
        ClientId = clientId;
        TotalAmount = 0;
        _itemOrders = new();
        Code = Guid.NewGuid().ToString().Substring(0, 10);
        CreatedAt = DateTime.Now;
    }

    protected Order()
    {
        _itemOrders = new();
        Code = Guid.NewGuid().ToString().Substring(0, 10);
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

        TotalAmount = Voucher!.CalculateDiscount(TotalAmount);
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
    
    public void UpdateItem(ItemOrder itemOrder, int quantity)
    {
        var item = _itemOrders.FirstOrDefault(o => o.ProductId == itemOrder.ProductId);

        if (item is null)
        {
            throw new InvalidRemoveItemOrder($"Item not found in order {itemOrder.ProductName}");
        }

        item.UpdateUnits(quantity);
        CalculateTotalAmountOrder();
    }

    public void Start()
    {
        OrderStatus = OrderStatus.Started;
    }

    public void Cancel()
    {
        OrderStatus = OrderStatus.Canceled;
    }

    public void Paid()
    {
        OrderStatus = OrderStatus.Paid;
    }
}