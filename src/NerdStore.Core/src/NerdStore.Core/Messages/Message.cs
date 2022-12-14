using System.Diagnostics.CodeAnalysis;

namespace NerdStore.Core.Messages;

[ExcludeFromCodeCoverage]
public abstract class Message
{
    public string MessageType { get; protected set; }
    public Guid AggregateId { get; protected set; }

    protected Message()
    {
        MessageType = GetType().Name;
    }
}