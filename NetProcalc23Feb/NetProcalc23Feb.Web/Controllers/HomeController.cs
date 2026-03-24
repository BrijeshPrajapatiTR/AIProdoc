using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetProcalc23Feb.Web.Data;

namespace NetProcalc23Feb.Web.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _db;
    public HomeController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index()
    {
        var sections = await _db.Menus
            .AsNoTracking()
            .GroupBy(m => m.ParentCaption)
            .OrderBy(g => g.Key)
            .ToListAsync();
        return View(sections);
    }
}
