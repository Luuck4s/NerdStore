using NerdStore.Vendas.Domain.Entities;

namespace NerdStore.Vendas.Domain.Repository;

public interface IVoucherRepository
{
    Task<Voucher?> GetVoucherByCode(string code);
}