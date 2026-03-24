namespace NetProcalc23Feb.Domain.Entities;

public class Obligation
{
    public int Id { get; set; }
    public int CaseId { get; set; }
    public string Type { get; set; } = "Support"; // child/spousal/etc
    public decimal Amount { get; set; }
    public string Frequency { get; set; } = "Monthly"; // Monthly/Weekly/Biweekly/SemiMonthly
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal InterestRateAnnual { get; set; } // for delinquent calc
}
