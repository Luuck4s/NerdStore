using NerdStore.SagaBus.Core.Messages.IntegrationEvents;
using NerdStore.SagaBus.Payment.Commands;
using Rebus.Bus;
using Rebus.Handlers;

namespace NerdStore.SagaBus.Payment.EventHandlers
{
    public class PaymentEventHandler :
        IHandleMessages<OrderStarted>
    {
        private readonly IBus _bus;

        public PaymentEventHandler(IBus bus)
        {
            _bus = bus;
        }

        public Task Handle(OrderStarted message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("REALIZANDO PAGAMENTO!");
            Console.ForegroundColor = ConsoleColor.Black;

            _bus.Send(new MakePaymentCommand() { AggregateRoot = message.AggregateRoot }).Wait();

            return Task.CompletedTask;
        }
    }
}