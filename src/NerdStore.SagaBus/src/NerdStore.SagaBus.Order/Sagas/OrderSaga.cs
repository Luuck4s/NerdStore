using NerdStore.SagaBus.Core.Messages.IntegrationEvents;
using NerdStore.SagaBus.Order.Comands;
using NerdStore.SagaBus.Order.SagasData;
using Rebus.Bus;
using Rebus.Handlers;
using Rebus.Sagas;

namespace NerdStore.SagaBus.Order.Sagas;

public class OrderSaga: Saga<OrderSagaData>,
    IAmInitiatedBy<StartOrderCommand>,
    IHandleMessages<PaymentSuccessful>,
    IHandleMessages<OrderEnded>,
    IHandleMessages<PaymentRefused>,
    IHandleMessages<OrderCanceled>
{
     private readonly IBus _bus;

        public OrderSaga(IBus bus)
        {
            _bus = bus;
        }

        protected override void CorrelateMessages(ICorrelationConfig<OrderSagaData> config)
        {
            config.Correlate<StartOrderCommand>(m => m.AggregateRoot, d => d.Id);
            config.Correlate<PaymentSuccessful>(m => m.AggregateRoot, d => d.Id);
            config.Correlate<OrderEnded>(m => m.AggregateRoot, d => d.Id);
            config.Correlate<PaymentRefused>(m => m.AggregateRoot, d => d.Id);
            config.Correlate<OrderCanceled>(m => m.AggregateRoot, d => d.Id);
        }

        public Task Handle(StartOrderCommand message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Pedido Realizado!");
            Console.ForegroundColor = ConsoleColor.Black;

            _bus.Publish(new OrderStarted() { AggregateRoot = message.AggregateRoot }).Wait();
            Data.OrderStarted = true;

            SagaProcess();

            return Task.CompletedTask;
        }

        public Task Handle(PaymentSuccessful message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Pagamento Realizado!");
            Console.ForegroundColor = ConsoleColor.Black;

            _bus.Publish(new OrderEnded() { AggregateRoot = message.AggregateRoot }).Wait();
            Data.PaymentSuccessful = true;

            SagaProcess();

            return Task.CompletedTask;
        }

        public Task Handle(OrderEnded message)
        {
            Data.OrderEnded = true;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Pedido FINALIZADO!");
            Console.ForegroundColor = ConsoleColor.Black;

            SagaProcess();

            return Task.CompletedTask;
        }

        public Task Handle(PaymentRefused message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Pagamento Recusado!");
            Console.ForegroundColor = ConsoleColor.Black;

            _bus.Publish(new OrderCanceled() { AggregateRoot = message.AggregateRoot }).Wait();
            Data.PaymentSuccessful = false;

            SagaProcess();

            return Task.CompletedTask;
        }

        public Task Handle(OrderCanceled message)
        {
            Data.OrderCanceled = true;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Pedido CANCELADO!");
            Console.ForegroundColor = ConsoleColor.Black;

            SagaProcess();

            return Task.CompletedTask;
        }

        private void SagaProcess()
        {
            if (Data.CompletedSaga)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Finalizando Saga!");
                Console.ForegroundColor = ConsoleColor.Black;

                MarkAsComplete();
            }
        }
}