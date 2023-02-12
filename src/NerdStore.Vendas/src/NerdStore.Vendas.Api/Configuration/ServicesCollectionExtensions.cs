using NerdStore.Vendas.Data.Repositories;
using NerdStore.Vendas.Domain.Repository;

namespace NerdStore.Vendas.Api.Configuration;

public static class ServicesCollectionExtensions
{
    public static void AddServices(this IServiceCollection service)
    {
        service.AddScoped<IOrderRepository, OrderRepository>();
    }
}