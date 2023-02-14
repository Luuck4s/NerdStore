using NerdStore.Vendas.Domain.Enums;

namespace NerdStore.Vendas.Api.Contracts.Response.Voucher;

public class VoucherResponse
{
    public string Code { get;  set; }
    public decimal? Percent { get;  set; }
    public decimal? DiscountAmount { get;  set; }
    public int Quantity { get;  set; }
    public VoucherDiscountType VoucherDiscountType { get;  set; }
}