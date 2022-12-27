using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Catalogo.Domain.Entities;
using NerdStore.Catalogo.Domain.ValueObjects;

namespace NerdStore.Catalogo.Data.Mappings;

public class ProdutoMapping: IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Produtos");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name)
            .IsRequired()
            .HasColumnType("varchar(250)");
        builder.Property(c => c.Description)
            .IsRequired()
            .HasColumnType("varchar(500)");
        builder.Property(c => c.Image)
            .IsRequired()
            .HasColumnType("varchar(250)");
        builder.Property(c => c.Amount)
            .IsRequired()
            .HasColumnType("decimal(10,2)");

        builder
            .HasOne(p => p.Dimensions)
            .WithOne(d => d.Product)
            .HasForeignKey<Dimensions>(d => d.Id);

        builder.Ignore(c => c.Notifications);
    }
}