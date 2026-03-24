using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetProcalc23Feb.Web.Data;

namespace NetProcalc23Feb.Web.Controllers;

public class MenusController : Controller
{
    private readonly AppDbContext _db;
    public MenusController(AppDbContext db) => _db = db;

    [HttpGet("/menus")] public async Task<IActionResult> Index()
        => View(await _db.Menus.AsNoTracking().OrderBy(m => m.ParentCaption).ThenBy(m => m.Order).ToListAsync());
}
