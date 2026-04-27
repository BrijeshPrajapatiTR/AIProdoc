namespace NetProcalc.Domain.Entities;

public sealed class Payment
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateOnly Date { get; set; }
    public decimal Amount { get; set; }
    public string Method { get; set; } = "Cash";
}
