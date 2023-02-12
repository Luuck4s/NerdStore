using System.Diagnostics.CodeAnalysis;

namespace NerdStore.Core.Entities;

[ExcludeFromCodeCoverage]
public abstract class Entity
{
    public Guid Id { get; private set; }

    protected Entity(Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
    }

    public override string ToString()
    {
        return $"{GetType().Namespace} [Id={Id}]";
    }
}