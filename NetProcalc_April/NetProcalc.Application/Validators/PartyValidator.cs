using FluentValidation;
using NetProcalc.Domain.Entities;

namespace NetProcalc.Application.Validators;

public sealed class PartyValidator : AbstractValidator<Party>
{
    public PartyValidator()
    {
        RuleFor(p => p.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(p => p.LastName).NotEmpty().MaximumLength(50);
        RuleFor(p => p.Role).NotEmpty();
    }
}
