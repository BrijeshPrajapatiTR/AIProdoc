namespace NetProcalc23Feb.Domain.Entities;

public class ValidationRule
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ProcedureName { get; set; } = string.Empty;
    public string Field { get; set; } = string.Empty;
    public string Rule { get; set; } = string.Empty; // human-readable
}
