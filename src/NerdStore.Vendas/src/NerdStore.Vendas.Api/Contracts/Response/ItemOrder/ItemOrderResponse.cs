namespace NerdStore.Vendas.Api.Contracts.Response.ItemOrder;

public class ItemOrderResponse
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitAmount { get; set; }
}