NetProcalc23Feb — ASP.NET Core 8.1 Clean Architecture migration of Clarion procalc

How to run (Windows/macOS/Linux)
1) Prereqs: .NET SDK 8.0.201+ installed
2) cd NetProcalc23Feb
3) ./scripts/setup.ps1  (Windows PowerShell) OR bash ./scripts/setup.sh
4) dotnet run --project NetProcalc23Feb.Web
5) Open http://localhost:5080

What’s included
- 3 projects: Domain, Application, Web
- EF Core Sqlite (dev) with auto-create/seed
- AutoMapper, FluentValidation
- MVC + Razor Pages to match Clarion browse/update/report patterns

Notes
- Calculators match screenshots in Expected output as reference.
- Data import guidance from Clarion TPS/ODBC in docs/DataImport.md
- Mapping matrix Clarion procedures → ASP.NET routes in docs/MappingMatrix.md
