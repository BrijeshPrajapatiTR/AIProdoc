namespace NetProcalc23Feb.Domain.ValueObjects;

public readonly record struct Money(decimal Amount)
{
    public static implicit operator decimal(Money m) => m.Amount;
    public static implicit operator Money(decimal v) => new(v);
    public override string ToString() => Amount.ToString("C");
}
