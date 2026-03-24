namespace NetProcalc23Feb.Domain.Entities;

public class Debit
{
    public int Id { get; set; }
    public int CaseId { get; set; }
    public Case Case { get; set; } = null!;
    public string Reason { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}
