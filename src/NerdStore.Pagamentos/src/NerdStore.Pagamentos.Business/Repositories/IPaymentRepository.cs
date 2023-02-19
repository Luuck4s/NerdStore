using NerdStore.Core.Data;

namespace NerdStore.Pagamentos.Business.Repositories;

public interface IPaymentRepository: IRepository<Payment>
{
    void Add(Payment payment);
    void AddTransaction(Transaction transaction);
}