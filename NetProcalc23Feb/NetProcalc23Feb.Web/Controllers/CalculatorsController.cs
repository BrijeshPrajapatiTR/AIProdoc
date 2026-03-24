using Microsoft.AspNetCore.Mvc;
using NetProcalc23Feb.Application.Calculations.ChildSupport;
using NetProcalc23Feb.Application.Calculations.Models;

namespace NetProcalc23Feb.Web.Controllers;

public class CalculatorsController : Controller
{
    public IActionResult ChildSupport() => View();

    [HttpPost]
    public IActionResult ChildSupport(ChildSupportInput input)
    {
        var calc = new ChildSupportCalculator();
        var result = calc.Calculate(input);
        ViewBag.Result = result;
        return View(input);
    }

    public IActionResult PaymentSplitter() => View();
    public IActionResult Amortizer() => View();
    public IActionResult DelinquentSupport() => View();
}
