namespace NerdStore.Vendas.Api.Contracts.Requests.Voucher;

public class AddVoucherRequest
{
    public Guid ClientId { get;  set; }
    public string VoucherCode { get;  set; }
}