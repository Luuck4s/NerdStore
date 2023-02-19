using Microsoft.AspNetCore.Mvc;
using NerdStore.SagaBus.Order.Comands;
using Rebus.Bus;

namespace NerdStore.SagaBus.Api.Controllers;

[ApiController]
public class HomeController : ControllerBase
{
    private readonly IBus _bus;

    public HomeController(IBus bus)
    {
        _bus = bus;
    }

    [HttpGet("start-order")]
    public IActionResult Index()
    {
        _bus.Send(new StartOrderCommand { AggregateRoot = Guid.NewGuid()}).Wait();

        return Ok();
    }
}