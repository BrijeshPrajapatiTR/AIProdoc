namespace NetProcalc23Feb.Domain.Entities;

public class Case
{
    public int Id { get; set; }
    public string CaseNumber { get; set; } = string.Empty;
    public string? County { get; set; }
    public DateTime OpenedOn { get; set; } = DateTime.UtcNow.Date;
    public List<Obligation> Obligations { get; set; } = new();
    public List<Payment> Payments { get; set; } = new();
    public List<Judgment> Judgments { get; set; } = new();
}
