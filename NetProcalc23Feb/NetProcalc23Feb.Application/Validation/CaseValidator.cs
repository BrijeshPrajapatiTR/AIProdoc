using FluentValidation;
using NetProcalc23Feb.Domain.Entities;

namespace NetProcalc23Feb.Application.Validation;

public class CaseValidator : AbstractValidator<Case>
{
    public CaseValidator()
    {
        RuleFor(x => x.CaseId).NotEmpty().MaximumLength(40);
    }
}
