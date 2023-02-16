using NerdStore.Api.Contracts.Response.ItemOrder;
using NerdStore.Api.Contracts.Response.Voucher;

namespace NerdStore.Api.Contracts.Response.Order;

public class OrderResponse
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public string OrderStatus { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; }
    public VoucherResponse? Voucher { get; set; }
    public List<ItemOrderResponse> ItensOrder { get; set; }
}