namespace NetProcalc23Feb.Application.Services;

public static class Amortizer
{
    // Standard amortization payment
    public static decimal Payment(decimal principal, decimal annualRate, int termsPerYear, int totalYears)
    {
        if (principal <= 0 || totalYears <= 0 || termsPerYear <= 0) return 0m;
        var r = (annualRate/100m) / termsPerYear;
        var n = termsPerYear * totalYears;
        if (r == 0) return decimal.Round(principal / n, 2);
        var pow = Math.Pow(1 + (double)r, n);
        var pmt = (double)principal * (double)r * pow / (pow - 1);
        return decimal.Round((decimal)pmt, 2);
    }
}
