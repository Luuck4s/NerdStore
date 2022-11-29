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
    }

    public override void Validar()
    { }

    public override string ToString()
    {
        return $"{Nome} - {Codigo}";
    }
}