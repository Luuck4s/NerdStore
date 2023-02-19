using NerdStore.Core.Messages;

namespace NerdStore.Vendas.Domain.Events;

public class ItemOrderRemoved : Event
{
    public Guid ProductId { get; set; }

    public ItemOrderRemoved(Guid productId, Guid aggregateId)
    {
        ProductId = productId;
        AggregateId = aggregateId;
    }
}