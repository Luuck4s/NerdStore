namespace NerdStore.Core.Events.IntegrationEvents.Order;

public class OrderPaymentSuccessful: IntegrationEvent
{
    public Guid OrderId { get; set; }
    public Guid ClientId { get; set; }
    
    public OrderPaymentSuccessful(Guid orderId, Guid clientId) : base(orderId)
    {
        OrderId = orderId;
        ClientId = clientId;
    }
}