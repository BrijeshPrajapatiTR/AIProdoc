using Microsoft.AspNetCore.Mvc;

namespace NetProcalc.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
}
