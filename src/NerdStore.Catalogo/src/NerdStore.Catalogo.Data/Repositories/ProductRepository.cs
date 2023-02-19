using Microsoft.EntityFrameworkCore;
using NerdStore.Catalogo.Data.Contexts;
using NerdStore.Catalogo.Domain.Entities;
using NerdStore.Catalogo.Domain.Repositories;
using NerdStore.Core.Data;

namespace NerdStore.Catalogo.Data.Repositories;

public class ProductRepository: IProductRepository
{
    private readonly CatalogContext _context;

    public ProductRepository(CatalogContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await _context.Produtos.Include(p => p.Category)
            .Include(p => p.Dimensions)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Product?> GetById(Guid id)
    {
        return await _context.Produtos
            .Include(p => p.Category)
            .Include(p => p.Dimensions)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Product>> GetByCategory(int codigo)
    {
        return await _context.Produtos.AsNoTracking()
            .Include(p => p.Category)
            .Where(c => c.Category!.Code == codigo)
            .ToListAsync();
    }

    public async Task<IEnumerable<Category>> GetCategories()
    {
        return await _context.Categorias.Include(x => x.Products).AsNoTracking().ToListAsync();
    }

    public void Add(Product product)
    {
        _context.Produtos.Add(product);
    }

    public void Update(Product product)
    {
        _context.Produtos.Update(product);
    }

    public void Add(Category category)
    {
        _context.Categorias.Add(category);
    }

    public void Update(Category category)
    {
        _context.Categorias.Update(category);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}