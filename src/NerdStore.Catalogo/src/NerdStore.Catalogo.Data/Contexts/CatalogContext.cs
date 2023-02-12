using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using NerdStore.Catalogo.Data.Mappings;
using NerdStore.Catalogo.Domain.Entities;
using NerdStore.Core.Data;
using NerdStore.Core.Messages;

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
        modelBuilder.Ignore<Notification>();
        modelBuilder.Ignore<Event>();

        modelBuilder.ApplyConfiguration(new ProdutoMapping());
        modelBuilder.ApplyConfiguration(new CategoryMapping());
        modelBuilder.ApplyConfiguration(new DimensionsMapping());
    }

    public async Task<bool> Commit()
    {
        return await base.SaveChangesAsync() > 0;
    }
}