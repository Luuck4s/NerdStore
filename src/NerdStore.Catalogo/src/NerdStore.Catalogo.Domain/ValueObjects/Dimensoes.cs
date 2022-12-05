using Flunt.Validations;
using NerdStore.Catalogo.Domain.Entities;
using NerdStore.Core.ValueObjects;

namespace NerdStore.Catalogo.Domain.ValueObjects;

public class Dimensoes: ValueObject
{
    public decimal Altura { get; private set; }
    public decimal Largura { get; private set; }
    public decimal Profundidade { get; private set; }

    public Dimensoes(decimal profundidade, decimal largura, decimal altura)
    {
        Profundidade = profundidade;
        Largura = largura;
        Altura = altura;
        
        Validar();
    }
    
    public sealed override void Validar()
    {
        AddNotifications(
            new Contract<Produto>()
                .Requires()
                .IsGreaterThan(
                    Altura,
                    0,
                    "Dimensoes.Altura", 
                    "Altura precisa ser maior que 0")
                .IsGreaterThan(
                    Largura,
                    0,
                    "Dimensoes.Largura", 
                    "Largura precisa ser maior que 0")
                .IsGreaterThan(
                    Profundidade,
                    0,
                    "Dimensoes.Profundidade", 
                    "Profundidade precisa ser maior que 0")
        );
    }

    private string DescricaoFormatada()
    {
        return $"LxAxP: {Largura} x {Altura} x {Profundidade}";
    }

    public override string ToString()
    {
        return DescricaoFormatada();
    }
}