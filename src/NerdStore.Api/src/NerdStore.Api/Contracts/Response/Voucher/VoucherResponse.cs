namespace NerdStore.Api.Contracts.Response.Voucher;

public class VoucherResponse
{
    public string Code { get;  set; }
    public decimal? Percent { get;  set; }
    public decimal? DiscountAmount { get;  set; }
    public int Quantity { get;  set; }
    public string VoucherDiscountType { get;  set; }
}