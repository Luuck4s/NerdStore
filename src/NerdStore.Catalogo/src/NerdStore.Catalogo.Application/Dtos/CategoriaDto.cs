using System.ComponentModel.DataAnnotations;

namespace NerdStore.Catalogo.Application.Dtos;

public class CategoriaDto
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string Nome { get; set; } = String.Empty;

    [Required]
    public int Codigo { get; set; }
}