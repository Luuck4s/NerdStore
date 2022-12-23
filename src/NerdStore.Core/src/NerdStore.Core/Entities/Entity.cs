using System.Diagnostics.CodeAnalysis;
using Flunt.Notifications;

namespace NerdStore.Core.Entities;

[ExcludeFromCodeCoverage]
public abstract class Entity: Notifiable<Notification>
{
    public Guid Id { get; private set; }

    protected Entity()
    {
        Id = Guid.NewGuid();
    }

    public abstract void Validate();

    public override string ToString()
    {
        return $"{GetType().Namespace} [Id={Id}]";
    }
}