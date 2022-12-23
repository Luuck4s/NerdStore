using System.Diagnostics.CodeAnalysis;
using MediatR;
using NerdStore.Core.Messages;

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
}