using NetProcalc23Feb.Domain.Entities;

namespace NetProcalc23Feb.Application.Services;

public interface IFrequencyConverter {
  decimal ToMonthly(decimal amount, Frequency from);
  decimal FromMonthly(decimal monthlyAmount, Frequency to);
}

public class FrequencyConverter : IFrequencyConverter {
  public decimal ToMonthly(decimal amount, Frequency from) => from switch {
    Frequency.Monthly => amount,
    Frequency.SemiMonthly => amount * 2m,
    Frequency.BiWeekly => amount * 26m / 12m,
    Frequency.Weekly => amount * 52m / 12m,
    _ => amount
  };
  public decimal FromMonthly(decimal monthlyAmount, Frequency to) => to switch {
    Frequency.Monthly => monthlyAmount,
    Frequency.SemiMonthly => monthlyAmount / 2m,
    Frequency.BiWeekly => monthlyAmount * 12m / 26m,
    Frequency.Weekly => monthlyAmount * 12m / 52m,
    _ => monthlyAmount
  };
}

public interface IAmortizer { decimal ComputeMonthlyPayment(decimal principal, decimal annualRate, int months); }

public class Amortizer : IAmortizer {
  public decimal ComputeMonthlyPayment(decimal principal, decimal annualRate, int months) {
    if (months <= 0) return 0m;
    var r = annualRate / 12m / 100m;
    if (r == 0m) return Math.Round(principal / months, 2);
    var pow = (decimal)Math.Pow(1 + (double)r, months);
    var pmt = principal * (r * pow) / (pow - 1);
    return Math.Round(pmt, 2);
  }
}

public interface IChildSupportCalculator { ChildSupportResult Calculate(ChildSupportInput input); }
public record ChildSupportInput(decimal GrossMonthlyIncomeP1, decimal GrossMonthlyIncomeP2, int Children, decimal AdjustmentsP1, decimal AdjustmentsP2);
public record ChildSupportResult(decimal GuidelineAmountP1, decimal GuidelineAmountP2, decimal NetObligationPayer);

public class ChildSupportCalculator : IChildSupportCalculator {
  private readonly IFrequencyConverter _freq;
  public ChildSupportCalculator(IFrequencyConverter freq){ _freq=freq; }
  public ChildSupportResult Calculate(ChildSupportInput input) {
    var net1 = Math.Max(0, input.GrossMonthlyIncomeP1 - input.AdjustmentsP1);
    var net2 = Math.Max(0, input.GrossMonthlyIncomeP2 - input.AdjustmentsP2);
    var total = net1 + net2;
    if (total == 0) return new(0,0,0);
    var share1 = net1 / total; var share2 = net2 / total;
    var tableBase = input.Children switch { 1 => 500m, 2 => 800m, 3 => 1000m, _ => 1200m };
    var g1 = Math.Round(tableBase * share1, 2);
    var g2 = Math.Round(tableBase * share2, 2);
    var payerNet = net1 > net2 ? g1 - g2 : g2 - g1;
    return new(g1, g2, Math.Round(Math.Abs(payerNet), 2));
  }
}
