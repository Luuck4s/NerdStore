using NerdStore.Core.Dtos;

namespace NerdStore.Pagamentos.Business.Services;

public interface IPaymentService
{
    Task<Transaction> MakePaymentOrder(PaymentOrderDto paymentOrderDto);
}