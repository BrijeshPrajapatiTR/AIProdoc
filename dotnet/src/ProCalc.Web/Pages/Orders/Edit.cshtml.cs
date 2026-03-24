using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProCalc.Core.Domain;
using ProCalc.Core.Infrastructure;

namespace ProCalc.Web.Pages.Orders;

public class EditModel(AppDbContext db) : PageModel
{
    [BindProperty]
    public Order Order { get; set; } = new();
    [BindProperty]
    public List<OrderLine> Lines { get; set; } = [];

    public SelectList CustomerList { get; set; } = default!;
    public SelectList ProductList { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        await LoadAsync(id);
        if (Order.Id == 0) return RedirectToPage("Index");
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadLookups();
            return Page();
        }
        db.Attach(Order).State = EntityState.Modified;
        var existing = db.OrderLines.Where(l => l.OrderId == Order.Id);
        db.OrderLines.RemoveRange(existing);
        foreach (var l in Lines)
        {
            l.OrderId = Order.Id;
            db.OrderLines.Add(l);
        }
        await db.SaveChangesAsync();
        return RedirectToPage("Index");
    }

    public async Task<IActionResult> OnPostAddLine()
    {
        await LoadAsync(Order.Id);
        Lines.Add(new OrderLine { Qty = 1, UnitPrice = 0 });
        return Page();
    }

    public async Task<IActionResult> OnPostRemoveLine(int index)
    {
        await LoadAsync(Order.Id);
        if (index >= 0 && index < Lines.Count)
            Lines.RemoveAt(index);
        return Page();
    }

    private async Task LoadAsync(int id)
    {
        Order = await db.Orders.Include(o => o.Lines).FirstOrDefaultAsync(o => o.Id == id) ?? new Order();
        Lines = Order.Lines.OrderBy(l => l.Id).ToList();
        await LoadLookups();
    }

    private async Task LoadLookups()
    {
        CustomerList = new SelectList(await db.Customers.OrderBy(c => c.Name).ToListAsync(), nameof(Customer.Id), nameof(Customer.Name));
        ProductList = new SelectList(await db.Products.OrderBy(p => p.Name).ToListAsync(), nameof(Product.Id), nameof(Product.Name));
    }
}
