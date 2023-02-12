using NerdStore.Core.Commands;

namespace NerdStore.Vendas.Domain.Commands;

public class CreateOrderCommand: ICommand
{
    public Guid ClientId { get;  set; }

    public CreateOrderCommand(Guid clientId)
    {
        ClientId = clientId;
    }
}