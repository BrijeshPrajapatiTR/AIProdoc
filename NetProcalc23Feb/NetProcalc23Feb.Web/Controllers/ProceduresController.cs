using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetProcalc23Feb.Web.Data;

namespace NetProcalc23Feb.Web.Controllers;

public class ProceduresController : Controller
{
    private readonly AppDbContext _db;
    public ProceduresController(AppDbContext db) => _db = db;

    [HttpGet("/procedures")] public async Task<IActionResult> Index()
        => View(await _db.Procedures.AsNoTracking().OrderBy(p => p.Name).ToListAsync());

    [HttpGet("/procedures/{name}")]
    public async Task<IActionResult> Details(string name)
    {
        var p = await _db.Procedures.AsNoTracking().FirstOrDefaultAsync(x => x.Name == name);
        if (p == null) return NotFound();
        return View(p);
    }
}
