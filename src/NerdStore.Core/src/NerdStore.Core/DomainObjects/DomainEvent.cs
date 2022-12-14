using System.Diagnostics.CodeAnalysis;
using NerdStore.Core.Messages;

namespace NerdStore.Core.DomainObjects;

[ExcludeFromCodeCoverage]
public abstract class DomainEvent: Event
{
    protected DomainEvent(Guid aggregateId)
    {
        AggregateId = aggregateId;
    }
}