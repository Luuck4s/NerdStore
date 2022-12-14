using MediatR;
using Microsoft.Extensions.Logging;
using NerdStore.Catalogo.Domain.Events;
using NerdStore.Catalogo.Domain.Repositories;

namespace NerdStore.Catalogo.Domain.EventHandlers;

public class ProdutoEventHandler: INotificationHandler<ProdutoAbaixoEstoqueEvent>
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly ILogger<ProdutoEventHandler> _logger;

    public ProdutoEventHandler(IProdutoRepository produtoRepository, ILogger<ProdutoEventHandler> logger)
    {
        _produtoRepository = produtoRepository;
        _logger = logger;
    }
    
    public async Task Handle(ProdutoAbaixoEstoqueEvent notification, CancellationToken cancellationToken)
    {
        var produto = await _produtoRepository.ObterPorId(notification.AggregateId);

        _logger.LogInformation($"Produto {produto.Id} - {produto.Nome} abaixo do estoque");
    }
}