namespace NetProcalc23Feb.Application.Services;

public static class ChildSupportCalculator
{
    // Baseline: simple income share model placeholder
    public static (decimal ObligorShare, decimal ObligeeShare, decimal Total) Calculate(decimal obligorMonthlyIncome, decimal obligeeMonthlyIncome, decimal baselinePercent)
    {
        var totalIncome = Math.Max(0m, obligorMonthlyIncome) + Math.Max(0m, obligeeMonthlyIncome);
        if (totalIncome <= 0) return (0,0,0);
        var baseline = totalIncome * baselinePercent / 100m;
        var obligorShare = baseline * (obligorMonthlyIncome / totalIncome);
        var obligeeShare = baseline - obligorShare;
        return (decimal.Round(obligorShare,2), decimal.Round(obligeeShare,2), decimal.Round(baseline,2));
    }
}
