using NerdStore.Core.Messages;

namespace NerdStore.Vendas.Domain.Events;

public class ItemOrderAdded : Event
{
    public Guid ProductId { get; set; }

    public ItemOrderAdded(Guid productId, Guid aggregateId)
    {
        ProductId = productId;
        AggregateId = aggregateId;
    }
}