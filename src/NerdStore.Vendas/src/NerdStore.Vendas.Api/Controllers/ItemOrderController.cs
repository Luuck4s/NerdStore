using Microsoft.AspNetCore.Mvc;
using NerdStore.Core.Contracts.Results;
using NerdStore.Core.EventHandler;
using NerdStore.Vendas.Api.Contracts.Requests.Product;
using NerdStore.Vendas.Domain.Commands;

namespace NerdStore.Vendas.Api.Controllers;

[ApiController]
[Route("v1/item-order")]
public class ItemOrderController: ControllerBase
{
    private readonly IMediatRHandler _mediator;

    public ItemOrderController(IMediatRHandler mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("{orderId}/add")]
    public async Task<GenericCommandResult> AddItem(Guid orderId, AddItemOrderRequest request)
    {
        var command = new AddItemOrderCommand(request.ClientId, request.ProductId, request.Name, request.Quantity, request.UnitAmount, orderId);
        await _mediator.PublishCommand(command);

        return new(command.AggregateId);
    }
}