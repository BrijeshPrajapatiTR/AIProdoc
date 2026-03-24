using FluentValidation;
using NetProcalc23Feb.Domain.Entities;

namespace NetProcalc23Feb.Application.Validation;

public class PartyValidator : AbstractValidator<Party>
{
    public PartyValidator()
    {
        RuleFor(x => x.Last).NotEmpty().MaximumLength(50);
        RuleFor(x => x.First).NotEmpty().MaximumLength(50);
    }
}
