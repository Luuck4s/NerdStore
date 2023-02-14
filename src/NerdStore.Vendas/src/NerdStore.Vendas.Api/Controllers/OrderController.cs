using Microsoft.AspNetCore.Mvc;
using NerdStore.Core.Contracts.Results;
using NerdStore.Core.EventHandler;
using NerdStore.Vendas.Api.Contracts.Order;
using NerdStore.Vendas.Api.Contracts.Requests.Order;
using NerdStore.Vendas.Api.Queries;
using NerdStore.Vendas.Domain.Commands;

namespace NerdStore.Vendas.Api.Controllers;

[ApiController]
[Route("v1/order")]
public class OrderController: ControllerBase
{
    private readonly IMediatRHandler _mediator;
    private readonly IOrderQueries _orderQueries;

    public OrderController(IMediatRHandler mediator, IOrderQueries orderQueries)
    {
        _mediator = mediator;
        _orderQueries = orderQueries;
    }
    
    [HttpGet("get-all")]
    public async Task<List<OrderResponse>> GetAllOrders()
    {
        return await _orderQueries.GetAllOrders();
    }
    
    [HttpGet("{id}")]
    public async Task<OrderResponse> GetOrder(Guid id)
    {
        return await _orderQueries.GetOrder(id);
    }
    
    [HttpPost()]
    public async Task<GenericCommandResult> CreateOrder(CreateOrderRequest request)
    {
        var command = new CreateOrderCommand(request.ClientId);
        await _mediator.PublishCommand(command);

        return new(command.AggregateId);
    }
}