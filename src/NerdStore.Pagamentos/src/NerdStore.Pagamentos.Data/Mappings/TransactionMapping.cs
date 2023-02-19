using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Pagamentos.Business;

namespace NerdStore.Pagamentos.Data.Mappings;

public class TransactionMapping: IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Total)
            .HasColumnType("decimal(10,2)");
        builder.Property(c => c.OrderId);
        builder.Property(c => c.PaymentId);
        builder.Property(c => c.StatusTransaction);

        builder.ToTable("Transactions");
    }
}