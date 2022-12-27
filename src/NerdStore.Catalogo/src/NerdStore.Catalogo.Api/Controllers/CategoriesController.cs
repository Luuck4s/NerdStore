using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalogo.Api.Contracts.v1.Category;
using NerdStore.Catalogo.Domain.Entities;
using NerdStore.Catalogo.Domain.Repositories;

namespace NerdStore.Catalogo.Api.Controllers;

public class CategoriesController: ControllerBase
{
    private IProductRepository _productRepository;

    public CategoriesController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _productRepository.GetCategories();
        return Ok(categories);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest request)
    {
        var category = new Category(
            request.Name,
            request.Code
        );
        
        _productRepository.Add(category);
        await _productRepository.UnitOfWork.Commit();

        return Ok(category);
    }
}