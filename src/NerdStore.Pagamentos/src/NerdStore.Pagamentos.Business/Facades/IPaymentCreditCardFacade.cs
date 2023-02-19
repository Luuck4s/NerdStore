using NerdStore.Pagamentos.Business.ValuesObjects;

namespace NerdStore.Pagamentos.Business.Facades;

public interface IPaymentCreditCardFacade
{
    Transaction MakePayment(Order order, Payment payment);
}