using Microsoft.EntityFrameworkCore;
using NerdStore.Catalogo.Api.Settings;
using NerdStore.Catalogo.Data.Contexts;

namespace NerdStore.Catalogo.Api.Configuration;

public static class DatabaseServiceCollectionExtensions
{
    public static void AddDatabaseServices(this IServiceCollection services,  ConfigurationManager configuration)
    {
        var databaseSettings = configuration.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>();

        if (databaseSettings.InMemory)
        {
            services.AddDbContext<CatalogContext>(
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
        }


    }
}