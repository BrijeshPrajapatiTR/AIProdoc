using Microsoft.AspNetCore.Mvc;
using NetProcalc23Feb.Application.Calculators;
using NetProcalc23Feb.Web.Models;

namespace NetProcalc23Feb.Web.Controllers;

public class CalculatorsController(ChildSupportCalculatorService child, DelinquentSupportCalculatorService delinq) : Controller
{
    [HttpGet]
    public IActionResult ChildSupport() => View(new ChildSupportInput());

    [HttpPost]
    public IActionResult ChildSupport(ChildSupportInput input)
    {
        var result = child.NormalizeAmounts(input.MonthlyAmount);
        ViewBag.Result = result;
        return View(input);
    }

    [HttpGet]
    public IActionResult Delinquent() => View();
}
