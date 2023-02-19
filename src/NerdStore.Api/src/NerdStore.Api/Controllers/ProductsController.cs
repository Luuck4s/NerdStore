using Microsoft.AspNetCore.Mvc;
using NerdStore.Api.Contracts.Requests.Product;
using NerdStore.Api.Queries;
using NerdStore.Catalogo.Domain.Entities;
using NerdStore.Catalogo.Domain.Repositories;
using NerdStore.Catalogo.Domain.Services;
using NerdStore.Catalogo.Domain.ValueObjects;
using NerdStore.Core.Contracts.Results;

namespace NerdStore.Api.Controllers;

[ApiController]
[Route("v1/products")]
public class ProductsController : ControllerBase
{
    private IProductQueries _productQueries;
    private IProductRepository _productRepository;
    private IStockService _stockService;

    public ProductsController(IProductQueries productQueries, IStockService stockService, IProductRepository productRepository)
    {
        _productQueries = productQueries;
        _stockService = stockService;
        _productRepository = productRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
       var products = await _productQueries.GetAllProducts();
       return Ok(products);
    }
    
    [HttpGet("{productId}")]
    public async Task<IActionResult> GetById(Guid productId)
    {
        var product = await _productRepository.GetById(productId);

        if (product is null)
        {
            return BadRequest("Product not found");
        }
        
        return Ok(product);
    }
    
    [HttpPost("add-stock")]
    public async Task<IActionResult> AddStock([FromBody] AddStockRequest request)
    {
        var product = await _productRepository.GetById(request.ProductId);

        if (product is null)
        {
            return BadRequest("Product not found");
        }

        await _stockService.AddStock(product.Id, request.Quantity);
        
        return Ok();
    }
    
    [HttpPost("debit-stock")]
    public async Task<IActionResult> DebitStock([FromBody] DebitStockRequest request)
    {
        var product = await _productRepository.GetById(request.ProductId);

        if (product is null)
        {
            return BadRequest("Product not found");
        }

        var result = await _stockService.DebitStock(product.Id, request.Quantity);

        if (result)
        {
            return Ok();
        }

        return BadRequest("Product without stock");
    }

    [HttpPost]
    public async Task<GenericCommandResult> CreateProduct([FromBody] CreateProductRequest request)
    {
        request.Validate();

        if (request.IsValid is false)
        {
            return await Task.FromResult(new GenericCommandResult(request.Notifications));
        }

        var product = new Product(
            request.Name,
            request.Description,
            true,
            request.Amount,
            request.CategoryId,
            DateTime.Now,
            request.Image,
            new Dimensions(request.Depth, request.Width, request.Height)
        );
        
        _productRepository.Add(product);
        await _productRepository.UnitOfWork.Commit();

        return await Task.FromResult(new GenericCommandResult(product.Id));
    }
}