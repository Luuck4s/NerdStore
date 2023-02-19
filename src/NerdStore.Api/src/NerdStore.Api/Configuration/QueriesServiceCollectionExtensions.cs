using NerdStore.Api.Queries;

namespace NerdStore.Api.Configuration;

public static class QueriesServiceCollectionExtensions
{
    public static void AddQueries(this IServiceCollection services)
    {
        services.AddScoped<IOrderQueries, OrderQueries>();
        services.AddScoped<IProductQueries, ProductQueries>();
        services.AddScoped<ICategoryQueries, CategoryQueries>();
    }
}