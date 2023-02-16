using NerdStore.Api.Contracts.Response.Product;
using NerdStore.Catalogo.Domain.Repositories;

namespace NerdStore.Api.Queries;

public class ProductQueries: IProductQueries
{
    private readonly IProductRepository _productRepository;

    public ProductQueries(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<ProductResponse>> GetAllProducts()
    {
        var products = await _productRepository.GetAll();
        return products.Select(x => new ProductResponse
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            IsActive = x.IsActive,
            Amount = x.Amount,
            CreationDate = x.CreationDate,
            QuantityStock = x.QuantityStock,
        }).ToList();
    }
}