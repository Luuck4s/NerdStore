using NerdStore.Api.Contracts.Response.Category;

namespace NerdStore.Api.Queries;

public interface ICategoryQueries
{
    Task<List<CategoryResponse>> GetAllCategories();
}