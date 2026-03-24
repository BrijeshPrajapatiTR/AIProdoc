# NetProcalc23Feb (Clarion → ASP.NET Core 8.1)

This branch (DotnetAI) contains a clean-architecture ASP.NET Core 8.1 rewrite scaffold of the Clarion ProCalc app.

Projects
- NetProcalc23Feb.Domain — entities and value objects
- NetProcalc23Feb.Application — services, validators, mappings
- NetProcalc23Feb.Web — ASP.NET Core 8.1 MVC + Razor Pages UI

Run (quick start)
1) Prereqs: .NET SDK 8.0.x
2) From repo root:
   - Windows: powershell -ExecutionPolicy Bypass -File scripts/setup.ps1
   - Linux/macOS: chmod +x scripts/setup.sh && ./scripts/setup.sh
3) Run the web app:
   - dotnet run --project src/NetProcalc23Feb.Web/NetProcalc23Feb.Web.csproj
4) Browse: https://localhost:5243 (or the Kestrel port shown)

Notes
- EF Core uses SQLite by default (app.db). The app ensures the database exists on startup.
- Swagger is enabled in Development at /swagger.

Mapping matrix (Clarion → ASP.NET routes)
- File → Parties browse → GET /Parties → Views/Parties/Index.cshtml
- File → New Party → GET/POST /Parties/Create
- File → Cases browse → GET /Cases
- File → New Case → GET/POST /Cases/Create
- Powerpack → Child Support (General/Adjustment) → GET/POST /Calculators/ChildSupport
- Powerpack → Calculate Semi-monthly/Biweekly/Weekly → GET/POST /Calculators/Frequency
- Powerpack → Obligation Amortizer → GET/POST /Calculators/Amortizer

Data import (TPS/ODBC → RDBMS)
- Use Clarion Topspeed (TPS) ODBC driver to export to CSV or SELECT via ODBC, then import with a one-off ETL into SQLite/SQL Server/PostgreSQL.
- See docs/DataImport.md for options and code snippets.

Screenshots parity
- See docs/ExpectedOutput.md; we mirrored menus and calculators shown in repo Expected output images.

Next steps
- Drop in exact calculator formulas from Clarion .TXA or specification
- Flesh out Delinquent Support wizard and QuestPDF report layouts for parity
