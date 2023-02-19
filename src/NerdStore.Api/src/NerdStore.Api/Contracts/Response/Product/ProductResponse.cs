namespace NerdStore.Api.Contracts.Response.Product;

public class ProductResponse
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreationDate { get; set; }
    public int QuantityStock { get; set; }
    public DimensionsResponse Dimensions { get; set; }
}