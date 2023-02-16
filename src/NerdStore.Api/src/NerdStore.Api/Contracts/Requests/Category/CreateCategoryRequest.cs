using Flunt.Notifications;
using Flunt.Validations;

namespace NerdStore.Api.Contracts.Requests.Category;

public class CreateCategoryRequest: Notifiable<Notification>
{
    public string Name { get; set; } = String.Empty;

    public int Code { get; set; }
    
    public void Validate()
    {
        AddNotifications(
            new Contract<CreateCategoryRequest>()
                .Requires()
                .IsNotNullOrWhiteSpace(
                    Name,
                    "Categoria.Nome",
                    "Nome não pode ser nulo ou vazio")
                .IsGreaterThan(
                    Code,
                    0,
                    "Categoria.Codigo",
                    "Código precisa ser um valor maior que zero")
        );
    }
}