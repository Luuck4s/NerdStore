using MediatR;

namespace NerdStore.Core.Notification;

public class DomainNotification: Messages.Message, INotification
{
    public DateTime Timestamp { get; private set; }
    public Guid DomainNotificationId { get; private set; }
    public string Key { get; private set; }
    public string Value { get; private set; }
    public int Version { get; private set; }

    public DomainNotification(string key, string value, Guid aggregateId)
    {
        Timestamp = DateTime.Now;
        DomainNotificationId = Guid.NewGuid();
        Version = 1;
        Key = key;
        Value = value;
        AggregateId = aggregateId;
    }
}