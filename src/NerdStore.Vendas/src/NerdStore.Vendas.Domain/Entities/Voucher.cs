using NerdStore.Core.Entities;
using NerdStore.Vendas.Domain.Enums;

namespace NerdStore.Vendas.Domain.Entities;

public class Voucher : Entity
{
    public string Code { get; private set; }
    public decimal? Percent { get; private set; }
    public decimal? DiscountAmount { get; private set; }
    public int Quantity { get; private set; }
    public VoucherDiscountType VoucherDiscountType { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UsedAt { get; private set; }
    public DateTime? ValidUntil { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsUsed { get; private set; }

    public ICollection<Order> Orders { get; set; }

    public decimal CalculateDiscount(decimal totalAmount)
    {
        decimal? discount;
        switch (VoucherDiscountType)
        {
            case VoucherDiscountType.Percentage:
                if (Percent is null) return totalAmount;

                discount = totalAmount * Percent / 100;
                break;
            case VoucherDiscountType.Amount:
                if (DiscountAmount is null) return totalAmount;

                discount = DiscountAmount;
                break;
            default:
                discount = 0;
                break;
        }

        var result = (decimal)(totalAmount - discount);
        return result < 0 ? 0 : result;
    }
}