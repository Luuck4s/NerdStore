namespace NerdStore.Api.Contracts.Requests.ItemOrder;

public class DeleteItemOrderRequest
{
    public Guid ClientId { get;  set; }
    public Guid ProductId { get;  set; }
}