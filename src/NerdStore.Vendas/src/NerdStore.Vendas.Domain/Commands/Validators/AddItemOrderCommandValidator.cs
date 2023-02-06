using FluentValidation;

namespace NerdStore.Vendas.Domain.Commands.Validators;

public class AddItemOrderCommandValidator : AbstractValidator<AddItemOrderCommand>
{
    public AddItemOrderCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEqual(Guid.Empty);
        RuleFor(x => x.ClientId)
            .NotEqual(Guid.Empty);
        RuleFor(x => x.Name)
            .NotNull();
        RuleFor(x => x.Quantity).GreaterThan(0);
        RuleFor(x => x.UnitAmount).GreaterThan(0);
    }
}