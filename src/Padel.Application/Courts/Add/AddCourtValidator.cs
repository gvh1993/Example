using FluentValidation;

namespace Padel.Application.Courts.Add;

internal sealed class AddCourtValidator : AbstractValidator<AddCourtCommand>
{
    public AddCourtValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Court name is required.");
    }
}
