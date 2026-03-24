using Microsoft.AspNetCore.Mvc;
using NetProcalc23Feb.Application.Services;

namespace NetProcalc23Feb.Web.Controllers;

public class CalculatorsController : Controller {
  private readonly IFrequencyConverter _freq; private readonly IAmortizer _amo; private readonly IChildSupportCalculator _csc;
  public CalculatorsController(IFrequencyConverter f, IAmortizer a, IChildSupportCalculator c){ _freq=f; _amo=a; _csc=c; }

  public IActionResult Index() => View();

  [HttpGet] public IActionResult ChildSupport() => View();
  [HttpPost] public IActionResult ChildSupport(decimal p1, decimal p2, int children, decimal adj1, decimal adj2){
    var res = _csc.Calculate(new(p1,p2,children,adj1,adj2));
    return View("ChildSupportResult", res);
  }

  [HttpGet] public IActionResult Frequency() => View();
  [HttpPost] public IActionResult Frequency(decimal amount, int from, int to){
    var monthly = _freq.ToMonthly(amount, (NetProcalc23Feb.Domain.Entities.Frequency)from);
    var result = _freq.FromMonthly(monthly, (NetProcalc23Feb.Domain.Entities.Frequency)to);
    ViewBag.Result = result; return View();
  }

  [HttpGet] public IActionResult Amortizer() => View();
  [HttpPost] public IActionResult Amortizer(decimal principal, decimal annualRate, int months){
    var pmt = _amo.ComputeMonthlyPayment(principal, annualRate, months);
    ViewBag.Payment = pmt; return View();
  }
}
