using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using NerdStore.Core.Data;
using NerdStore.Vendas.Data.Mappings;
using NerdStore.Vendas.Domain.Entities;
using NerdStore.Vendas.Domain.Entities.ValueObject;

namespace NerdStore.Vendas.Data.Contexts;

public class VendasContext: DbContext, IUnitOfWork
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<ItemOrder> ItemOrders { get; set; }
    public DbSet<Voucher> Vouchers { get; set; }

    public VendasContext(DbContextOptions<VendasContext> options): base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Notification>();

        modelBuilder.ApplyConfiguration(new OrderMapping());
        modelBuilder.ApplyConfiguration(new ItemOrderMapping());
        modelBuilder.ApplyConfiguration(new VoucherMapping());
    }

    public async Task<bool> Commit()
    {
        return await base.SaveChangesAsync() > 0;
    }
}