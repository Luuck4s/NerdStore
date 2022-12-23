using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Catalogo.Domain.Entities;

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

        builder.OwnsOne(c => c.Dimensions, cm =>
        {
            cm.Property(c => c.Height)
                .HasColumnName("Altura")
                .HasColumnType("int");

            cm.Property(c => c.Width)
                .HasColumnName("Largura")
                .HasColumnType("int");

            cm.Property(c => c.Depth)
                .HasColumnName("Profundidade")
                .HasColumnType("int");

            cm.Ignore(c => c.Notifications);
        });
    }
}