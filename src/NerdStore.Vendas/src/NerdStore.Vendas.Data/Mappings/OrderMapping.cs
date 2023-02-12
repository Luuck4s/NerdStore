using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Vendas.Domain.Entities;

namespace NerdStore.Vendas.Data.Mappings;

public class OrderMapping : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Code)
            .HasColumnType("varchar(10)");
        builder.Property(c => c.TotalAmount)
            .HasColumnType("decimal(10,2)");
        builder.Property(c => c.OrderStatus)
            .IsRequired()
            .HasColumnType("int");
        builder.Property(c => c.VoucherId);

        builder.HasMany(c => c.ItemOrders)
            .WithOne(c => c.Order)
            .HasForeignKey(c => c.OrderId);

        builder.HasOne(c => c.Voucher)
            .WithMany(c => c.Orders)
            .HasForeignKey(c => c.VoucherId);
    }
}