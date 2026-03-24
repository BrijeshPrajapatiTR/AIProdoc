namespace NetProcalc23Feb.Application.Calculations.Models;

public record ChildSupportPolicy(decimal MonthlyCap, Dictionary<int, decimal> Percents)
{
    public decimal GuidelinePercentForChildren(int n)
        => Percents.TryGetValue(Math.Clamp(n,1,5), out var p) ? p : Percents[5];
}

public record ChildSupportInput
{
    public int ChildrenUnder18 { get; init; }
    public decimal MonthlyNetResources { get; init; }
    public decimal SSN_Deduction { get; init; }
    public decimal MedicalInsurance { get; init; }
    public decimal OtherAdjustments { get; init; }
    public ChildSupportPolicy Policy { get; init; } = new(9000m, new(){[1]=0.20m,[2]=0.25m,[3]=0.30m,[4]=0.35m,[5]=0.40m});
}

public record ChildSupportResult
{
    public decimal Monthly { get; init; }
    public decimal Semimonthly { get; init; }
    public decimal Biweekly { get; init; }
    public decimal Weekly { get; init; }
    public string Notes { get; init; } = string.Empty;
}
