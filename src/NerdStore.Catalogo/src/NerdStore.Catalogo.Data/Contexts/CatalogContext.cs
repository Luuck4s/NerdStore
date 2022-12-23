using Microsoft.EntityFrameworkCore;
using NerdStore.Catalogo.Domain.Entities;
using NerdStore.Core.Data;

namespace NerdStore.Catalogo.Data.Contexts;

public class CatalogContext: DbContext, IUnitOfWork
{
    private const string DefaultVarcharType = "varchar(100)";

    public DbSet<Product> Produtos { get; set; }
    public DbSet<Category> Categorias { get; set; }

    public CatalogContext(DbContextOptions<CatalogContext> options): base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var properties = modelBuilder.Model
            .GetEntityTypes()
            .SelectMany(e => e.GetProperties()
            .Where(p => p.ClrType == typeof(string)));

        foreach (var property in properties)
        {
            property.SetColumnType(DefaultVarcharType);
        }

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogContext).Assembly);
    }

    public async Task<bool> Commit()
    {
        return await base.SaveChangesAsync() > 0;
    }
}