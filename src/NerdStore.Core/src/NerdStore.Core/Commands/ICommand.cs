using MediatR;
using NerdStore.Core.Message;

namespace NerdStore.Core.Commands;

public abstract class ICommand: IMessage, IRequest<bool>
{
    public DateTime Timestamp { get; init; }

    protected ICommand()
    {
        Timestamp = DateTime.Now;
    }
}