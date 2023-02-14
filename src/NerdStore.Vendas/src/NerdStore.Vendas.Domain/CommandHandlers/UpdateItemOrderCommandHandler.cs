using MediatR;
using NerdStore.Vendas.Domain.Commands;

namespace NerdStore.Vendas.Domain.CommandHandlers;

public class UpdateItemOrderCommandHandler: IRequestHandler<UpdateItemOrderCommand, bool>
{
    public Task<bool> Handle(UpdateItemOrderCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}