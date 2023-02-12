using System.Diagnostics.CodeAnalysis;
using NerdStore.Core.Messages;

namespace NerdStore.Core.Entities;

[ExcludeFromCodeCoverage]
public abstract class Entity
{
    public Guid Id { get; private set; }
    private List<Event> _events;
    public IReadOnlyCollection<Event> Events => _events.AsReadOnly();

    protected Entity(Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        _events = new();
    }

    public void AddEvent(Event @event)
    {
        _events.Add(@event);
    }
    
    public void ClearEvents()
    {
        _events.Clear();
    }

    public override string ToString()
    {
        return $"{GetType().Namespace} [Id={Id}]";
    }
}