using Microsoft.EntityFrameworkCore;
using ProCalc.Core.Domain;

namespace ProCalc.Core.Infrastructure;

public static class DbInitializer
{
    public static async Task InitializeAsync(AppDbContext db)
    {
        await db.Database.MigrateAsync();

        if (!await db.Customers.AnyAsync())
        {
            var cust = new Customer { Code = "C001", Name = "Acme Corp", Email = "info@acme.test", Phone = "+1 555-0100" };
            db.Customers.Add(cust);
        }

        if (!await db.Products.AnyAsync())
        {
            db.Products.AddRange(
                new Product { Sku = "P-100", Name = "Widget", Price = 9.99m },
                new Product { Sku = "P-200", Name = "Gadget", Price = 19.49m }
            );
        }

        await db.SaveChangesAsync();
    }
}
