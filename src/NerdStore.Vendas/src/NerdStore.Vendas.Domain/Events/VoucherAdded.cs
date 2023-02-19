using NerdStore.Core.Messages;

namespace NerdStore.Vendas.Domain.Events;

public class VoucherAdded : Event
{
    public Guid VoucherId { get; set; }

    public VoucherAdded(Guid aggregateId, Guid voucherId)
    {
        VoucherId = voucherId;
        AggregateId = aggregateId;
    }
}