using Microsoft.EntityFrameworkCore;
using NetProcalc23Feb.Application.Common.Abstractions;
using NetProcalc23Feb.Domain.Entities;

namespace NetProcalc23Feb.Web.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext
{
    public DbSet<Party> Parties => Set<Party>();
    public DbSet<Case> Cases => Set<Case>();
    public DbSet<Obligation> Obligations => Set<Obligation>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<Judgment> Judgments => Set<Judgment>();
}

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (!db.Parties.Any())
        {
            db.Parties.AddRange(
                new Party{ FirstName = "Alex", LastName = "Smith", Role="Obligor"},
                new Party{ FirstName = "Jordan", LastName = "Lee", Role="Obligee"}
            );
            await db.SaveChangesAsync();
        }
    }
}
