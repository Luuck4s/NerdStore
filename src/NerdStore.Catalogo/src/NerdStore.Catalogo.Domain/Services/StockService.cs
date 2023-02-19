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

        var productDebitStock = await DebitItemStock(product!, quantity);

        if (productDebitStock is false)
        {
            return await Task.FromResult(false);
        }

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

    public async Task<bool> AddListItemStock(ItemOrderListDto itemsOrder)
    {
        foreach (var itemOrder in itemsOrder.Items)
        {
            var product = await _productRepository.GetById(itemOrder.Id);
            await AddStock(product!, itemOrder.Quantity);
            _productRepository.Update(product!);
        }

        return await _productRepository.UnitOfWork.Commit();
    }

    private async Task<bool> DebitItemStock(Product product, int quantity)
    {
        if (product!.HasStock(quantity) is false)
        {
            return false;
        }
        
        if (product.QuantityStock < 10)
        {
            var @event = new LowStockProductEvent(product.Id, product.QuantityStock);
            await _mediatR.PublishEvent(@event);
        }
        
        product.DebitStock(quantity);
        return true;
    }

    public async Task<bool> AddStock(Product product, int quantity)
    {
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