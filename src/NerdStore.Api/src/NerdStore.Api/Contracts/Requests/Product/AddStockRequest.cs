namespace NerdStore.Api.Contracts.Requests.Product;

public class AddStockRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}