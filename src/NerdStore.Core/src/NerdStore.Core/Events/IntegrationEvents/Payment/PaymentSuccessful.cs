namespace NerdStore.Core.Events.IntegrationEvents.Payment;

public class PaymentSuccessful: IntegrationEvent
{
    public Guid ClientId { get; set; }
    public Guid PaymentId { get; set; }
    public Guid TransactionId { get; set; }
    public PaymentSuccessful(Guid aggregateId, Guid clientId, Guid paymentId, Guid transactionId) : base(aggregateId)
    {
        ClientId = clientId;
        PaymentId = paymentId;
        TransactionId = transactionId;
    }
}