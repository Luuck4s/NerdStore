using NerdStore.Core.Dtos;

namespace NerdStore.Core.Events.IntegrationEvents.Order;

public class OrderStarted : IntegrationEvent
{
    public Guid ClientId { get; private set; }
    public decimal Total { get; private set; }
    public ItemOrderListDto ItemOrderList { get; private set; }
    public string CarNumber { get; private set; }
    public DateTime CardExpiration { get; private set; }
    public string CardCvv { get; private set; }

    public OrderStarted(Guid aggregateId, Guid clientId, decimal total, ItemOrderListDto items,
        string carNumber, DateTime cardExpiration, string cardCvv) : base(aggregateId)
    {
        ClientId = clientId;
        Total = total;
        ItemOrderList = items;
        CarNumber = carNumber;
        CardExpiration = cardExpiration;
        CardCvv = cardCvv;
    }
}