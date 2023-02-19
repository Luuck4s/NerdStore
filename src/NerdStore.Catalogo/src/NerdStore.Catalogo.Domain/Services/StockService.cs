using System.Diagnostics.CodeAnalysis;
using NerdStore.Catalogo.Domain.Entities;
using NerdStore.Catalogo.Domain.Events;
using NerdStore.Catalogo.Domain.Repositories;
using NerdStore.Core.Dtos;
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

        await DebitItemStock(product!, quantity);

        _productRepository.Update(product!);
        return await _productRepository.UnitOfWork.Commit();
    }

    public async Task<bool> DebitListItemStock(ItemOrderListDto itemsOrder)
    {
        foreach (var itemOrder in itemsOrder.Items)
        {
            var product = await _productRepository.GetById(itemOrder.Id);
            await DebitItemStock(product!, itemOrder.Quantity);
            _productRepository.Update(product!);
        }

        return await _productRepository.UnitOfWork.Commit();
    }

    private async Task DebitItemStock(Product product, int quantity)
    {
        if (product!.HasStock(quantity) is false)
        {
            return;
        }
        
        if (product.QuantityStock < 10)
        {
            var @event = new LowStockProductEvent(product.Id, product.QuantityStock);
            await _mediatR.PublishEvent(@event);
        }
        
        product.DebitStock(quantity);
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