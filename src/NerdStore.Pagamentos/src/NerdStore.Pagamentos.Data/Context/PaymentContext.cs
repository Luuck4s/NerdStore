using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using NerdStore.Core.Data;
using NerdStore.Core.EventHandler;
using NerdStore.Core.Messages;
using NerdStore.Pagamentos.Business;
using NerdStore.Pagamentos.Data.Extensions;
using NerdStore.Pagamentos.Data.Mappings;

namespace NerdStore.Pagamentos.Data.Context;

public class PaymentContext: DbContext, IUnitOfWork
{
    private readonly IMediatRHandler _mediatRHandler;

    public DbSet<Payment> Payments { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public PaymentContext(DbContextOptions<PaymentContext> options, IMediatRHandler mediatRHandler): base(options)
    {
        _mediatRHandler = mediatRHandler;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Notification>();
        modelBuilder.Ignore<Event>();

        modelBuilder.ApplyConfiguration(new PaymentMapping());
        modelBuilder.ApplyConfiguration(new TransactionMapping());
    }
    public async Task<bool> Commit()
    {
      await _mediatRHandler.PublishEvents(this);
      return await base.SaveChangesAsync() > 0;
    }
}