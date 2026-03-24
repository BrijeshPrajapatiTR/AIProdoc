using Microsoft.EntityFrameworkCore;
using NetProcalc23Feb.Domain.Entities;

namespace NetProcalc23Feb.Application.Common.Abstractions;

public interface IAppDbContext
{
    DbSet<Party> Parties { get; }
    DbSet<Case> Cases { get; }
    DbSet<Obligation> Obligations { get; }
    DbSet<Payment> Payments { get; }
    DbSet<Judgment> Judgments { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
