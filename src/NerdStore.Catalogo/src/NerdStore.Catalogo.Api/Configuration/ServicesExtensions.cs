using MediatR;
using Microsoft.EntityFrameworkCore;
using NerdStore.Catalogo.Data.Contexts;
using NerdStore.Catalogo.Data.Repositories;
using NerdStore.Catalogo.Domain.EventHandlers;
using NerdStore.Catalogo.Domain.Repositories;
using NerdStore.Catalogo.Domain.Services;
using NerdStore.Core.EventHandler;

namespace NerdStore.Catalogo.Api.Configuration;

public static class ServicesExtensions
{
    public static void AddServices(this IServiceCollection service)
    {
        service.AddMediatR(typeof(Program));
        service.AddScoped<IProductRepository, ProductRepository>();
        service.AddScoped<IStockService, StockService>();
        service.AddScoped<IMediatRHandler, MediatRHandler>();
        service.AddTransient<ProductEventHandler>();
    }
}