using System.Diagnostics.CodeAnalysis;
using NerdStore.Core.Messages;

namespace NerdStore.Core.Events;

[ExcludeFromCodeCoverage]
public abstract class DomainEvent: Event
{
    protected DomainEvent(Guid aggregateId)
    {
        AggregateId = aggregateId;
    }
}