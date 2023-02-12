using NerdStore.Core.Entities;
using NerdStore.Core.EventHandler;
using NerdStore.Vendas.Data.Contexts;

namespace NerdStore.Vendas.Data.Extensions;

public static class MediatorExtension
{
    public static async Task PublishEvents(this IMediatRHandler mediator, VendasContext ctx)
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