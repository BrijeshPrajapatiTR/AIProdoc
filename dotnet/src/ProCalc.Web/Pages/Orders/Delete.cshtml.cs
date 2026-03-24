using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProCalc.Core.Infrastructure;

namespace ProCalc.Web.Pages.Orders;

public class DeleteModel(AppDbContext db) : PageModel
{
    [BindProperty]
    public int Id { get; set; }

    public void OnGet(int id) => Id = id;

    public async Task<IActionResult> OnPostAsync()
    {
        var order = await db.Orders.FindAsync(Id);
        if (order is not null)
        {
            db.Orders.Remove(order);
            await db.SaveChangesAsync();
        }
        return RedirectToPage("Index");
    }
}
