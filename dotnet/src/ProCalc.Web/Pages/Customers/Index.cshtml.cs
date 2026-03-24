using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProCalc.Core.Domain;
using ProCalc.Core.Infrastructure;

namespace ProCalc.Web.Pages.Customers;

public class IndexModel(AppDbContext db) : PageModel
{
    public IList<Customer> Customers { get; set; } = [];
    public async Task OnGetAsync()
    {
        Customers = await db.Customers.AsNoTracking().OrderBy(c => c.Code).ToListAsync();
    }
}
