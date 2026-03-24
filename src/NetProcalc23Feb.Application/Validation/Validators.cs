using FluentValidation;
using NetProcalc23Feb.Domain.Entities;

namespace NetProcalc23Feb.Application.Validation;

public class PartyValidator : AbstractValidator<Party> {
  public PartyValidator() {
    RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
    RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
  }
}
