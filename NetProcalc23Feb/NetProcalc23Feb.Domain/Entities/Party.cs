namespace NetProcalc23Feb.Domain.Entities;

public class Party
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Role { get; set; } // Obligor/Obligee/etc
    public string FullName => ($"{FirstName} {LastName}").Trim();
}
