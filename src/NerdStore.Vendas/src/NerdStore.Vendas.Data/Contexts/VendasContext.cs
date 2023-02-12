using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using NerdStore.Core.Data;
using NerdStore.Core.EventHandler;
using NerdStore.Core.Messages;
using NerdStore.Vendas.Data.Extensions;
using NerdStore.Vendas.Data.Mappings;
using NerdStore.Vendas.Domain.Entities;
using NerdStore.Vendas.Domain.Entities.ValueObject;

namespace NerdStore.Vendas.Data.Contexts;

public class VendasContext: DbContext, IUnitOfWork
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<ItemOrder> ItemOrders { get; set; }
    public DbSet<Voucher> Vouchers { get; set; }
    private readonly IMediatRHandler _mediatRHandler;

    public VendasContext(DbContextOptions<VendasContext> options, IMediatRHandler mediatRHandler): base(options)
    {
        _mediatRHandler = mediatRHandler;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Notification>();
        modelBuilder.Ignore<Event>();

        modelBuilder.ApplyConfiguration(new OrderMapping());
        modelBuilder.ApplyConfiguration(new ItemOrderMapping());
        modelBuilder.ApplyConfiguration(new VoucherMapping());
    }

    public async Task<bool> Commit()
    {
        await _mediatRHandler.PublishEvents(this);
        return await base.SaveChangesAsync() > 0;
    }
}