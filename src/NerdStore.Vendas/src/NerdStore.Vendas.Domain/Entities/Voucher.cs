using System.Text.Json.Serialization;
using NerdStore.Core.Entities;
using NerdStore.Vendas.Domain.Enums;

namespace NerdStore.Vendas.Domain.Entities;

public class Voucher : Entity
{
    public string Code { get;  set; }
    public decimal? Percent { get;  set; }
    public decimal? DiscountAmount { get;  set; }
    public int Quantity { get;  set; }
    public VoucherDiscountType VoucherDiscountType { get;  set; }
    public DateTime CreatedAt { get;  set; }
    public DateTime? UsedAt { get;  set; }
    public DateTime? ValidUntil { get;  set; }
    public bool IsActive { get;  set; }
    public bool IsUsed { get;  set; }
    public ICollection<Order>? Orders { get; set; }

    public Voucher()
    {
        Code = Guid.NewGuid().ToString().Substring(0, 10);
    }

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

    public bool IsAppliable()
    {
        if (ValidUntil < DateTime.Today)
        {
            return false;
        }

        if (Quantity <= 0)
        {
            return false;
        }

        if (IsActive == false)
        {
            return false;
        }

        return !IsUsed;
    }
}