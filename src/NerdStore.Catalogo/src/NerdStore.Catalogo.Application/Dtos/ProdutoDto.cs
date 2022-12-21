using System.ComponentModel.DataAnnotations;

namespace NerdStore.Catalogo.Application.Dtos;

public class ProdutoDto
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid CategoriaId { get; set; }

    [Required]
    public string Nome { get; set; } = string.Empty;
    [Required]
    public string Descricao { get; set; } = string.Empty;

    [Required]
    public bool Ativo { get; set; }

    [Required]
    public decimal Valor { get; set; }

    [Required]
    public DateTime DataCadastro { get; set; }

    [Required]
    public string Imagem { get; set; } = string.Empty;

    [Required]
    public int QuantidadeEstoque { get; set; }

    [Required]
    public decimal Altura { get; set; }

    [Required]
    public decimal Largura { get; set; }

    [Required]
    public decimal Profundidade { get; set; }

    public IEnumerable<CategoriaDto> Categorias { get; set; }
}