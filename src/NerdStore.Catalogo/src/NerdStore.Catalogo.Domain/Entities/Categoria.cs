using Flunt.Validations;
using NerdStore.Core.Entities;

namespace NerdStore.Catalogo.Domain.Entities;

public class Categoria: Entity
{
    public string Nome { get; private set; }

    public int Codigo { get; private set; }

    public Categoria(string nome, int codigo)
    {
        Nome = nome;
        Codigo = codigo;

        Validar();
    }

    public override void Validar()
    {
        AddNotifications(
            new Contract<Categoria>()
                .Requires()
                .IsNotNullOrWhiteSpace(
                    Nome,
                    "Categoria.Nome",
                    "Nome não pode ser nulo ou vazio")
                .IsGreaterThan(
                    Codigo,
                    0,
                    "Categoria.Codigo",
                    "Código precisa ser um valor maior que zero")
        );
    }

    public override string ToString()
    {
        return $"{Nome} - {Codigo}";
    }
}