NetProcalc23Feb — ASP.NET Core 8 MVC migration scaffold for Clarion procalc

How to run
- Prereq: .NET SDK 8.0.x
- Windows: powershell -ExecutionPolicy Bypass -File scripts/setup.ps1
- macOS/Linux: chmod +x scripts/setup.sh && ./scripts/setup.sh
- Then: dotnet run --project src/NetProcalc23Feb.Web/NetProcalc23Feb.Web.csproj

URLs
- Home: /
- Parties: /Parties
- Cases: /Cases
- Calculators:
  - Frequency: /Calculators/Frequency
  - Amortizer: /Calculators/Amortizer
  - Child Support: /Calculators/ChildSupport

Switch DB
- Default is SQLite file app.db. To switch to SQL Server or PostgreSQL, edit Program.cs UseSqlite() and add provider package.
