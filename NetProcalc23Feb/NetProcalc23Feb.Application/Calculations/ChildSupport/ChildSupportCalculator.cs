using NetProcalc23Feb.Application.Calculations.Models;

namespace NetProcalc23Feb.Application.Calculations.ChildSupport;

public sealed class ChildSupportCalculator
{
    public ChildSupportResult Calculate(ChildSupportInput input)
    {
        decimal netResources = input.MonthlyNetResources
            - input.SSN_Deduction
            - input.MedicalInsurance
            - input.OtherAdjustments;
        if (netResources < 0) netResources = 0;
        var applicable = Math.Min(netResources, input.Policy.MonthlyCap);
        decimal pct = input.Policy.GuidelinePercentForChildren(input.ChildrenUnder18);
        decimal monthly = Math.Round(applicable * pct, 2, MidpointRounding.AwayFromZero);
        return new ChildSupportResult
        {
            Monthly = monthly,
            Semimonthly = Math.Round(monthly / 2m, 2),
            Biweekly = Math.Round(monthly * 12m / 26m, 2),
            Weekly = Math.Round(monthly * 12m / 52m, 2),
            Notes = "Guideline calculation with adjustments; override in policy as needed."
        };
    }
}
