namespace NerdStore.Vendas.Api.Requests.Product;

public class AddItemOrderRequest
{
    public Guid ClientId { get;  set; }
    public Guid ProductId { get;  set; }
    public string Name { get;  set; } = string.Empty;
    public int Quantity { get;  set; }
    public decimal UnitAmount { get;  set; }
}