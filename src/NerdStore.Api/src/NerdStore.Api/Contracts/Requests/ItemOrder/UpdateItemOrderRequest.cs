namespace NerdStore.Api.Contracts.Requests.ItemOrder;

public class UpdateItemOrderRequest
{
    public Guid ClientId { get;  set; }
    public Guid ProductId { get;  set; }
    public int Quantity { get;  set; }
}