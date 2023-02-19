using NerdStore.Api.Contracts.Response.Category;
using NerdStore.Catalogo.Domain.Repositories;

namespace NerdStore.Api.Queries;

public class CategoryQueries: ICategoryQueries
{
    private IProductRepository _productRepository;

    public CategoryQueries(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<List<CategoryResponse>> GetAllCategories()
    {
        var categories = await _productRepository.GetCategories();
        return categories.Select(c => new CategoryResponse()
        {
            Id = c.Id,
            Name = c.Name,
            Code = c.Code
        }).ToList();
    }
}