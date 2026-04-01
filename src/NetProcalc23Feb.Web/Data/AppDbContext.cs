using Microsoft.EntityFrameworkCore;
using NetProcalc23Feb.Domain.Entities;

namespace NetProcalc23Feb.Web.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Party> Parties => Set<Party>();
    public DbSet<Case> Cases => Set<Case>();
}
