using Rebus.Sagas;

namespace NerdStore.SagaBus.Order.SagasData;

public class OrderSagaData: SagaData
{
    public bool OrderStarted { get; set; }
    public bool PaymentSuccessful { get; set; }
    public bool OrderEnded { get; set; }
    public bool OrderCanceled { get; set; }

    public bool CompletedSaga => OrderStarted
                                && PaymentSuccessful
                                && OrderEnded 
                                || OrderCanceled;
}
