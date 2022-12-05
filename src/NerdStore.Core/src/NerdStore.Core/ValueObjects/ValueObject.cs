using Flunt.Notifications;

namespace NerdStore.Core.ValueObjects;

public abstract class ValueObject: Notifiable<Notification>
{
    public abstract void Validar();
}