using NerdStore.Core.Events;

namespace NerdStore.Vendas.Domain.Events;

public class DraftOrderCreated : DomainEvent
{
    public Guid ClientId { get; set; }

    public DraftOrderCreated(Guid clientId, Guid aggregateId) : base(aggregateId)
    {
        ClientId = clientId;
        AggregateId = aggregateId;
    }
}