namespace NetProcalc23Feb.Domain.Entities;

public class Payment
{
    public int Id { get; set; }
    public int CaseId { get; set; }
    public Case Case { get; set; } = null!;
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    public decimal Amount { get; set; }
    public bool Confirmed { get; set; }
}
