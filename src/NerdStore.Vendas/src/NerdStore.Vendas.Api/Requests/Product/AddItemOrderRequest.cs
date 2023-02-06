namespace NerdStore.Vendas.Api.Requests.Product;

public class AddItemOrderRequest
{
    public Guid ClientId { get;  set; }
    public Guid ProductId { get;  set; }
    public string Name { get;  set; }
    public int Quantity { get;  set; }
    public decimal UnitAmount { get;  set; }
}