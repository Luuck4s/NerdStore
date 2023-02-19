using NerdStore.SagaBus.Core.Messages.IntegrationEvents;
using Rebus.Handlers;

namespace NerdStore.SagaBus.Order.EventHandlers;

public class OrderEventHandler: IHandleMessages<OrderStarted>
{
    public Task Handle(OrderStarted message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("PEGUEI EM OUTRO LUGAR");
        Console.ForegroundColor = ConsoleColor.Black;
        return Task.CompletedTask;
    }
}