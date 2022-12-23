using System.Diagnostics.CodeAnalysis;
using Flunt.Notifications;

namespace NerdStore.Core.ValueObjects;

[ExcludeFromCodeCoverage]
public abstract class ValueObject: Notifiable<Notification>
{
    public abstract void Validate();
}