using System.Diagnostics.CodeAnalysis;
using MediatR;
using NerdStore.Core.Commands;
using NerdStore.Core.Data.EventSourcing;
using NerdStore.Core.Events;
using NerdStore.Core.Messages;
using NerdStore.Core.Notification;

namespace NerdStore.Core.EventHandler;

[ExcludeFromCodeCoverage]
public class MediatRHandler: IMediatRHandler
{
    private readonly IMediator _mediator;
    private readonly IEventSourcingRepository _eventSourcingRepository;

    public MediatRHandler(IMediator mediator, IEventSourcingRepository eventSourcingRepository)
    {
        _mediator = mediator;
        _eventSourcingRepository = eventSourcingRepository;
    }

    public async Task PublishEvent<T>(T @event) where T : Event
    {
        if (@event is not DomainEvent)
        {
            await _eventSourcingRepository.SaveEvent(@event);
        }

        await _mediator.Publish(@event);
    }

    public async Task PublishCommand<T>(T command) where T : ICommand
    {
        await _mediator.Send(command);
    }

    public async Task PublishNotification<T>(T notification) where T : DomainNotification
    {
        await _mediator.Publish(notification);
    }
}