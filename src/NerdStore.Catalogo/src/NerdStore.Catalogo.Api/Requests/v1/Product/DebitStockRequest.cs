namespace NerdStore.Catalogo.Api.Requests.v1.Product;

public class DebitStockRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}