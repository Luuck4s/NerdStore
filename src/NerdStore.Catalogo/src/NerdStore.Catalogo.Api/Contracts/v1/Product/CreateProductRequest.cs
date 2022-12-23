namespace NerdStore.Catalogo.Api.Contracts.v1.Product;

public class CreateProductRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public decimal Amount { get; set; }

    public string Image { get; set; } = string.Empty;

    public int QuantityStock { get; set; }

    public decimal Height { get; set; }
    public decimal Width { get; set; }
    public decimal Depth { get; set; }
}