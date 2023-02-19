using Microsoft.AspNetCore.Mvc;
using NerdStore.Api.Contracts.Requests.Category;
using NerdStore.Api.Contracts.Response.Category;
using NerdStore.Api.Queries;
using NerdStore.Catalogo.Domain.Entities;
using NerdStore.Catalogo.Domain.Repositories;

namespace NerdStore.Api.Controllers;

[ApiController]
[Route("v1/categories")]
public class CategoriesController: ControllerBase
{
    private IProductRepository _productRepository;
    private ICategoryQueries _categoryQueries;

    public CategoriesController(IProductRepository productRepository, ICategoryQueries categoryQueries)
    {
        _productRepository = productRepository;
        _categoryQueries = categoryQueries;
    }
    
    [HttpGet]
    public async Task<List<CategoryResponse>> GetAll()
    {
        return await _categoryQueries.GetAllCategories();
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest request)
    {
        request.Validate();

        if (request.IsValid is false)
        {
            return BadRequest(request.Notifications);
        }
        
        var category = new Category(
            request.Name,
            request.Code
        );
        
        _productRepository.Add(category);
        await _productRepository.UnitOfWork.Commit();

        return Ok(category);
    }
}