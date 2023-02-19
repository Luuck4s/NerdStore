using Microsoft.EntityFrameworkCore;
using NerdStore.Api.Settings;
using NerdStore.Catalogo.Data.Contexts;
using NerdStore.Pagamentos.Data.Context;
using NerdStore.Vendas.Data.Contexts;

namespace NerdStore.Api.Configuration;

public static class DatabaseServiceCollectionExtensions
{
    public static void AddDatabaseServices(this IServiceCollection services,  ConfigurationManager configuration)
    {
        var databaseSettings = configuration.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>()!;

        if (databaseSettings.InMemory)
        {
            services.AddDbContext<CatalogContext>(
                opt =>
                    opt.UseInMemoryDatabase("Database")
            );
            services.AddDbContext<VendasContext>(
                opt =>
                    opt.UseInMemoryDatabase("Database")
            );
            services.AddDbContext<PaymentContext>(
                opt =>
                    opt.UseInMemoryDatabase("Database")
            );
        }
        else
        {
            services.AddDbContext<CatalogContext>(
                opt =>
                    opt.UseSqlServer(databaseSettings.ConnectionString)
            );
            services.AddDbContext<VendasContext>(
                opt =>
                    opt.UseSqlServer(databaseSettings.ConnectionString)
            );  
            services.AddDbContext<PaymentContext>(
                opt =>
                    opt.UseSqlServer(databaseSettings.ConnectionString)
            );  
        }


    }
}