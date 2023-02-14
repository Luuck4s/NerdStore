using NerdStore.Vendas.Api.Contracts.Response.ItemOrder;

namespace NerdStore.Vendas.Api.Contracts.Order;

public class OrderResponse
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<ItemOrderResponse> ItensOrder { get; set; }
}