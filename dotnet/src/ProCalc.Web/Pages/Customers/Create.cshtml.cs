using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProCalc.Core.Domain;
using ProCalc.Core.Infrastructure;

namespace ProCalc.Web.Pages.Customers;

public class CreateModel(AppDbContext db) : PageModel
{
    [BindProperty]
    public Customer Customer { get; set; } = new();

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();
        db.Customers.Add(Customer);
        await db.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
