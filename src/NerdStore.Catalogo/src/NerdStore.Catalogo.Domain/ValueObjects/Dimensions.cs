using Flunt.Validations;
using NerdStore.Catalogo.Domain.Entities;
using NerdStore.Core.ValueObjects;

namespace NerdStore.Catalogo.Domain.ValueObjects;

public class Dimensions: ValueObject
{
    public decimal Height { get; private set; }
    public decimal Width { get; private set; }
    public decimal Depth { get; private set; }

    public Dimensions(decimal depth, decimal width, decimal height)
    {
        Depth = depth;
        Width = width;
        Height = height;
        
        Validate();
    }
    
    public sealed override void Validate()
    {
        AddNotifications(
            new Contract<Product>()
                .Requires()
                .IsGreaterThan(
                    Height,
                    0,
                    "Dimensoes.Altura", 
                    "Altura precisa ser maior que 0")
                .IsGreaterThan(
                    Width,
                    0,
                    "Dimensoes.Largura", 
                    "Largura precisa ser maior que 0")
                .IsGreaterThan(
                    Depth,
                    0,
                    "Dimensoes.Profundidade", 
                    "Profundidade precisa ser maior que 0")
        );
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