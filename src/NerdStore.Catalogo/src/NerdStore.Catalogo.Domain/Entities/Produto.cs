using Flunt.Validations;
using NerdStore.Catalogo.Domain.ValueObjects;
using NerdStore.Core.Entities;
using NerdStore.Core.Interfaces;

namespace NerdStore.Catalogo.Domain.Entities;

public class Produto : Entity, IAggregateRoot
{
    public Guid CategoriaId { get; private set; }
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public bool Ativo { get; private set; }
    public decimal Valor { get; private set; }
    public DateTime DataCadastro { get; private set; }
    public string Imagem { get; private set; }
    public int QuantidadeEstoque { get; private set; }
    public Categoria Categoria { get; private set; }
    public Dimensoes Dimensoes { get; private set; }

    public Produto(
        string nome, string descricao, bool ativo, decimal valor, Guid categoriaId, DateTime dataCadastro,
        string imagem, Dimensoes dimensoes)
    {
        CategoriaId = categoriaId;
        Nome = nome;
        Descricao = descricao;
        Ativo = ativo;
        Valor = valor;
        DataCadastro = dataCadastro;
        Imagem = imagem;
        Dimensoes = dimensoes;
        
        Validar();
    }

    public void Ativar() => Ativo = true;

    public void Desativar() => Ativo = false;

    public void AlterarCategoria(Categoria categoria)
    {
        Categoria = categoria;
        CategoriaId = categoria.Id;
    }

    public void AlterarDescricao(string descricao)
    {
        Descricao = descricao;
    }

    public void DebitarEstoque(int quantidade)
    {
        if (quantidade < 0) quantidade *= -1;

        QuantidadeEstoque -= quantidade;
    }

    public void ReporEstoque(int quantidade)
    {
        QuantidadeEstoque += quantidade;
    }

    public bool PossuiEstoque(int quantidade) => QuantidadeEstoque >= quantidade;

    public sealed override void Validar()
    {
        
        AddNotifications(
            new Contract<Produto>()
                .Requires()
                .IsNotNullOrWhiteSpace(
                    Nome,
                    "Produto.Nome",
                    "Nome não pode ser nulo ou vazio")
                .IsNotNullOrWhiteSpace(
                    Descricao,
                    "Produto.Descricao",
                    "Descricao não pode ser nulo ou vazio")
                .IsNotNullOrWhiteSpace(
                    Imagem,
                    "Produto.Imagem",
                    "Imagem não pode ser nulo ou vazio")
                .AreNotEquals(
                    CategoriaId, 
                    Guid.Empty, 
                    "Produto.CategoriaId", 
                    "Categoria do produto não pode ser inexistente")
                .IsGreaterThan(
                    Valor,
                    0,
                    "Produto.Valor", 
                    "Valor do produto não pode ser zero")
        );
        AddNotifications(Dimensoes);
    }
}