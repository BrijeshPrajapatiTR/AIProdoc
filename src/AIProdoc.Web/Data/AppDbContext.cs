using AIProdoc.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace AIProdoc.Web.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Record> Records => Set<Record>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Record>(e =>
            {
                e.Property(p => p.Name).HasMaxLength(120).IsRequired();
                e.Property(p => p.Description).HasMaxLength(512);
                e.Property(p => p.Amount).HasPrecision(18,2);
                e.HasIndex(p => p.Name);
            });
        }
    }
}
