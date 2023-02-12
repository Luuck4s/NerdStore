using NerdStore.Core.Entities;

namespace NerdStore.Catalogo.Domain.Entities;

public class Category: Entity
{
    public string Name { get; private set; } = string.Empty;

    public int Code { get; private set; }

    public ICollection<Product>? Products { get; private set; }

    protected Category()
    { }

    public Category(string name, int code)
    {
        Name = name;
        Code = code;
    }

    public override string ToString()
    {
        return $"{Name} - {Code}";
    }
}