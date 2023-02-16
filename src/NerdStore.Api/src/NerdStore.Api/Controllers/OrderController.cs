using Microsoft.AspNetCore.Mvc;
using NerdStore.Api.Contracts.Requests.Order;
using NerdStore.Api.Contracts.Response.Order;
using NerdStore.Api.Queries;
using NerdStore.Core.Contracts.Results;
using NerdStore.Core.EventHandler;
using NerdStore.Vendas.Domain.Commands;

namespace NerdStore.Api.Controllers;

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
    
    [HttpPost]
    public async Task<GenericCommandResult> CreateOrder(CreateOrderRequest request)
    {
        var command = new CreateOrderCommand(request.ClientId);
        await _mediator.PublishCommand(command);

        return new(command.AggregateId);
    }
    
    [HttpPost("{orderId}/start")]
    public async Task<IActionResult> StartOder(Guid orderId, StartOrderRequest request)
    {
        var command = new StartOrderCommand(orderId, request.ClientId, request.CarNumber, request.CardExpiration, request.CardCvv);
        await _mediator.PublishCommand(command);

        return Ok();
    }
}