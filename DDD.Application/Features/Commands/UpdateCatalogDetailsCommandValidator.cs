using FluentValidation;

namespace DDD.Application.Features.Commands;

public class UpdateCatalogDetailsCommandValidator : AbstractValidator<UpdateCatalogDetailsCommand>
{
    public UpdateCatalogDetailsCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .MaximumLength(500);
    }
}
