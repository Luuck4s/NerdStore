using FluentValidation;

namespace NerdStore.Vendas.Domain.Commands.Validators;

public class AddItemOrderCommandValidator : AbstractValidator<AddItemOrderCommand>
{
    public AddItemOrderCommandValidator()
    {
        RuleFor(x => x.ProductId).NotNull().NotEqual(Guid.Empty);
        RuleFor(x => x.ClientId).NotNull().NotEqual(Guid.Empty);
        RuleFor(x => x.Quantity).NotNull().GreaterThan(0);
        RuleFor(x => x.UnitAmount).NotNull().GreaterThan(0);
    }
}