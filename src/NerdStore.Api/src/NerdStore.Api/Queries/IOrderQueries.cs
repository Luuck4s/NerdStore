using NerdStore.Api.Contracts.Response.Order;

namespace NerdStore.Api.Queries;

public interface IOrderQueries
{
    Task<List<OrderResponse>> GetAllOrders();
    Task<OrderResponse> GetOrder(Guid id);
}