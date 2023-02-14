using NerdStore.Vendas.Api.Contracts.Order;

namespace NerdStore.Vendas.Api.Queries;

public interface IOrderQueries
{
    Task<List<OrderResponse>> GetAllOrders();
    Task<OrderResponse> GetOrder(Guid id);
}