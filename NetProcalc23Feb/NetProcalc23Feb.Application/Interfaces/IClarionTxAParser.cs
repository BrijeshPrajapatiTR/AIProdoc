using NetProcalc23Feb.Domain.Entities;

namespace NetProcalc23Feb.Application.Interfaces;

public interface IClarionTxAParser
{
    Task<(IReadOnlyList<MenuItem> Menus, IReadOnlyList<ProcedureDef> Procedures)> ParseAsync(string txaPath, CancellationToken ct = default);
}
