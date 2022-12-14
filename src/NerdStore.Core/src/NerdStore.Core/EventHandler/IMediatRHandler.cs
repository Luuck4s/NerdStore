using NerdStore.Core.Messages;

namespace NerdStore.Core.EventHandler;

public interface IMediatRHandler
{
    Task PublicarEvento<T>(T evento) where T : Event;
}