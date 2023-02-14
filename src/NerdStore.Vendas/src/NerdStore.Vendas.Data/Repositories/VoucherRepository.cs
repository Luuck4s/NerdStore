using Microsoft.EntityFrameworkCore;
using NerdStore.Core.Data;
using NerdStore.Vendas.Data.Contexts;
using NerdStore.Vendas.Domain.Entities;
using NerdStore.Vendas.Domain.Repository;

namespace NerdStore.Vendas.Data.Repositories;

public class VoucherRepository: IVoucherRepository
{
    
    private readonly VendasContext _context;
    public IUnitOfWork UnitOfWork => _context;

    public VoucherRepository(VendasContext context)
    {
        _context = context;
    }
    
    public async Task<Voucher?> GetVoucherByCode(string code)
    {
        return await _context.Vouchers.FirstOrDefaultAsync(x => x.Code == code);
    }
}