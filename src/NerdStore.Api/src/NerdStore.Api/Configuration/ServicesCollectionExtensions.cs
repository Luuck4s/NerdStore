using MediatR;
using NerdStore.Catalogo.Data.Repositories;
using NerdStore.Catalogo.Domain.Repositories;
using NerdStore.Catalogo.Domain.Services;
using NerdStore.Core.EventHandler;
using NerdStore.Vendas.Data.Repositories;
using NerdStore.Vendas.Domain.Repository;

namespace NerdStore.Api.Configuration;

public static class ServicesCollectionExtensions
{
    public static void AddServices(this IServiceCollection service)
    {
        service.AddMediatR(typeof(Program));
        service.AddScoped<IMediatRHandler, MediatRHandler>();
        
        service.AddScoped<IOrderRepository, OrderRepository>();
        service.AddScoped<IVoucherRepository, VoucherRepository>();
        service.AddScoped<IProductRepository, ProductRepository>();
        service.AddScoped<IStockService, StockService>();
    }
}