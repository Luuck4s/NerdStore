using MediatR;
using NerdStore.Vendas.Domain.Commands;

namespace NerdStore.Vendas.Domain.CommandHandlers;

public class AddItemOrderCommandHandler: IRequestHandler<AddItemOrderCommand, bool>
{
    public async Task<bool> Handle(AddItemOrderCommand request, CancellationToken cancellationToken)
    {
        return true;
    }
}