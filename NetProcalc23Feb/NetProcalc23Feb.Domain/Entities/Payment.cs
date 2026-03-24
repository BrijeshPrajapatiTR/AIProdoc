namespace NetProcalc23Feb.Domain.Entities;

public class Payment
{
    public int Id { get; set; }
    public int CaseId { get; set; }
    public DateTime PaidOn { get; set; }
    public decimal Amount { get; set; }
}
