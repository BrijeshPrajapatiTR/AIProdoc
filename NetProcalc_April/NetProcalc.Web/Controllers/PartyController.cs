using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetProcalc.Domain.Entities;

namespace NetProcalc.Web.Controllers;

public class PartyController : Controller
{
    private readonly IDbContextFactory<InMemoryDb> _factory;
    private readonly IValidator<Party> _validator;

    public PartyController(IDbContextFactory<InMemoryDb> factory, IValidator<Party> validator)
    {
        _factory = factory; _validator = validator;
    }

    public async Task<IActionResult> Index()
    {
        await using var db = await _factory.CreateDbContextAsync();
        var parties = await db.Parties.ToListAsync();
        return View(parties);
    }

    public IActionResult Create() => View(new Party());

    [HttpPost]
    public async Task<IActionResult> Create(Party model)
    {
        var result = await _validator.ValidateAsync(model);
        if (!result.IsValid)
        {
            foreach (var err in result.Errors)
                ModelState.AddModelError(err.PropertyName, err.ErrorMessage);
            return View(model);
        }
        await using var db = await _factory.CreateDbContextAsync();
        db.Parties.Add(model);
        await db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
