namespace NetProcalc23Feb.Domain.Entities;

public class Obligation
{
    public int Id { get; set; }
    public int CaseId { get; set; }
    public Case Case { get; set; } = null!;
    public decimal Amount { get; set; }
    public Frequency Frequency { get; set; } = Frequency.Monthly;
    public string Type { get; set; } = "Child Support";
    public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
}

public enum Frequency { Monthly = 1, Biweekly = 2, Weekly = 3, Semimonthly = 4 }
