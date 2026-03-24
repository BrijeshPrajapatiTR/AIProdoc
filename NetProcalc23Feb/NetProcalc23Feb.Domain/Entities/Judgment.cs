namespace NetProcalc23Feb.Domain.Entities;

public class Judgment
{
    public int Id { get; set; }
    public int CaseId { get; set; }
    public Case Case { get; set; } = null!;
    public decimal Principal { get; set; }
    public decimal InterestRate { get; set; } // annual percent
    public bool Compound { get; set; }
}
