namespace NerdStore.Core.Events.IntegrationEvents.Payment;

public class PaymentRejected: IntegrationEvent
{
    public Guid TransactionId { get; set; }
    public Guid OrderId { get; set; }
    public PaymentRejected(Guid aggregateId, Guid transactionId, Guid orderId) : base(aggregateId)
    {
        TransactionId = transactionId;
        OrderId = orderId;
    }
}