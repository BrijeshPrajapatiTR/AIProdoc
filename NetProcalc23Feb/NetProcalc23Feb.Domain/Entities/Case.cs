using System.ComponentModel.DataAnnotations;

namespace NetProcalc23Feb.Domain.Entities;

public class Case
{
    public int Id { get; set; }
    [Required, MaxLength(40)] public string CaseId { get; set; } = string.Empty;
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    [MaxLength(200)] public string? Description { get; set; }
    public int? ObligorId { get; set; }
    public Party? Obligor { get; set; }
    public int? ObligeeId { get; set; }
    public Party? Obligee { get; set; }
    public ICollection<Obligation> Obligations { get; set; } = new List<Obligation>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    public Judgment? Judgment { get; set; }
    public Debit? Debit { get; set; }
}
