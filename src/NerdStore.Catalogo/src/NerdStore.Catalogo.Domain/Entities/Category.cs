using Flunt.Validations;
using NerdStore.Core.Entities;

namespace NerdStore.Catalogo.Domain.Entities;

public class Category: Entity
{
    public string Name { get; private set; }

    public int Code { get; private set; }

    public ICollection<Product> Products { get; private set; }

    protected Category()
    { }

    public Category(string name, int code)
    {
        Name = name;
        Code = code;

        Validate();
    }

    public sealed override void Validate()
    {
        AddNotifications(
            new Contract<Category>()
                .Requires()
                .IsNotNullOrWhiteSpace(
                    Name,
                    "Categoria.Nome",
                    "Nome não pode ser nulo ou vazio")
                .IsGreaterThan(
                    Code,
                    0,
                    "Categoria.Codigo",
                    "Código precisa ser um valor maior que zero")
        );
    }

    public override string ToString()
    {
        return $"{Name} - {Code}";
    }
}