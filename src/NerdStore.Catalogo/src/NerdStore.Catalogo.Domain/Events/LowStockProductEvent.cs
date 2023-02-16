using NerdStore.Core.Events;

namespace NerdStore.Catalogo.Domain.Events;

public class LowStockProductEvent: DomainEvent
{
    public int RemainingQuantity { get; private set; }

    public LowStockProductEvent(Guid aggregateId, int remainingQuantity) : base(aggregateId)
    {
        RemainingQuantity = remainingQuantity;
    }
}