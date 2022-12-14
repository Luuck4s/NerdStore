using System.Diagnostics.CodeAnalysis;
using MediatR;

namespace NerdStore.Core.Messages;

[ExcludeFromCodeCoverage]
public abstract class Event: Message, INotification
{
    public DateTime Timestamp { get; private set; }

    protected Event()
    {
        Timestamp = DateTime.Now;
    }
}