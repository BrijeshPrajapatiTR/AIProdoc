using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetProcalc23Feb.Domain.Entities;
using NetProcalc23Feb.Web.Data;

namespace NetProcalc23Feb.Web.Controllers;

public class CasesController : Controller
{
    private readonly AppDbContext _db;
    public CasesController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index() => View(await _db.Cases.AsNoTracking().ToListAsync());

    public IActionResult Create() => View(new Case());

    [HttpPost]
    public async Task<IActionResult> Create(Case model)
    {
        if (!ModelState.IsValid) return View(model);
        _db.Cases.Add(model);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
