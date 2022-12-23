using System;
using NerdStore.Catalogo.Domain.Entities;
using Xunit;

namespace tests.Catalogo.Domain.Entities;

public class ProdutoTests
{
    private const string GenericInvalidString = "";
    private const string GenericString = "xpto";
    private const int GenericInt = 1;
    private const decimal GenericDecimal = 12.2m;

    [Fact]
    public void Validar_GivenInvalidValues_ShouldBeInvalidProduct()
    {
        var product = new Product(
            GenericInvalidString,
            GenericInvalidString,
            false,
            0,
            Guid.NewGuid(),
            DateTime.Now, 
            GenericInvalidString,
            new(GenericInt, GenericInt , GenericInt)
            );
        
        Assert.False(product.IsValid);
        Assert.NotEmpty(product.Notifications);
    }
    
    [Fact]
    public void Validar_GivenInvalidDimensoesValues_ShouldBeInvalidProduct()
    {
        var product = new Product(
            GenericInvalidString,
            GenericInvalidString,
            false,
            0,
            Guid.NewGuid(),
            DateTime.Now, 
            GenericInvalidString,
            new(0, 0 , 0)
        );
        
        Assert.False(product.IsValid);
        Assert.NotEmpty(product.Notifications);
    }
    
    [Fact]
    public void Validar_GivenValidValues_ShouldBeValidProduct()
    {
        var product = new Product(
            GenericString,
            GenericString,
            true,
            GenericDecimal,
            Guid.NewGuid(),
            DateTime.Now, 
            GenericString,
            new(GenericInt, GenericInt , GenericInt)
        );
        
        Assert.True(product.IsValid);
    }
}