namespace NerdStore.Core.Events.IntegrationEvents.Order;

public class OrderStockConfirmed: IntegrationEvent
{
    public Guid ClientId { get; private set; }
    public decimal Total { get; private set; }
    public string CarNumber { get; private set; }
    public DateTime CardExpiration { get; private set; }
    public string CardCvv { get; private set; }

    public OrderStockConfirmed(Guid aggregateId, Guid clientId, decimal total, string carNumber, DateTime cardExpiration, string cardCvv) : base(aggregateId)
    {
        ClientId = clientId;
        Total = total;
        CarNumber = carNumber;
        CardExpiration = cardExpiration;
        CardCvv = cardCvv;
    }
}