using NerdStore.Vendas.Api.Contracts.Order;
using NerdStore.Vendas.Api.Contracts.Response.ItemOrder;
using NerdStore.Vendas.Domain.Repository;

namespace NerdStore.Vendas.Api.Queries;

public class OrderQueries: IOrderQueries
{
    private readonly IOrderRepository _orderRepository;

    public OrderQueries(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<List<OrderResponse>> GetAllOrders()
    {
        var orders = await _orderRepository.GetAllOrders();
        return orders.Select(o => new OrderResponse
        {
            Id = o.Id,
            ClientId = o.ClientId,
            CreatedAt = o.CreatedAt,
            TotalAmount = o.TotalAmount,
            ItensOrder = o.ItemOrders.Select(io => new ItemOrderResponse
            {
                OrderId = io.OrderId,
                Quantity = io.Quantity,
                ProductId = io.ProductId,
                ProductName = io.ProductName,
                UnitAmount = io.UnitAmount,
            }).ToList()
        }).ToList();
    }

    public async Task<OrderResponse> GetOrder(Guid id)
    {
        var order = await _orderRepository.GetOrder(id);
        return new()
        {
            Id = order.Id,
            ClientId = order.ClientId,
            CreatedAt = order.CreatedAt,
            TotalAmount = order.TotalAmount,
            ItensOrder = order.ItemOrders.Select(io => new ItemOrderResponse
            {
                OrderId = io.OrderId,
                Quantity = io.Quantity,
                ProductId = io.ProductId,
                ProductName = io.ProductName,
                UnitAmount = io.UnitAmount,
            }).ToList(),
        };
    }
}