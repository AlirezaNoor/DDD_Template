using FluentValidation;

namespace DDD.Application.Features.Commands;

public class AddCatalogItemCommandValidator : AbstractValidator<AddCatalogItemCommand>
{
    public AddCatalogItemCommandValidator()
    {
        RuleFor(x => x.CatalogId)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Item name is required.")
            .MaximumLength(100);

        RuleFor(x => x.Amount)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage("Currency is required.")
            .Length(3);
    }
}
