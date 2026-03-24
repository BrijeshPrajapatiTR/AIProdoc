using AIProdoc.Web.Data;
using AIProdoc.Web.Models;
using AIProdoc.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AIProdoc.Web.Controllers
{
    public class RecordsController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IBusinessRules _rules;
        public RecordsController(AppDbContext db, IBusinessRules rules)
        {
            _db = db; _rules = rules;
        }

        // Browse
        public async Task<IActionResult> Index(string? q, int page = 1, int pageSize = 20)
        {
            var query = _db.Records.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(q))
                query = query.Where(r => r.Name.Contains(q));
            var total = await query.CountAsync();
            var items = await query
                .OrderByDescending(r => r.Id)
                .Skip((page-1)*pageSize)
                .Take(pageSize)
                .ToListAsync();
            ViewBag.Total = total; ViewBag.Page = page; ViewBag.PageSize = pageSize; ViewBag.Q = q;
            return View(items);
        }

        // Create
        public IActionResult Create() => View(new Record());
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Record r, decimal taxRatePercent = 0)
        {
            if (!_rules.ValidateRecord(r, out var err)) ModelState.AddModelError(string.Empty, err!);
            if (!ModelState.IsValid) return View(r);
            // example business logic usage
            r.Amount = _rules.CalculateTotalWithTax(r.Amount, taxRatePercent);
            _db.Add(r); await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Edit
        public async Task<IActionResult> Edit(int id)
        {
            var r = await _db.Records.FindAsync(id);
            return r == null ? NotFound() : View(r);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Record r)
        {
            if (id != r.Id) return BadRequest();
            if (!_rules.ValidateRecord(r, out var err)) ModelState.AddModelError(string.Empty, err!);
            if (!ModelState.IsValid) return View(r);
            _db.Update(r); await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Details
        public async Task<IActionResult> Details(int id)
        {
            var r = await _db.Records.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return r == null ? NotFound() : View(r);
        }

        // Delete
        public async Task<IActionResult> Delete(int id)
        {
            var r = await _db.Records.FindAsync(id);
            return r == null ? NotFound() : View(r);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var r = await _db.Records.FindAsync(id);
            if (r != null) { _db.Records.Remove(r); await _db.SaveChangesAsync(); }
            return RedirectToAction(nameof(Index));
        }
    }
}
