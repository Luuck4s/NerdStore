using NerdStore.Core.Commands;
using NerdStore.Core.Messages;

namespace NerdStore.Core.EventHandler;

public interface IMediatRHandler
{
    Task PublishEvent<T>(T evento) where T : Event;
    Task PublishCommand<T>(T command) where T : ICommand;
}