using System.Text;
using EventSourcing.Services;
using EventStore.ClientAPI;
using NerdStore.Core.Data.EventSourcing;
using NerdStore.Core.Messages;
using Newtonsoft.Json;

namespace EventSourcing.Repository;

public class EventSourcingRepository: IEventSourcingRepository
{
    private readonly IEventStoreService _eventStore;

    public EventSourcingRepository(IEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task SaveEvent<TEvent>(TEvent @event) where TEvent : Event
    {
        await _eventStore.GetConnection().AppendToStreamAsync(
            @event.AggregateId.ToString(),
            ExpectedVersion.Any,
            FormatData(@event)
        );
    }

    public async Task<IEnumerable<StoredEvent>> GetEvents(Guid aggregateId)
    {
        var aggregateEvents = await _eventStore
            .GetConnection()
            .ReadStreamEventsForwardAsync(aggregateId.ToString(), 0, 500, false);

        var eventsList = new List<StoredEvent>();

        foreach (var resolvedEvent in aggregateEvents.Events)
        {
            var dataEncoded = Encoding.UTF8.GetString(resolvedEvent.Event.Data);
            var jsonData = JsonConvert.DeserializeObject<BaseEvent>(dataEncoded);

            var @event = new StoredEvent(
                resolvedEvent.Event.EventId,
                resolvedEvent.Event.EventType,
                jsonData.Timestamp,
                dataEncoded
            );

            eventsList.Add(@event);
        }

        return eventsList;
    }

    private static IEnumerable<EventData> FormatData<TEvent>(TEvent @event) where TEvent : Event
    {
        yield return new EventData(
            Guid.NewGuid(),
            @event.MessageType,
            true,
            Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event)),
            null
        );
    }

    internal class BaseEvent
    {
        public DateTime Timestamp { get; set; }
    }
}