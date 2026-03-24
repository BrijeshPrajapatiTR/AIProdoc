using Microsoft.AspNetCore.Mvc;

namespace AIProdoc.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult About() => View();
    }
}
