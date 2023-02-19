using MediatR;
using Microsoft.Extensions.Logging;
using NerdStore.Catalogo.Domain.Events;
using NerdStore.Catalogo.Domain.Repositories;
using NerdStore.Catalogo.Domain.Services;
using NerdStore.Core.Events.IntegrationEvents.Order;

namespace NerdStore.Catalogo.Domain.EventHandlers;

public class ProductEventHandler: INotificationHandler<LowStockProductEvent>, INotificationHandler<OrderPaymentRejected>
{
    private readonly IProductRepository _productRepository;
    private readonly IStockService _stockService;
    private readonly ILogger<ProductEventHandler> _logger;

    public ProductEventHandler(IProductRepository productRepository, ILogger<ProductEventHandler> logger, IStockService stockService)
    {
        _productRepository = productRepository;
        _logger = logger;
        _stockService = stockService;
    }
    
    public async Task Handle(LowStockProductEvent notification, CancellationToken cancellationToken)
    {
        var produto = await _productRepository.GetById(notification.AggregateId);

        _logger.LogInformation($"Produto {produto!.Id} - {produto.Name} abaixo do estoque");
    }

    public async Task Handle(OrderPaymentRejected notification, CancellationToken cancellationToken)
    {
        await _stockService.AddListItemStock(notification.ItemOrderList);
    }
}