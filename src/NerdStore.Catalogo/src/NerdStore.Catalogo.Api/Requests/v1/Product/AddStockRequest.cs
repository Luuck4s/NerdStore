namespace NerdStore.Catalogo.Api.Requests.v1.Product;

public class AddStockRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}