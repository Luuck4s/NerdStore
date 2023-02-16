namespace NerdStore.Api.Contracts.Requests.Order;

public class StartOrderRequest
{
    public Guid ClientId { get;  set; }
    public string CarNumber { get; set; } = string.Empty;
    public DateTime CardExpiration { get;  set; }
    public string CardCvv { get;  set; } = string.Empty;
}