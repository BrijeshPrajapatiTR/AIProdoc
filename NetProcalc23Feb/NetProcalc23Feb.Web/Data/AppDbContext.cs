using Microsoft.EntityFrameworkCore;
using NetProcalc23Feb.Domain.Entities;

namespace NetProcalc23Feb.Web.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<MenuItem> Menus => Set<MenuItem>();
    public DbSet<ProcedureDef> Procedures => Set<ProcedureDef>();
    public DbSet<ValidationRule> ValidationRules => Set<ValidationRule>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<MenuItem>().HasIndex(x => new { x.ParentCaption, x.Order });
        b.Entity<ProcedureDef>().HasIndex(x => x.Name).IsUnique();
    }
}
