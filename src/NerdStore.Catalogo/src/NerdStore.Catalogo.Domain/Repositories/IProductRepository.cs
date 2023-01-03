using NerdStore.Catalogo.Domain.Entities;
using NerdStore.Core.Data;

namespace NerdStore.Catalogo.Domain.Repositories;

public interface IProductRepository: IRepository<Product>
{
    Task<IEnumerable<Product>> GetAll();

    Task<Product?> GetById(Guid id);

    Task<IEnumerable<Product>> GetByCategory(int codigo);

    Task<IEnumerable<Category>> GetCategories();

    void Add(Product product);

    void Update(Product product);

    void Add(Category category);

    void Update(Category category);
}