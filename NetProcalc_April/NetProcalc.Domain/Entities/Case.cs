namespace NetProcalc.Domain.Entities;

public sealed class Case
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string CaseNumber { get; set; } = string.Empty;
    public string Court { get; set; } = string.Empty;
    public List<Obligation> Obligations { get; set; } = new();
    public List<Payment> Payments { get; set; } = new();
}
