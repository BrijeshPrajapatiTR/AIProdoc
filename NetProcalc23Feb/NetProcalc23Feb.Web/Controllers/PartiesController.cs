using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetProcalc23Feb.Application.Common.Abstractions;
using NetProcalc23Feb.Domain.Entities;
using NetProcalc23Feb.Web.Models;

namespace NetProcalc23Feb.Web.Controllers;

public class PartiesController(IAppDbContext db, IMapper mapper) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var items = await db.Parties.OrderBy(p => p.LastName).ToListAsync();
        return View(items.Select(mapper.Map<PartyDto>).ToList());
    }

    [HttpGet]
    public IActionResult Create() => View(new PartyDto());

    [HttpPost]
    public async Task<IActionResult> Create(PartyDto dto)
    {
        if (!ModelState.IsValid) return View(dto);
        db.Parties.Add(mapper.Map<Party>(dto));
        await db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
