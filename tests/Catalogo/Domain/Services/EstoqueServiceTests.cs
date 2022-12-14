using System;
using Moq;
using NerdStore.Catalogo.Domain.Entities;
using NerdStore.Catalogo.Domain.Repositories;
using NerdStore.Catalogo.Domain.Services;
using NerdStore.Core.EventHandler;
using Xunit;

namespace tests.Catalogo.Domain.Services;

public class EstoqueServiceTests
{
    private readonly Mock<IProdutoRepository> _repository = new();
    private readonly Mock<IMediatRHandler> _mediatR = new();
    private readonly IEstoqueService _service;

    private const int InitialInventory = 10;

    public EstoqueServiceTests()
    {
        _service = new EstoqueService(_repository.Object, _mediatR.Object);
    }
    
    [Fact]
    public async void DebitarEstoque_GivenValidValues_ShouldUpdateProduct()
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
        product.ReporEstoque(InitialInventory);

        _repository.Setup(x => x.ObterPorId(It.IsAny<Guid>()))
            .ReturnsAsync(product);
        
        _repository.Setup(x => x.UnitOfWork.Commit())
            .ReturnsAsync(true);
        
        var result = await _service.DebitarEstoque(product.Id, InitialInventory - 1);
        
        Assert.NotEqual(InitialInventory, product.QuantidadeEstoque);
        Assert.True(result);
    }

    [Fact]
    public async void ReporEstoque_GivenValidValues_ShouldUpdateProduct()
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
        product.ReporEstoque(InitialInventory);

        _repository.Setup(x => x.ObterPorId(It.IsAny<Guid>()))
            .ReturnsAsync(product);
        
        _repository.Setup(x => x.UnitOfWork.Commit())
            .ReturnsAsync(true);
        
        var result = await _service.ReporEstoque(product.Id, InitialInventory + 10);

        Assert.NotEqual(InitialInventory, product.QuantidadeEstoque);
        Assert.True(result);
    }

    [Fact]
    public async void DebitarEstoque_GivenAProductWithoutInvetory_ShouldReturnFalse()
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
        product.ReporEstoque(InitialInventory);

        _repository.Setup(x => x.ObterPorId(It.IsAny<Guid>()))
            .ReturnsAsync(product);
        
        _repository.Setup(x => x.UnitOfWork.Commit())
            .ReturnsAsync(true);
        
        var result = await _service.DebitarEstoque(product.Id, InitialInventory + 1);
        
        Assert.Equal(InitialInventory, product.QuantidadeEstoque);
        Assert.False(result);
    }
}