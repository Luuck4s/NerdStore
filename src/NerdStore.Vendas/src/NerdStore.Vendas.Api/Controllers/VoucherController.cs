using Microsoft.AspNetCore.Mvc;
using NerdStore.Core.Contracts.Results;
using NerdStore.Core.EventHandler;
using NerdStore.Vendas.Api.Contracts.Requests.Voucher;
using NerdStore.Vendas.Domain.Commands;

namespace NerdStore.Vendas.Api.Controllers;

[ApiController]
[Route("v1/voucher")]
public class VoucherController: ControllerBase
{
    private readonly IMediatRHandler _mediator;

    public VoucherController(IMediatRHandler mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("{orderId}")]
    public async Task<GenericCommandResult> AddItem(Guid orderId, AddVoucherRequest request)
    {
        var command = new AddVoucherCommand(request.ClientId, orderId, request.VoucherCode);
        await _mediator.PublishCommand(command);

        return new(command.AggregateId);
    }
}