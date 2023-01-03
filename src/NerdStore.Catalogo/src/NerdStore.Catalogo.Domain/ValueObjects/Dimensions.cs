using Flunt.Validations;
using NerdStore.Catalogo.Domain.Entities;
using NerdStore.Core.ValueObjects;

namespace NerdStore.Catalogo.Domain.ValueObjects;

public class Dimensions: ValueObject
{
    public decimal Height { get; private set; }
    public decimal Width { get; private set; }
    public decimal Depth { get; private set; }
    public Product Product { get; private set; }

    public Dimensions(decimal depth, decimal width, decimal height)
    {
        Depth = depth;
        Width = width;
        Height = height;
    }

    private string FormattedDescription()
    {
        return $"LxAxP: {Width} x {Height} x {Depth}";
    }

    public override string ToString()
    {
        return FormattedDescription();
    }
}