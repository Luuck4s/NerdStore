using NerdStore.Api.Contracts.Response.Product;

namespace NerdStore.Api.Queries;

public interface IProductQueries
{
    Task<List<ProductResponse>> GetAllProducts();
}