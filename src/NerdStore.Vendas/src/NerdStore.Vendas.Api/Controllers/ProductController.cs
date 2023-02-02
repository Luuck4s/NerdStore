using MediatR;
using Microsoft.AspNetCore.Mvc;
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

    [HttpGet("/add-item")]
    public Task AddItem()
    {
        var command = new AddItemOrderCommand(Guid.Empty, Guid.Empty, "", 1, 1);
        _mediator.Send(command);
        return Task.CompletedTask;
    }
}