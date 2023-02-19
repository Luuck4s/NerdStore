using NerdStore.Core.Dtos;
using NerdStore.Core.EventHandler;
using NerdStore.Core.Events.IntegrationEvents.Payment;
using NerdStore.Pagamentos.Business.Enums;
using NerdStore.Pagamentos.Business.Facades;
using NerdStore.Pagamentos.Business.Repositories;
using NerdStore.Pagamentos.Business.ValuesObjects;

namespace NerdStore.Pagamentos.Business.Services;

public class PaymentService: IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IPaymentCreditCardFacade _paymentCreditCard;
    private readonly IMediatRHandler _mediatRHandler;

    public PaymentService(IMediatRHandler mediatRHandler, IPaymentCreditCardFacade paymentCreditCard, IPaymentRepository paymentRepository)
    {
        _mediatRHandler = mediatRHandler;
        _paymentCreditCard = paymentCreditCard;
        _paymentRepository = paymentRepository;
    }

    public async Task<Transaction> MakePaymentOrder(PaymentOrderDto paymentOrderDto)
    {
        var order = new Order()
        {
            Id = paymentOrderDto.OrderId,
            Amount = paymentOrderDto.Amount,
        };

        var payment = new Payment()
        {
            Amount = paymentOrderDto.Amount,
            CardCvv = paymentOrderDto.CardCvv,
            CarNumber = paymentOrderDto.CarNumber,
            CardExpiration = paymentOrderDto.CardExpiration,
            OrderId = paymentOrderDto.OrderId
        };

        var transaction = _paymentCreditCard.MakePayment(order, payment);

        if (transaction.StatusTransaction is StatusTransaction.Paid)
        {
            var @event = new PaymentSuccessful(order.Id, paymentOrderDto.ClientId, payment.Id, transaction.Id);
            payment.AddEvent(@event);

            _paymentRepository.Add(payment);
            _paymentRepository.AddTransaction(transaction);

            await _paymentRepository.UnitOfWork.Commit();
            return transaction;
        }

        await _mediatRHandler.PublishEvent(new PaymentRejected(order.Id, transaction.Id));
        return transaction;
    }
}