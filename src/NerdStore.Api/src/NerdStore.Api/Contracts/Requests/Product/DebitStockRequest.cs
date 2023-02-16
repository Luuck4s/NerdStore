namespace NerdStore.Api.Contracts.Requests.Product;

public class DebitStockRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}