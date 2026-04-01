namespace NetProcalc23Feb.Domain.Entities;

public class Case
{
    public int Id { get; set; }
    public string CaseNumber { get; set; } = string.Empty;
    public DateTime OpenedOn { get; set; } = DateTime.UtcNow.Date;
}
