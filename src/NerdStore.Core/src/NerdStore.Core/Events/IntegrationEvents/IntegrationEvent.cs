using NerdStore.Core.Messages;

namespace NerdStore.Core.Events.IntegrationEvents;

public abstract class IntegrationEvent : Event
{
    protected IntegrationEvent(Guid aggregateId)
    {
        AggregateId = aggregateId;
    }
}