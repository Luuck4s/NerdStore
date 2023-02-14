using FluentValidation;

namespace NerdStore.Vendas.Domain.Commands.Validators;

public class DeleteItemOrderCommandValidator : AbstractValidator<DeleteItemOrderCommand>
{
    public DeleteItemOrderCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEqual(Guid.Empty)
            .NotNull();
        RuleFor(x => x.OrderId)
            .NotEqual(Guid.Empty)
            .NotNull();
        RuleFor(x => x.ClientId)
            .NotEqual(Guid.Empty)
            .NotNull();
    }
}