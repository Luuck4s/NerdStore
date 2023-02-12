using NerdStore.Core.Data;
using NerdStore.Vendas.Domain.Entities;
using NerdStore.Vendas.Domain.Entities.ValueObject;

namespace NerdStore.Vendas.Domain.Repository;

public interface IOrderRepository: IRepository<Order>
{
    Task<Order?> GetOrder(Guid id);
    Task<List<Order>> GetAllOrders();
    Task<IEnumerable<Order>> GetOrdersByClient(Guid clientId);
    Task<Order?> GetOrderDraftedByClient(Guid clientId);

    void Add(Order order);
    void Update(Order order);

    Task<ItemOrder?> GetItemOrder(Guid id);
    Task<IEnumerable<ItemOrder>> GetItemOrderByOrder(Guid orderId);

    void AddItemOrder(ItemOrder itemOrder);
    void UpdateItemOrder(ItemOrder itemOrder);
}