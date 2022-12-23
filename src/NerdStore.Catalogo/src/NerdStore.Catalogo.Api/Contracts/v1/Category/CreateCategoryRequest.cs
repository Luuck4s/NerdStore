namespace NerdStore.Catalogo.Api.Contracts.v1.Category;

public class CreateCategoryRequest
{
    public Guid Id { get; set; }

    public string Nome { get; set; } = String.Empty;

    public int Codigo { get; set; }
}