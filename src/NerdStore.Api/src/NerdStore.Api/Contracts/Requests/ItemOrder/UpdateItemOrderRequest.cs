namespace NerdStore.Api.Contracts.Requests.Product;

public class UpdateItemOrderRequest
{
    public Guid ClientId { get;  set; }
    public Guid ProductId { get;  set; }
    public int Quantity { get;  set; }
}