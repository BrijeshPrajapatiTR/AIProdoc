namespace NetProcalc.Domain.Entities;

public sealed class Party
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string Role { get; set; } = "Unknown"; // Obligor/Obligee
}
