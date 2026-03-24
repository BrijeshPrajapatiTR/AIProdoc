namespace NetProcalc23Feb.Domain.Entities;

public class Judgment
{
    public int Id { get; set; }
    public int CaseId { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Principal { get; set; }
    public decimal InterestRateAnnual { get; set; }
    public DateTime AsOf { get; set; }
}
