using NerdStore.Core.Commands;

namespace NerdStore.Vendas.Domain.Commands;

public class CancelOrderCommand: ICommand
{
    public Guid OrderId { get; set; }

    public CancelOrderCommand(Guid orderId)
    {
        OrderId = orderId;
    }
}