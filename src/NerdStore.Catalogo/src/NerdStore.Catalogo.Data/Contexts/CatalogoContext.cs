using Microsoft.EntityFrameworkCore;
using NerdStore.Catalogo.Domain.Entities;
using NerdStore.Core.Data;

namespace NerdStore.Catalogo.Data.Contexts;

public class CatalogoContext: DbContext, IUnitOfWork
{
    private const string DefaultVarcharType = "varchar(100)";

    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }

    public CatalogoContext(DbContextOptions<CatalogoContext> options): base(options)
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

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogoContext).Assembly);
    }

    public async Task<bool> Commit()
    {
        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property("DataCadastro").CurrentValue = DateTime.Now;
            }
            
            if (entry.State == EntityState.Modified)
            {
                entry.Property("DataCadastro").IsModified = false;
            }
        }

        return await base.SaveChangesAsync() > 0;
    }
}