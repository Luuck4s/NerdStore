using NerdStore.Core.Entities;
using NerdStore.Core.EventHandler;
using NerdStore.Pagamentos.Data.Context;

namespace NerdStore.Pagamentos.Data.Extensions;

public static class MediatorExtension
{
    public static async Task PublishEvents(this IMediatRHandler mediator, PaymentContext ctx)
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.Events.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.Events)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearEvents());

        var tasks = domainEvents
            .Select(async (domainEvent) => {
                await mediator.PublishEvent(domainEvent);
            });

        await Task.WhenAll(tasks);
    }
}