using NerdStore.Core.Entities;
using NerdStore.Core.Interfaces;

namespace NerdStore.Pagamentos.Business;

public class Payment: Entity, IAggregateRoot
{
    public Guid OrderId { get; set; }
    public Guid ClientId { get; set; }
    public string Status { get; set; }
    public decimal Amount { get; set; }
    public string CarNumber { get; set; }
    public DateTime CardExpiration { get;  set; }
    public string CardCvv { get;  set; }

    public Transaction Transaction { get; set; }

    public void ChangeStatus(string status)
    {
        Status = status;
    }
}