using NerdStore.Core.Commands;
using NerdStore.Core.Messages;
using NerdStore.Core.Notification;

namespace NerdStore.Core.EventHandler;

public interface IMediatRHandler
{
    Task PublishEvent<T>(T @event) where T : Event;
    Task PublishCommand<T>(T command) where T : ICommand;
    Task PublishNotification<T>(T notification) where T : DomainNotification;
}