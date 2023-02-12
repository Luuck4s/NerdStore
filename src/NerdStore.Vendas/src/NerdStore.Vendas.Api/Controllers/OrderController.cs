using Microsoft.AspNetCore.Mvc;
using NerdStore.Core.Contracts.Results;
using NerdStore.Core.EventHandler;
using NerdStore.Vendas.Api.Requests.Order;
using NerdStore.Vendas.Api.Requests.Product;
using NerdStore.Vendas.Domain.Commands;
using NerdStore.Vendas.Domain.Entities;
using NerdStore.Vendas.Domain.Repository;

namespace NerdStore.Vendas.Api.Controllers;

[ApiController]
[Route("v1/order")]
public class OrderController: ControllerBase
{
    private readonly IMediatRHandler _mediator;
    private readonly IOrderRepository _orderRepository;

    public OrderController(IMediatRHandler mediator, IOrderRepository orderRepository)
    {
        _mediator = mediator;
        _orderRepository = orderRepository;
    }
    
    [HttpGet("/get-all")]
    public async Task<List<Order>> GetAllOrders()
    {
        return await _orderRepository.GetAllOrders();
    }
    
    [HttpGet("/get-order/{id}")]
    public async Task<Order> GetOrder(Guid id)
    {
        return await _orderRepository.GetOrder(id);
    }
    
    [HttpPost("/create-order")]
    public async Task<GenericCommandResult> CreateOrder(CreateOrderRequest request)
    {
        var command = new CreateOrderCommand(request.ClientId);
        await _mediator.PublishCommand(command);

        return new(command.AggregateId);
    }

    [HttpPost("/{orderId}/add-item")]
    public async Task<GenericCommandResult> AddItem(Guid orderId, AddItemOrderRequest request)
    {
        var command = new AddItemOrderCommand(request.ClientId, request.ProductId, request.Name, request.Quantity, request.UnitAmount, orderId);
        await _mediator.PublishCommand(command);

        return new(command.AggregateId);
    }
}