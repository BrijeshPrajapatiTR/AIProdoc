namespace NetProcalc23Feb.Domain.Entities;

public class MenuItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Caption { get; set; } = string.Empty;
    public string? ParentCaption { get; set; }
    public string? ProcedureName { get; set; }
    public int Order { get; set; }
}
