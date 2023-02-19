using NerdStore.Core.Commands;

namespace NerdStore.Vendas.Domain.Commands;

public class EndOrderCommand: ICommand
{
    public Guid OrderId { get; set; }
    public Guid ClientId { get; set; }

    public EndOrderCommand(Guid orderId, Guid clientId)
    {
        OrderId = orderId;
        ClientId = clientId;
        AggregateId = orderId;
    }
}