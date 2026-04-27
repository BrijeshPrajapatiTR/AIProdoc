using NetProcalc.Domain.Entities;

namespace NetProcalc.Application.Services;

public interface IAmortizerService
{
    decimal AccruedInterest(Obligation obligation, DateOnly asOf);
}

internal sealed class AmortizerService : IAmortizerService
{
    public decimal AccruedInterest(Obligation obligation, DateOnly asOf)
    {
        var end = obligation.EndDate ?? asOf;
        if (end < obligation.StartDate) return 0m;
        var days = (end.ToDateTime(TimeOnly.MinValue) - obligation.StartDate.ToDateTime(TimeOnly.MinValue)).Days;
        var dailyRate = (double)obligation.InterestRate / 100.0 / 365.0;
        var interest = obligation.Principal * (decimal)(dailyRate * days);
        return Math.Round(interest, 2);
    }
}
