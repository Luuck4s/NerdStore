using NerdStore.Core.Messages;

namespace NerdStore.Core.EventHandler;

public interface IMediatRHandler
{
    Task PublishEvent<T>(T evento) where T : Event;
}