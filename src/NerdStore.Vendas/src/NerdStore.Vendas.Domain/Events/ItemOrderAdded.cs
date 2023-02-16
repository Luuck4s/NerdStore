using NerdStore.Core.Events;

namespace NerdStore.Vendas.Domain.Events;

public class ItemOrderAdded : DomainEvent
{
    public Guid ProductId { get; set; }

    public ItemOrderAdded(Guid productId, Guid aggregateId) : base(aggregateId)
    {
        ProductId = productId;
        AggregateId = aggregateId;
    }
}