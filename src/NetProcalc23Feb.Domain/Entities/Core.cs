namespace NetProcalc23Feb.Domain.Entities;

public enum Frequency { Monthly=1, SemiMonthly=2, BiWeekly=3, Weekly=4 }
public record Money(decimal Amount) { public static implicit operator decimal(Money m)=>m.Amount; public static Money From(decimal v)=> new Money(v); }

public class Party { public int Id {get;set;} public string FirstName {get;set;} = string.Empty; public string LastName {get;set;} = string.Empty; public string Role {get;set;} = string.Empty; }

public class Case { public int Id {get;set;} public string CaseNumber {get;set;} = string.Empty; public DateTime OpenedOn {get;set;} = DateTime.UtcNow; public ICollection<Obligation> Obligations {get;set;} = new List<Obligation>(); public ICollection<Payment> Payments {get;set;} = new List<Payment>(); }

public class Obligation { public int Id {get;set;} public int CaseId {get;set;} public Case? Case {get;set;} public Money Amount {get;set;} = new(0); public Frequency Frequency {get;set;} = Frequency.Monthly; public decimal InterestRateAnnual {get;set;} = 0m; public DateTime StartDate {get;set;} = DateTime.UtcNow.Date; }

public class Payment { public int Id {get;set;} public int CaseId {get;set;} public Case? Case {get;set;} public Money Amount {get;set;} = new(0); public DateTime PaidOn {get;set;} = DateTime.UtcNow.Date; }

public class Adjustment { public int Id {get;set;} public int CaseId {get;set;} public Case? Case {get;set;} public string Reason {get;set;} = string.Empty; public Money Amount {get;set;} = new(0); public DateTime EffectiveOn {get;set;} = DateTime.UtcNow.Date; }

public class Judgment { public int Id {get;set;} public int CaseId {get;set;} public Case? Case {get;set;} public Money Principal {get;set;} = new(0); public decimal InterestRateAnnual {get;set;} = 0m; public DateTime EnteredOn {get;set;} = DateTime.UtcNow.Date; }