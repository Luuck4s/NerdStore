using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Catalogo.Domain.ValueObjects;

namespace NerdStore.Catalogo.Data.Mappings;

public class DimensionsMapping: IEntityTypeConfiguration<Dimensions>
{
    public void Configure(EntityTypeBuilder<Dimensions> builder)
    {
        builder.ToTable("Dimensions");
        builder.HasKey(x => x.Id);

        builder.Property(c => c.Height)
            .HasColumnName("Altura")
            .HasColumnType("int");

        builder.Property(c => c.Width)
            .HasColumnName("Largura")
            .HasColumnType("int");

        builder.Property(c => c.Depth)
            .HasColumnName("Profundidade")
            .HasColumnType("int");
    }
}