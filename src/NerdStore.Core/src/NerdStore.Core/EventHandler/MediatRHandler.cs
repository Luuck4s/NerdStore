using System.Diagnostics.CodeAnalysis;
using MediatR;
using NerdStore.Core.Commands;
using NerdStore.Core.Messages;
using NerdStore.Core.Notification;

namespace NerdStore.Core.EventHandler;

[ExcludeFromCodeCoverage]
public class MediatRHandler: IMediatRHandler
{
    private readonly IMediator _mediator;

    public MediatRHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task PublishEvent<T>(T evento) where T : Event
    {
        await _mediator.Publish(evento);
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