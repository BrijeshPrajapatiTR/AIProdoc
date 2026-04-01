using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetProcalc23Feb.Domain.Entities;
using NetProcalc23Feb.Web.Data;

namespace NetProcalc23Feb.Web.Controllers;

public class PartiesController : Controller
{
    private readonly AppDbContext _db;
    public PartiesController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index() => View(await _db.Parties.AsNoTracking().ToListAsync());

    public IActionResult Create() => View(new Party());

    [HttpPost]
    public async Task<IActionResult> Create(Party model)
    {
        if (!ModelState.IsValid) return View(model);
        _db.Parties.Add(model);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
