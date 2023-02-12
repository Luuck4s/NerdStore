using System.Diagnostics.CodeAnalysis;
using NerdStore.Catalogo.Domain.Events;
using NerdStore.Catalogo.Domain.Repositories;
using NerdStore.Core.EventHandler;

namespace NerdStore.Catalogo.Domain.Services;

public class StockService: IStockService
{
    private readonly IProductRepository _productRepository;
    private readonly IMediatRHandler _mediatR;

    public StockService(IProductRepository productRepository, IMediatRHandler mediatR)
    {
        _productRepository = productRepository;
        _mediatR = mediatR;
    }

    public async Task<bool> DebitStock(Guid productId, int quantity)
    {
        var product = await _productRepository.GetById(productId);

        if (product!.HasStock(quantity) is false)
        {
            return false;
        }
        
        product.DebitStock(quantity);

        if (product.QuantityStock < 10)
        {
            var @event = new LowStockProductEvent(product.Id, product.QuantityStock);
            await _mediatR.PublishEvent(@event);
        }

        _productRepository.Update(product);
        return await _productRepository.UnitOfWork.Commit();
    }

    public async Task<bool> AddStock(Guid productId, int quantity)
    {
        var product = await _productRepository.GetById(productId);

        product!.AddStock(quantity);

        _productRepository.Update(product);
        return await _productRepository.UnitOfWork.Commit();
    }

    [ExcludeFromCodeCoverage]
    public void Dispose()
    {
        _productRepository.Dispose();
    }
}