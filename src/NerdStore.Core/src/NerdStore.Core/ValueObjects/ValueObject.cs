using System.Diagnostics.CodeAnalysis;

namespace NerdStore.Core.ValueObjects;

[ExcludeFromCodeCoverage]
public abstract class ValueObject
{
    public Guid Id { get; private set; }

    public ValueObject()
    {
        Id = Guid.NewGuid();
    }
}