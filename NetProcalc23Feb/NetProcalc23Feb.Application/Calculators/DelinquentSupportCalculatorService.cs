using NetProcalc23Feb.Domain.Entities;

namespace NetProcalc23Feb.Application.Calculators;

public class DelinquentSupportCalculatorService
{
    // Computes interest accrual for obligations/judgments minus payments (simplified)
    public decimal ComputeBalance(Case caze, DateTime asOf)
    {
        decimal principal = caze.Judgments.Sum(j => j.Principal);
        decimal interest = 0m;
        foreach (var j in caze.Judgments)
        {
            var years = (decimal)(asOf - j.AsOf).TotalDays / 365.25m;
            interest += j.Principal * j.InterestRateAnnual/100m * years;
        }
        decimal paid = caze.Payments.Where(p => p.PaidOn <= asOf).Sum(p => p.Amount);
        return principal + interest - paid;
    }
}
