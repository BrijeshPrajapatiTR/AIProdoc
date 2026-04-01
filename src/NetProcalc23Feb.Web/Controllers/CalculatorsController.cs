using Microsoft.AspNetCore.Mvc;
using NetProcalc23Feb.Application.Services;
using NetProcalc23Feb.Domain.Enums;

namespace NetProcalc23Feb.Web.Controllers;

public class CalculatorsController : Controller
{
    public IActionResult Frequency() => View();
    [HttpPost]
    public IActionResult Frequency(decimal amount, Frequency from)
    {
        var monthly = FrequencyConverter.ToMonthly(amount, from);
        ViewBag.Amount = amount; ViewBag.From = from; ViewBag.Monthly = monthly;
        return View();
    }

    public IActionResult Amortizer() => View();
    [HttpPost]
    public IActionResult Amortizer(decimal principal, decimal rateAnnual, int termsPerYear, int years)
    {
        var pmt = Amortizer.Payment(principal, rateAnnual, termsPerYear, years);
        ViewBag.Principal = principal; ViewBag.Rate = rateAnnual; ViewBag.TPY = termsPerYear; ViewBag.Years = years; ViewBag.Payment = pmt;
        return View();
    }

    public IActionResult ChildSupport() => View();
    [HttpPost]
    public IActionResult ChildSupport(decimal obligorMonthlyIncome, decimal obligeeMonthlyIncome, decimal baselinePercent)
    {
        var (o, e, t) = ChildSupportCalculator.Calculate(obligorMonthlyIncome, obligeeMonthlyIncome, baselinePercent);
        ViewBag.Obligor = o; ViewBag.Obligee = e; ViewBag.Total = t;
        return View("ChildSupportResult");
    }
}
