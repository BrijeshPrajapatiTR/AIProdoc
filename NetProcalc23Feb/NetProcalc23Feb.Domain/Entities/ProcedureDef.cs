namespace NetProcalc23Feb.Domain.Entities;

public class ProcedureDef
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty; // Browse, Update, Report, Process, etc.
    public string? Description { get; set; }
    public string Route => $"/procedures/{Name}";
}
