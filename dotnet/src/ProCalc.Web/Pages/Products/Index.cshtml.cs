using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProCalc.Core.Domain;
using ProCalc.Core.Infrastructure;

namespace ProCalc.Web.Pages.Products;

public class IndexModel(AppDbContext db) : PageModel
{
    public IList<Product> Products { get; set; } = [];
    public async Task OnGetAsync()
    {
        Products = await db.Products.AsNoTracking().OrderBy(p => p.Sku).ToListAsync();
    }
}
