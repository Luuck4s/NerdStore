using NerdStore.Vendas.Api.Queries;

namespace NerdStore.Vendas.Api.Configuration;

public static class QueriesServiceCollectionExtensions
{
    public static void AddQueries(this IServiceCollection services)
    {
        services.AddScoped<IOrderQueries, OrderQueries>();
    }
}