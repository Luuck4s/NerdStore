using NerdStore.Core.Entities;
using NerdStore.Pagamentos.Business.Enums;

namespace NerdStore.Pagamentos.Business;

public class Transaction: Entity
{
    public Guid OrderId { get; set; }
    public Guid PaymentId { get; set; }
    public decimal Total { get; set; }
    public StatusTransaction StatusTransaction { get; set; }

    public Payment Payment { get; set; }
}