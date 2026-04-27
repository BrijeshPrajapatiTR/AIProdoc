NetProcalc_April — Clarion to ASP.NET Core 8 migration scaffold

Prereqs
- .NET SDK 8.0.204+ (Windows/macOS/Linux)
- Node optional for bundling (not required; Bootstrap from CDN)

Quick start
1) cd NetProcalc_April/NetProcalc.Web
2) dotnet restore
3) dotnet run

Open https://localhost:5001 (or console URL)

Structure
- NetProcalc.Domain: core entities
- NetProcalc.Application: services, validators
- NetProcalc.Web: MVC + Razor

Mapping snapshot (initial)
- Clarion Menu File -> / (Home nav) with File dropdown
- Parties and case -> GET /Party (index)
- New Party -> GET/POST /Party/Create

Next steps
- Port calculators: child support, delinquent support (tabs in Expected output)
- Implement persistence (SQLite/SQL Server)
- Add AutoMapper profiles & MediatR if needed
