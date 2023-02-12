using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Vendas.Domain.Entities.ValueObject;

namespace NerdStore.Vendas.Data.Mappings;

public class ItemOrderMapping : IEntityTypeConfiguration<ItemOrder>
{
    public void Configure(EntityTypeBuilder<ItemOrder> builder)
    {
        builder.ToTable("ItemsOrder");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.ProductName)
            .IsRequired()
            .HasColumnType("varchar(250)");
        builder.Property(c => c.Quantity)
            .IsRequired()
            .HasColumnType("int");;
        builder.Property(c => c.UnitAmount)
            .IsRequired()
            .HasColumnType("decimal(10,2)");

        builder.HasOne(c => c.Order)
            .WithMany(c => c.ItemOrders);
    }
}