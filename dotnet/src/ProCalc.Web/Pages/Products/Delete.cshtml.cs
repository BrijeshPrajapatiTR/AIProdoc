using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProCalc.Core.Domain;
using ProCalc.Core.Infrastructure;

namespace ProCalc.Web.Pages.Products;

public class DeleteModel(AppDbContext db) : PageModel
{
    [BindProperty]
    public Product? Product { get; set; }

    public async Task OnGetAsync(int id)
    {
        Product = await db.Products.FindAsync(id);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (Product is not null)
        {
            var toDelete = await db.Products.FindAsync(Product.Id);
            if (toDelete is not null)
            {
                db.Products.Remove(toDelete);
                await db.SaveChangesAsync();
            }
        }
        return RedirectToPage("Index");
    }
}
