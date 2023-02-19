namespace NerdStore.Core.Dtos;

public class PaymentOrderDto
{
    public Guid OrderId { get; set; }
    public Guid ClientId { get; set; }
    public decimal Amount { get; set; }
    public string CarNumber { get; set; } = string.Empty;
    public DateTime CardExpiration { get;  set; }
    public string CardCvv { get;  set; } = string.Empty;

}