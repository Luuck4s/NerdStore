using NerdStore.Core.Messages;

namespace NerdStore.Vendas.Domain.Events;

public class DraftOrderCreated : Event
{
    public Guid ClientId { get; set; }

    public DraftOrderCreated(Guid clientId, Guid aggregateId)
    {
        ClientId = clientId;
        AggregateId = aggregateId;
    }
}