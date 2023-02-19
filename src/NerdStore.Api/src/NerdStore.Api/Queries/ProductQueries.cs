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
            CategoryId = x.CategoryId,
            Dimensions = new DimensionsResponse
            {
                Width = x.Dimensions.Width,
                Height = x.Dimensions.Height,
                Depth = x.Dimensions.Width,
            }
        }).ToList();
    }

    public async Task<ProductResponse> GetProduct(Guid productId)
    {
        var product = await _productRepository.GetById(productId);
        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            IsActive = product.IsActive,
            Amount = product.Amount,
            CreationDate = product.CreationDate,
            QuantityStock = product.QuantityStock,
            CategoryId = product.CategoryId,
            Dimensions = new DimensionsResponse
            {
                Width = product.Dimensions.Width,
                Height = product.Dimensions.Height,
                Depth = product.Dimensions.Width,
            }
        };
    }
}