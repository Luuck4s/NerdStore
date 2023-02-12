using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Vendas.Domain.Entities;

namespace NerdStore.Vendas.Data.Mappings;

public class VoucherMapping: IEntityTypeConfiguration<Voucher>
{

    public void Configure(EntityTypeBuilder<Voucher> builder)
    {
        builder.ToTable("Vouchers");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Code)
            .IsRequired()
            .HasColumnType("varchar(100)");
        builder.Property(c => c.Quantity)
            .HasColumnType("int");
        builder.Property(c => c.Percent)
            .HasColumnType("int");
        builder.Property(c => c.ValidUntil);
        builder.Property(c => c.UsedAt);
        builder.Property(c => c.DiscountAmount)
            .HasColumnType("decimal(10,2)");
    }
}