using MediatR;
using Microsoft.Extensions.Logging;
using NerdStore.Catalogo.Domain.Events;
using NerdStore.Catalogo.Domain.Repositories;

namespace NerdStore.Catalogo.Domain.EventHandlers;

public class ProductEventHandler: INotificationHandler<LowStockProductEvent>
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<ProductEventHandler> _logger;

    public ProductEventHandler(IProductRepository productRepository, ILogger<ProductEventHandler> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }
    
    public async Task Handle(LowStockProductEvent notification, CancellationToken cancellationToken)
    {
        var produto = await _productRepository.GetById(notification.AggregateId);

        _logger.LogInformation($"Produto {produto!.Id} - {produto.Name} abaixo do estoque");
    }
}