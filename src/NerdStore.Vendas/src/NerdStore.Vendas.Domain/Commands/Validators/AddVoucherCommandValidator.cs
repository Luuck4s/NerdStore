using FluentValidation;

namespace NerdStore.Vendas.Domain.Commands.Validators;

public class AddVoucherCommandValidator : AbstractValidator<AddVoucherCommand>
{
    public AddVoucherCommandValidator()
    {
        RuleFor(x => x.VoucherCode)
            .NotNull();
        RuleFor(x => x.OrderId)
            .NotEqual(Guid.Empty)
            .NotNull();
        RuleFor(x => x.ClientId)
            .NotEqual(Guid.Empty)
            .NotNull();
    }
}