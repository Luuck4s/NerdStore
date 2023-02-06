namespace NerdStore.Core.Message;

public abstract class IMessage
{
    public Guid AggregateId { get; protected  set; }
    public string MessageType { get; protected set; }

    protected IMessage()
    {
        MessageType = GetType().Name;
        AggregateId = Guid.NewGuid();
    }
}