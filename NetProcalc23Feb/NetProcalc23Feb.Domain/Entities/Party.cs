using System.ComponentModel.DataAnnotations;

namespace NetProcalc23Feb.Domain.Entities;

public class Party
{
    public int Id { get; set; }
    [Required, MaxLength(50)] public string Last { get; set; } = string.Empty;
    [Required, MaxLength(50)] public string First { get; set; } = string.Empty;
    [MaxLength(50)] public string? Middle { get; set; }
    [MaxLength(20)] public string? Prefix { get; set; }
    [MaxLength(20)] public string? Suffix { get; set; }
    public bool BusinessName { get; set; }
    [MaxLength(80)] public string? Salutation { get; set; }
    [MaxLength(200)] public string? Address { get; set; }
    [MaxLength(60)] public string? City { get; set; }
    [MaxLength(30)] public string? State { get; set; }
    [MaxLength(15)] public string? Zip { get; set; }
    public ICollection<Case> CasesAsObligor { get; set; } = new List<Case>();
    public ICollection<Case> CasesAsObligee { get; set; } = new List<Case>();
    public override string ToString() => $"{Last}, {First}";
}
