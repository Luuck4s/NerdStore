using NerdStore.SagaBus.Core.Messages;
using NerdStore.SagaBus.Order.Comands;
using NerdStore.SagaBus.Order.Sagas;
using NerdStore.SagaBus.Payment.CommandHandlers;
using NerdStore.SagaBus.Payment.Commands;
using Rebus.Config;
using Rebus.Persistence.InMem;
using Rebus.Routing.TypeBased;
using Rebus.Transport.InMem;

namespace NerdStore.SagaBus.Api.Configuration;

public static class ServicesCollectionExtensions
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        var nomeFila = "fila_rebus";

        services.AddRebus(configure => configure
            .Transport(t => t.UseInMemoryTransport(new InMemNetwork(), nomeFila))
            //.Transport(t => t.UseRabbitMq("amqp://localhost", nomeFila))
            .Subscriptions(s => s.StoreInMemory())
            .Routing(r =>
            {
                r.TypeBased()
                    .MapAssemblyOf<Message>(nomeFila)
                    .MapAssemblyOf<StartOrderCommand>(nomeFila)
                    .MapAssemblyOf<MakePaymentCommand>(nomeFila);
            })
            .Sagas(s => s.StoreInMemory())
            .Options(o =>
            {
                o.SetNumberOfWorkers(1);
                o.SetMaxParallelism(1);
                o.SetBusName("Demo Rebus");
            })
        );

        // Register handlers 
        services.AutoRegisterHandlersFromAssemblyOf<PaymentCommandHandler>();
        services.AutoRegisterHandlersFromAssemblyOf<OrderSaga>();
    }
}