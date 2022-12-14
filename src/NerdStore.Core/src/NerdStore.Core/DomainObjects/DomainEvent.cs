using NerdStore.Core.Messages;

namespace NerdStore.Core.DomainObjects;

public abstract class DomainEvent: Event
{
    protected DomainEvent(Guid aggregateId)
    {
        AggregateId = aggregateId;
    }
}