using Microsoft.EntityFrameworkCore;
using NerdStore.Core.Data;
using NerdStore.Vendas.Data.Contexts;
using NerdStore.Vendas.Domain.Entities;
using NerdStore.Vendas.Domain.Entities.ValueObject;
using NerdStore.Vendas.Domain.Enums;
using NerdStore.Vendas.Domain.Repository;

namespace NerdStore.Vendas.Data.Repositories;

public class OrderRepository: IOrderRepository
{
    private readonly VendasContext _context;
    public IUnitOfWork UnitOfWork => _context;

    public OrderRepository(VendasContext context)
    {
        _context = context;
    }

    public async Task<Order?> GetOrder(Guid id)
    {
        return await _context.Orders
            .Include(x => x.ItemOrders)
            .Include(x => x.Voucher)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Order>> GetAllOrders()
    {
        return await _context.Orders.Include(x => x.ItemOrders).AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetOrdersByClient(Guid clientId)
    {
        return await _context.Orders.AsNoTracking().Where(o => o.ClientId == clientId).ToListAsync();
    }

    public async Task<Order?> GetOrderDraftedByClient(Guid clientId)
    {
        return await _context.Orders
            .Include(x => x.Voucher)
            .Include(x => x.ItemOrders)
            .FirstOrDefaultAsync(x => x.ClientId == clientId && x.OrderStatus == OrderStatus.Draft);
    }

    public void Add(Order order)
    {
        _context.Orders.Add(order);
    }

    public void Update(Order order)
    {
        _context.Orders.Update(order);
    }

    public async Task<ItemOrder?> GetItemOrder(Guid id)
    {
        return await _context.ItemOrders.FindAsync(id);
    }

    public async Task<ItemOrder?> GetItemOrderByOrderAndProduct(Guid orderId, Guid productId)
    {
        return await _context.ItemOrders.FirstOrDefaultAsync(x => x.OrderId == orderId && x.ProductId == productId);
    }

    public async Task<IEnumerable<ItemOrder>> GetItemOrderByOrder(Guid orderId)
    {
        return await _context.ItemOrders.AsNoTracking().Where(x => x.OrderId == orderId).ToListAsync();
    }

    public void AddItemOrder(ItemOrder itemOrder)
    {
        _context.ItemOrders.Add(itemOrder);
    }

    public void UpdateItemOrder(ItemOrder itemOrder)
    {
        _context.ItemOrders.Update(itemOrder);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}