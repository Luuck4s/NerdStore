using NerdStore.Core.Commands;

namespace NerdStore.Vendas.Domain.Commands;

public class AddVoucherCommand: ICommand
{
    public Guid ClientId { get; private set; }
    public Guid OrderId { get; private set; }
    public string VoucherCode { get; private set; }

    public AddVoucherCommand(Guid clientId, Guid orderId, string voucherCode)
    {
        ClientId = clientId;
        OrderId = orderId;
        VoucherCode = voucherCode;
    }
}