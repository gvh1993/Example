using FluentValidation;

namespace Padel.Application.Courts.Update;

internal sealed class UpdateCourtValidator : AbstractValidator<UpdateCourtCommand>
{
    public UpdateCourtValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Court ID is required.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Court name is required.");
    }
}
