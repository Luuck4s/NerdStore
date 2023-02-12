using Microsoft.EntityFrameworkCore;
using NerdStore.Vendas.Api.Settings;
using NerdStore.Vendas.Data.Contexts;

namespace NerdStore.Vendas.Api.Configuration;

public static class DatabaseServiceCollectionExtensions
{
    public static void AddDatabaseServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        var databaseSettings = configuration.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>();

        if (databaseSettings!.InMemory)
        {
            services.AddDbContext<VendasContext>(
                opt =>
                    opt.UseInMemoryDatabase("Database")
            );
        }
        else
        {
            services.AddDbContext<VendasContext>(
                opt =>
                    opt.UseSqlServer(databaseSettings.ConnectionString)
            );
        }
    }
}