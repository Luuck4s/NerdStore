using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalogo.Api.Contracts.v1.Product;
using NerdStore.Catalogo.Domain.Entities;
using NerdStore.Catalogo.Domain.Repositories;
using NerdStore.Catalogo.Domain.ValueObjects;

namespace NerdStore.Catalogo.Api.Controllers;

[ApiController]
[Route("v1/products")]
public class ProductsController : ControllerBase
{
    private IProductRepository _productRepository;

    public ProductsController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
       var products = await _productRepository.GetAll();
       return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
    {
        var product = new Product(
            request.Name,
            request.Description,
            true,
            request.Amount,
            Guid.NewGuid(),
            DateTime.Now,
            request.Image,
            new Dimensions(request.Depth, request.Width, request.Height)
        );
        
        _productRepository.Add(product);
        await _productRepository.UnitOfWork.Commit();

        return Ok(product);
    }
}