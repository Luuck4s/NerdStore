using NerdStore.Pagamentos.AntiCorruption.Configuration;
using NerdStore.Pagamentos.AntiCorruption.Gateway;
using NerdStore.Pagamentos.Business;
using NerdStore.Pagamentos.Business.Enums;
using NerdStore.Pagamentos.Business.Facades;
using NerdStore.Pagamentos.Business.ValuesObjects;

namespace NerdStore.Pagamentos.AntiCorruption.Facades;

public class PaymentCreditCardFacade: IPaymentCreditCardFacade
{
    private readonly ConfigManager _configManager;
    private readonly IPagarmeGateway _pagarmeGateway;

    public PaymentCreditCardFacade(ConfigManager configManager, IPagarmeGateway pagarmeGateway)
    {
        _configManager = configManager;
        _pagarmeGateway = pagarmeGateway;
    }

    public Transaction MakePayment(Order order, Payment payment)
    {
        var apiKey = _configManager.GetValue("apiKey");
        var encryptionKey = _configManager.GetValue("encryptionKey");

        var serviceKey = _pagarmeGateway.GetPagarmeServiceKey(apiKey, encryptionKey);
        var cardHashKey = _pagarmeGateway.GetCardHashKey(serviceKey, payment.CarNumber);

        var paymentResult = _pagarmeGateway.CommitTransaction(cardHashKey, order.Id.ToString(), payment.Amount);

        var transaction = new Transaction
        {
            OrderId = order.Id,
            Total = order.Amount,
            PaymentId = payment.Id,
            StatusTransaction = paymentResult
                ? StatusTransaction.Paid
                : StatusTransaction.Refused
        };

        return transaction;
    }
}