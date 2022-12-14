using System;
using System.Threading;
using Microsoft.Extensions.Logging;
using Moq;
using NerdStore.Catalogo.Domain.Entities;
using NerdStore.Catalogo.Domain.EventHandlers;
using NerdStore.Catalogo.Domain.Events;
using NerdStore.Catalogo.Domain.Repositories;
using Xunit;
using static Moq.It;

namespace tests.Catalogo.Domain.EventHandlers;

public class ProdutoAbaixoEstoqueEventTests
{
    private readonly Mock<IProdutoRepository> _produtoRepository = new();
    private readonly Mock<ILogger<ProdutoEventHandler>> _logger = new();
    private readonly ProdutoEventHandler _handler;
    
    public ProdutoAbaixoEstoqueEventTests()
    {
        _handler = new ProdutoEventHandler(_produtoRepository.Object, _logger.Object);
    }
    
    [Fact]
    public async void Handle_GivenValidEvent_ShouldBeLogInformation()
    {
        var product = new Produto(
            "xpto",
            "xpto",
            true,
            10,
            Guid.NewGuid(),
            DateTime.Now, 
            "xpto",
            new(1, 1 , 1)
        );
        var produtoAbaixoEstoqueEvent = new ProdutoAbaixoEstoqueEvent(Guid.NewGuid(), 10);

        _produtoRepository.Setup(x => x.ObterPorId(IsAny<Guid>()))
            .ReturnsAsync(product);

        await _handler.Handle(produtoAbaixoEstoqueEvent, CancellationToken.None);
    }
}