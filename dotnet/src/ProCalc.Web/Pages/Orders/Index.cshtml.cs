using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProCalc.Core.Domain;
using ProCalc.Core.Infrastructure;

namespace ProCalc.Web.Pages.Orders;

public class IndexModel(AppDbContext db) : PageModel
{
    public IList<Order> Orders { get; set; } = [];
    public async Task OnGetAsync()
    {
        Orders = await db.Orders.AsNoTracking().Include(o => o.Customer).Include(o => o.Lines).OrderByDescending(o => o.Id).ToListAsync();
    }
}
