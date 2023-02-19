using NerdStore.SagaBus.Core.Messages.IntegrationEvents;
using NerdStore.SagaBus.Payment.Commands;
using Rebus.Bus;
using Rebus.Handlers;

namespace NerdStore.SagaBus.Payment.CommandHandlers
{
    public class PaymentCommandHandler :
        IHandleMessages<MakePaymentCommand>
    {
        private readonly IBus _bus;

        public PaymentCommandHandler(IBus bus)
        {
            _bus = bus;
        }

        public Task Handle(MakePaymentCommand message)
        {
            if (Caos.MakePayment())
            {
                _bus.Publish(new PaymentSuccessful() { AggregateRoot = message.AggregateRoot }).Wait();
                return Task.CompletedTask;
            }

            _bus.Publish(new PaymentRefused() { AggregateRoot = message.AggregateRoot }).Wait();
            return Task.CompletedTask;
        }

        public class Caos
        {
            public static bool MakePayment()
            {
                return new Random().NextDouble() >= 0.5;
            }
        }
    }
}