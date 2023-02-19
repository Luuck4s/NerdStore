using NerdStore.Core.Commands;

namespace NerdStore.Vendas.Domain.Commands;

public class CancelOrderReverseStockCommand: ICommand
{
    public Guid OrderId { get; set; }

    public CancelOrderReverseStockCommand(Guid orderId)
    {
        OrderId = orderId;
        AggregateId = orderId;
    }
}