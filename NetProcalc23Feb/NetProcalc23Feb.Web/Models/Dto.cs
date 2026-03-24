namespace NetProcalc23Feb.Web.Models;

public record PartyDto
{
    public int Id { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string? Role { get; init; }
}

public record CaseDto
{
    public int Id { get; init; }
    public string CaseNumber { get; init; } = string.Empty;
}

public record ObligationDto
{
    public int Id { get; init; }
    public int CaseId { get; init; }
    public decimal Amount { get; init; }
    public string Frequency { get; init; } = "Monthly";
}

public record PaymentDto
{
    public int Id { get; init; }
    public int CaseId { get; init; }
    public DateTime PaidOn { get; init; }
    public decimal Amount { get; init; }
}

public record JudgmentDto
{
    public int Id { get; init; }
    public int CaseId { get; init; }
    public decimal Principal { get; init; }
    public decimal InterestRateAnnual { get; init; }
}

public class ChildSupportInput
{
    public decimal MonthlyAmount { get; set; }
}
