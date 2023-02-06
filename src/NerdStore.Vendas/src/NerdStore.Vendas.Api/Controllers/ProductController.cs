using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Core.Contracts.Results;
using NerdStore.Vendas.Api.Requests.Product;
using NerdStore.Vendas.Domain.Commands;

namespace NerdStore.Vendas.Api.Controllers;

[ApiController]
public class ProductController: ControllerBase
{
    private IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("/add-item")]
    public async Task<GenericCommandResult> AddItem(AddItemOrderRequest request)
    {
        var command = new AddItemOrderCommand(request.ClientId, request.ProductId, request.Name, request.Quantity, request.UnitAmount);
        await _mediator.Send(command);

        return new(command.AggregateId);
    }
}