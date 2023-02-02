using MediatR;
using NerdStore.Core.Message;

namespace NerdStore.Core.Events;

public abstract class IEvent: IMessage, INotification
{
    public DateTime Timestamp { get; init; }

    protected IEvent()
    {
        Timestamp = DateTime.Now;
    }
}