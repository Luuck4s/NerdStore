namespace NerdStore.Vendas.Api.Contracts.Requests.Product;

public class DeleteItemOrderRequest
{
    public Guid ClientId { get;  set; }
    public Guid ProductId { get;  set; }
}