using MediatR;
using NerdStore.Catalogo.Data.Repositories;
using NerdStore.Catalogo.Domain.Repositories;
using NerdStore.Catalogo.Domain.Services;

namespace NerdStore.Catalogo.Api.Configuration;

public static class ServicesExtensions
{
    public static void AddServices(this IServiceCollection service)
    {
        service.AddScoped<IProductRepository, ProductRepository>();
        service.AddScoped<IStockService, StockService>();
    }
}