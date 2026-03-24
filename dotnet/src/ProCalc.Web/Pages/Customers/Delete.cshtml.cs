using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProCalc.Core.Domain;
using ProCalc.Core.Infrastructure;

namespace ProCalc.Web.Pages.Customers;

public class DeleteModel(AppDbContext db) : PageModel
{
    [BindProperty]
    public Customer? Customer { get; set; }

    public async Task OnGetAsync(int id)
    {
        Customer = await db.Customers.FindAsync(id);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (Customer is not null)
        {
            var toDelete = await db.Customers.FindAsync(Customer.Id);
            if (toDelete is not null)
            {
                db.Customers.Remove(toDelete);
                await db.SaveChangesAsync();
            }
        }
        return RedirectToPage("Index");
    }
}
