using Flunt.Notifications;
using Flunt.Validations;

namespace NerdStore.Api.Contracts.Requests.Product;

public class CreateProductRequest : Notifiable<Notification>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public Guid CategoryId { get; set; }

    public decimal Amount { get; set; }

    public string Image { get; set; } = string.Empty;

    public decimal Height { get; set; }
    public decimal Width { get; set; }
    public decimal Depth { get; set; }

    public void Validate()
    {
        AddNotifications(
            new Contract<CreateProductRequest>()
                .Requires()
                .IsNotNullOrWhiteSpace(
                    Name,
                    "Produto.Nome",
                    "Nome não pode ser nulo ou vazio")
                .IsNotNullOrWhiteSpace(
                    Description,
                    "Produto.Descricao",
                    "Descricao não pode ser nulo ou vazio")
                .IsNotNullOrWhiteSpace(
                    Image,
                    "Produto.Imagem",
                    "Imagem não pode ser nulo ou vazio")
                .AreNotEquals(
                    CategoryId,
                    Guid.Empty,
                    "Produto.CategoriaId",
                    "Categoria do produto não pode ser inexistente")
                .IsGreaterThan(
                    Amount,
                    0,
                    "Produto.Valor",
                    "Valor do produto não pode ser zero")
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
}