using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Pagamentos.Business;

namespace NerdStore.Pagamentos.Data.Mappings;

public class PaymentMapping: IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Status);
        builder.Property(c => c.ClientId);
        builder.Property(c => c.OrderId);
        builder.Property(c => c.Amount)
            .HasColumnType("decimal(10,2)");
        builder.Property(c => c.CardCvv)
            .IsRequired()
            .HasColumnType("varchar(250)");
        builder.Property(c => c.CarNumber)
            .IsRequired()
            .HasColumnType("varchar(16)");
        builder.Property(c => c.CardExpiration)
            .IsRequired()
            .HasColumnType("varchar(10)");

        builder.HasOne(c => c.Transaction)
            .WithOne(c => c.Payment);

        builder.ToTable("Payments");
    }
}