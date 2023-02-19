using NerdStore.Core.Data;
using NerdStore.Pagamentos.Business;
using NerdStore.Pagamentos.Business.Repositories;
using NerdStore.Pagamentos.Data.Context;

namespace NerdStore.Pagamentos.Data.Repositories;

public class PaymentRepository: IPaymentRepository
{
    private readonly PaymentContext _context;
    public IUnitOfWork UnitOfWork => _context;

    public PaymentRepository(PaymentContext context)
    {
        _context = context;
    }
    
    public void Add(Payment payment)
    {
        _context.Payments.Add(payment);
    }

    public void AddTransaction(Transaction transaction)
    {
        _context.Transactions.Add(transaction);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}