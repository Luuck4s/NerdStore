using NerdStore.Core.Messages;

namespace NerdStore.Core.Data.EventSourcing;

public interface IEventSourcingRepository
{
    Task SaveEvent<TEvent>(TEvent @event) where TEvent : Event;
    Task<IEnumerable<StoredEvent>> GetEvents(Guid aggregateId);
}