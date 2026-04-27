using Microsoft.AspNetCore.Mvc;

namespace NetProcalc23Feb.Web.Controllers;

public class HomeController : Controller {
  public IActionResult Index() => View();
}
