using System;
using System.Threading;
using Microsoft.Extensions.Logging;
using Moq;
using NerdStore.Catalogo.Domain.Entities;
using NerdStore.Catalogo.Domain.EventHandlers;
using NerdStore.Catalogo.Domain.Events;
using NerdStore.Catalogo.Domain.Repositories;
using NerdStore.Catalogo.Domain.Services;
using Xunit;
using static Moq.It;

namespace tests.Catalogo.Domain.EventHandlers;

public class ProdutoAbaixoEstoqueEventTests
{
    private readonly Mock<IProductRepository> _produtoRepository = new();
    private readonly Mock<ILogger<ProductEventHandler>> _logger = new();
    private readonly ProductEventHandler _handler;
    
    public ProdutoAbaixoEstoqueEventTests()
    {
        var stockService = new Mock<StockService>();
        _handler = new ProductEventHandler(_produtoRepository.Object, _logger.Object, stockService.Object);
    }
    
    [Fact]
    public async void Handle_GivenValidEvent_ShouldBeLogInformation()
    {
        var product = new Product(
            "xpto",
            "xpto",
            true,
            10,
            Guid.NewGuid(),
            DateTime.Now, 
            "xpto",
            new(1, 1 , 1)
        );
        var produtoAbaixoEstoqueEvent = new LowStockProductEvent(Guid.NewGuid(), 10);

        _produtoRepository.Setup(x => x.GetById(IsAny<Guid>()))
            .ReturnsAsync(product);

        await _handler.Handle(produtoAbaixoEstoqueEvent, CancellationToken.None);
    }
}