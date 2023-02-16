namespace NerdStore.Core.Events.IntegrationEvents.Order;

public class OrderStockRejected: IntegrationEvent
{
    public Guid ClientId { get; private set; }

    public OrderStockRejected(Guid aggregateId, Guid clientId) : base(aggregateId)
    {
        ClientId = clientId;
    }
}