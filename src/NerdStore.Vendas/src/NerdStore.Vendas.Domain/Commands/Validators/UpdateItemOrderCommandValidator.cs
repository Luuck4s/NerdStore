using FluentValidation;

namespace NerdStore.Vendas.Domain.Commands.Validators;

public class UpdateItemOrderCommandValidator : AbstractValidator<UpdateItemOrderCommand>
{
    public UpdateItemOrderCommandValidator()
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
        RuleFor(x => x.Quantity)
            .NotNull()
            .GreaterThan(1);
    }
}