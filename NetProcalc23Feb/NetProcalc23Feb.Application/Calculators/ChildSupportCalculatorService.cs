namespace NetProcalc23Feb.Application.Calculators;

public class ChildSupportCalculatorService
{
    // Simple placeholder to mirror Clarion UI behavior
    public (decimal monthly, decimal biweekly, decimal weekly, decimal semimonthly) NormalizeAmounts(decimal monthly)
    {
        decimal weekly = monthly * 12m / 52m;
        decimal biweekly = weekly * 2m;
        decimal semimonthly = monthly / 2m;
        return (monthly, biweekly, weekly, semimonthly);
    }
}
