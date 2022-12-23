using Flunt.Validations;
using NerdStore.Catalogo.Domain.ValueObjects;
using NerdStore.Core.Entities;
using NerdStore.Core.Interfaces;

namespace NerdStore.Catalogo.Domain.Entities;

public class Product : Entity, IAggregateRoot
{
    public Guid CategoryId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool IsActive { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime CreationDate { get; private set; }
    public string Image { get; private set; }
    public int QuantityStock { get; private set; }
    public Category Category { get; private set; }
    public Dimensions Dimensions { get; private set; }

    protected Product()
    { }

    public Product(
        string name, string description, bool isActive, decimal amount, Guid categoryId, DateTime creationDate,
        string image, Dimensions dimensions)
    {
        CategoryId = categoryId;
        Name = name;
        Description = description;
        IsActive = isActive;
        Amount = amount;
        CreationDate = creationDate;
        Image = image;
        Dimensions = dimensions;
        
       Validate();
    }

    public void Activate() => IsActive = true;

    public void Deactivate() => IsActive = false;

    public void ChangeCategory(Category category)
    {
        Category = category;
        CategoryId = category.Id;
    }

    public void ChangeDescription(string description)
    {
        Description = description;
    }

    public void DebitStock(int quantidade)
    {
        if (quantidade < 0) quantidade *= -1;

        QuantityStock -= quantidade;
    }

    public void AddStock(int quantidade)
    {
        QuantityStock += quantidade;
    }

    public bool HasStock(int quantidade) => QuantityStock >= quantidade;

    public sealed override void Validate()
    {
        AddNotifications(
            new Contract<Product>()
                .Requires()
                .IsNotNullOrWhiteSpace(
                    Name,
                    "Produto.Nome",
                    "Nome não pode ser nulo ou vazio")
                .IsNotNullOrWhiteSpace(
                    Description,
                    "Produto.Descricao",
                    "Descricao não pode ser nulo ou vazio")
                .IsNotNullOrWhiteSpace(
                    Image,
                    "Produto.Imagem",
                    "Imagem não pode ser nulo ou vazio")
                .AreNotEquals(
                    CategoryId, 
                    Guid.Empty, 
                    "Produto.CategoriaId", 
                    "Categoria do produto não pode ser inexistente")
                .IsGreaterThan(
                    Amount,
                    0,
                    "Produto.Valor", 
                    "Valor do produto não pode ser zero")
        );
        AddNotifications(Dimensions);
    }
}