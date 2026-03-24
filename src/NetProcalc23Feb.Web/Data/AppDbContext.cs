using Microsoft.EntityFrameworkCore;
using NetProcalc23Feb.Domain.Entities;

namespace NetProcalc23Feb.Web.Data;

public class AppDbContext : DbContext {
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
  public DbSet<Party> Parties => Set<Party>();
  public DbSet<Case> Cases => Set<Case>();
  public DbSet<Obligation> Obligations => Set<Obligation>();
  public DbSet<Payment> Payments => Set<Payment>();
  public DbSet<Adjustment> Adjustments => Set<Adjustment>();
  public DbSet<Judgment> Judgments => Set<Judgment>();
  protected override void OnModelCreating(ModelBuilder b) {
    b.Entity<Party>().Property<string>(p=>p.FirstName).HasMaxLength(100);
    b.Entity<Party>().Property<string>(p=>p.LastName).HasMaxLength(100);
    b.Entity<Obligation>().OwnsOne(o => o.Amount);
    b.Entity<Payment>().OwnsOne(o => o.Amount);
    b.Entity<Adjustment>().OwnsOne(o => o.Amount);
    b.Entity<Judgment>().OwnsOne(o => o.Principal);
  }
}
