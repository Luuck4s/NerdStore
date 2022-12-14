using MediatR;
using NerdStore.Core.Messages;

namespace NerdStore.Core.EventHandler;

public class MediatRHandler: IMediatRHandler
{
    private readonly IMediator _mediator;

    public MediatRHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task PublicarEvento<T>(T evento) where T : Event
    {
        await _mediator.Publish(evento);
    }
}