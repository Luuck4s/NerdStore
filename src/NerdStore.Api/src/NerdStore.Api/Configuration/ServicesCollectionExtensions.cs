using MediatR;
using NerdStore.Catalogo.Data.Repositories;
using NerdStore.Catalogo.Domain.Repositories;
using NerdStore.Catalogo.Domain.Services;
using NerdStore.Core.EventHandler;
using NerdStore.Pagamentos.AntiCorruption.Configuration;
using NerdStore.Pagamentos.AntiCorruption.Facades;
using NerdStore.Pagamentos.AntiCorruption.Gateway;
using NerdStore.Pagamentos.Business.Facades;
using NerdStore.Pagamentos.Business.Repositories;
using NerdStore.Pagamentos.Business.Services;
using NerdStore.Pagamentos.Data.Repositories;
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
        service.AddScoped<IPaymentRepository, PaymentRepository>();

        service.AddScoped<IStockService, StockService>();
        service.AddScoped<IPaymentService, PaymentService>();

        service.AddScoped<IPagarmeGateway, PagarmeGateway>();
        service.AddScoped<IPaymentCreditCardFacade, PaymentCreditCardFacade>();

        service.AddScoped<ConfigManager>();
    }
}