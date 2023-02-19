using Microsoft.AspNetCore.Mvc;
using NerdStore.Api.Contracts.Requests.ItemOrder;
using NerdStore.Api.Contracts.Requests.Product;
using NerdStore.Core.Contracts.Results;
using NerdStore.Core.EventHandler;
using NerdStore.Vendas.Domain.Commands;

namespace NerdStore.Api.Controllers;

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
    
    [HttpPut("{orderId}/update")]
    public async Task<GenericCommandResult> UpdateItem(Guid orderId, UpdateItemOrderRequest request)
    {
        var command = new UpdateItemOrderCommand(request.ClientId, orderId, request.ProductId, request.Quantity);
        await _mediator.PublishCommand(command);

        return new(command.AggregateId);
    }
    
    [HttpDelete("{orderId}/delete")]
    public async Task<GenericCommandResult> DeleteItem(Guid orderId, DeleteItemOrderRequest request)
    {
        var command = new DeleteItemOrderCommand(request.ClientId, orderId, request.ProductId);
        await _mediator.PublishCommand(command);

        return new(command.AggregateId);
    }
}