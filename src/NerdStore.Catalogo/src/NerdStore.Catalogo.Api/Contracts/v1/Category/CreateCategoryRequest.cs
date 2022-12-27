namespace NerdStore.Catalogo.Api.Contracts.v1.Category;

public class CreateCategoryRequest
{
    public string Name { get; set; } = String.Empty;

    public int Code { get; set; }
}