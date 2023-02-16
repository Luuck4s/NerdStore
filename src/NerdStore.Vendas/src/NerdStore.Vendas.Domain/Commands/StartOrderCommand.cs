using NerdStore.Core.Commands;

namespace NerdStore.Vendas.Domain.Commands;

public class StartOrderCommand: ICommand
{
    public Guid ClientId { get;  set; }
    public Guid OrderId { get;  set; }
    public string CarNumber { get; set; }
    public DateTime CardExpiration { get;  set; }
    public string CardCvv { get;  set; }
    public StartOrderCommand(Guid orderId, Guid clientId, string carNumber, DateTime cardExpiration, string cardCvv)
    {
        OrderId = orderId;
        ClientId = clientId;
        CarNumber = carNumber;
        CardExpiration = cardExpiration;
        CardCvv = cardCvv;
    }
}