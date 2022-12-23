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

    public async Task<bool> DebitStock(Guid produtoId, int quantidade)
    {
        var produto = await _productRepository.GetById(produtoId);

        if (produto.HasStock(quantidade) is false)
        {
            return false;
        }
        
        produto.DebitStock(quantidade);

        if (produto.QuantityStock < 10)
        {
            var @event = new LowStockProductEvent(produto.Id, produto.QuantityStock);
            await _mediatR.PublishEvent(@event);
        }

        _productRepository.Update(produto);
        return await _productRepository.UnitOfWork.Commit();
    }

    public async Task<bool> AddStock(Guid produtoId, int quantidade)
    {
        var produto = await _productRepository.GetById(produtoId);

        produto.AddStock(quantidade);

        _productRepository.Update(produto);
        return await _productRepository.UnitOfWork.Commit();
    }

    [ExcludeFromCodeCoverage]
    public void Dispose()
    {
        _productRepository.Dispose();
    }
}