using Microsoft.EntityFrameworkCore;
using NetProcalc23Feb.Domain.Entities;

namespace NetProcalc23Feb.Web.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Party> Parties => Set<Party>();
    public DbSet<Case> Cases => Set<Case>();
    public DbSet<Obligation> Obligations => Set<Obligation>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<Judgment> Judgments => Set<Judgment>();
    public DbSet<Debit> Debits => Set<Debit>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<Case>()
            .HasOne(c => c.Obligor).WithMany(p => p.CasesAsObligor)
            .HasForeignKey(c => c.ObligorId).OnDelete(DeleteBehavior.Restrict);
        b.Entity<Case>()
            .HasOne(c => c.Obligee).WithMany(p => p.CasesAsObligee)
            .HasForeignKey(c => c.ObligeeId).OnDelete(DeleteBehavior.Restrict);
        base.OnModelCreating(b);
    }
}
