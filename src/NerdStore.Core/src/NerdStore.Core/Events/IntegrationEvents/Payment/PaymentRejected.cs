namespace NerdStore.Core.Events.IntegrationEvents.Payment;

public class PaymentRejected: IntegrationEvent
{
    public Guid TransactionId { get; set; }
    public PaymentRejected(Guid aggregateId, Guid transactionId) : base(aggregateId)
    {
        TransactionId = transactionId;
    }
}