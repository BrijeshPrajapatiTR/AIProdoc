using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using NetProcalc23Feb.Domain.Entities;
using NetProcalc23Feb.Web.Data;

namespace NetProcalc23Feb.Web.Controllers;

public class PartiesController : Controller {
  private readonly AppDbContext _db; private readonly IValidator<Party> _validator;
  public PartiesController(AppDbContext db, IValidator<Party> validator){ _db=db; _validator=validator; }

  public async Task<IActionResult> Index() => View(await _db.Parties.ToListAsync());
  public IActionResult Create() => View(new Party());
  [HttpPost] public async Task<IActionResult> Create(Party vm){
    var r = await _validator.ValidateAsync(vm);
    if(!r.IsValid){ r.Errors.ForEach(e => ModelState.AddModelError(e.PropertyName, e.ErrorMessage)); return View(vm); }
    _db.Parties.Add(vm); await _db.SaveChangesAsync(); return RedirectToAction(nameof(Index));
  }
}
