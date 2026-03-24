using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProCalc.Core.Domain;
using ProCalc.Core.Infrastructure;

namespace ProCalc.Web.Pages.Orders;

public class CreateModel(AppDbContext db) : PageModel
{
    [BindProperty]
    public int CustomerId { get; set; }
    [BindProperty]
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    public SelectList CustomerList { get; set; } = default!;

    public void OnGet()
    {
        CustomerList = new SelectList(db.Customers.OrderBy(c => c.Name).ToList(), nameof(Customer.Id), nameof(Customer.Name));
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) { OnGet(); return Page(); }
        var order = new Order { CustomerId = CustomerId, OrderDate = OrderDate };
        db.Orders.Add(order);
        await db.SaveChangesAsync();
        return RedirectToPage("Edit", new { id = order.Id });
    }
}
