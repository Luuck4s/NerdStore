using NerdStore.Catalogo.Domain.Events;
using NerdStore.Catalogo.Domain.Repositories;
using NerdStore.Core.EventHandler;

namespace NerdStore.Catalogo.Domain.Services;

public class EstoqueService: IEstoqueService
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IMediatRHandler _mediatR;

    public EstoqueService(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<bool> DebitarEstoque(Guid produtoId, int quantidade)
    {
        var produto = await _produtoRepository.ObterPorId(produtoId);

        if (produto is null)
        {
            return false;
        }

        if (produto.PossuiEstoque(quantidade) is false)
        {
            return false;
        }
        
        produto.DebitarEstoque(quantidade);

        if (produto.QuantidadeEstoque < 10)
        {
            var @event = new ProdutoAbaixoEstoqueEvent(produto.Id, produto.QuantidadeEstoque);
            await _mediatR.PublicarEvento(@event);
        }

        _produtoRepository.Atualizar(produto);
        return await _produtoRepository.UnitOfWork.Commit();
    }

    public async Task<bool> ReporEstoque(Guid produtoId, int quantidade)
    {
        var produto = await _produtoRepository.ObterPorId(produtoId);

        if (produto is null)
        {
            return false;
        }

        produto.ReporEstoque(quantidade);

        _produtoRepository.Atualizar(produto);
        return await _produtoRepository.UnitOfWork.Commit();
    }

    public void Dispose()
    {
        _produtoRepository.Dispose();
    }
}