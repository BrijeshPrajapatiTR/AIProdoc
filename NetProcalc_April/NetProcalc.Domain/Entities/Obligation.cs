namespace NetProcalc.Domain.Entities;

public sealed class Obligation
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Type { get; set; } = "ChildSupport"; // or Spousal, etc.
    public decimal Principal { get; set; }
    public decimal InterestRate { get; set; } // annual percentage
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
}
