using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetProcalc23Feb.Web.Data;
using NetProcalc23Feb.Domain.Entities;

namespace NetProcalc23Feb.Web.Controllers;

public class CasesController : Controller {
  private readonly AppDbContext _db;
  public CasesController(AppDbContext db){ _db=db; }

  public async Task<IActionResult> Index() => View(await _db.Cases.Include(c=>c.Obligations).ToListAsync());
  public IActionResult Create() => View(new Case());
  [HttpPost] public async Task<IActionResult> Create(Case vm){ if(!ModelState.IsValid) return View(vm); _db.Cases.Add(vm); await _db.SaveChangesAsync(); return RedirectToAction(nameof(Index)); }

  public async Task<IActionResult> Edit(int id){ var m = await _db.Cases.FindAsync(id); if(m==null) return NotFound(); return View(m); }
  [HttpPost] public async Task<IActionResult> Edit(Case vm){ if(!ModelState.IsValid) return View(vm); _db.Update(vm); await _db.SaveChangesAsync(); return RedirectToAction(nameof(Index)); }

  public async Task<IActionResult> Details(int id){ var m = await _db.Cases.Include(c=>c.Obligations).Include(c=>c.Payments).FirstOrDefaultAsync(x=>x.Id==id); if(m==null) return NotFound(); return View(m); }
}
