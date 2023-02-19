using NerdStore.Core.Dtos;

namespace NerdStore.Core.Events.IntegrationEvents.Order;

public class OrderPaymentRejected : IntegrationEvent
{
    public Guid ClientId { get; private set; }
    public ItemOrderListDto ItemOrderList { get; private set; }

    public OrderPaymentRejected(Guid aggregateId, Guid clientId, ItemOrderListDto items) : base(aggregateId)
    {
        ClientId = clientId;
        ItemOrderList = items;
    }
}