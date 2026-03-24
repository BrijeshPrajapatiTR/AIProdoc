# NetProcalc23Feb

A Clean Architecture ASP.NET Core 8.1 solution migrating the Clarion "procalc" app (menus, calculators, browse/update forms, and reports) to MVC + Razor Pages.

## Projects
- NetProcalc23Feb.Domain — Entities, value objects, domain events.
- NetProcalc23Feb.Application — Services, calculators, mappings, validators.
- NetProcalc23Feb.Web — ASP.NET Core MVC + Razor Pages UI, EF Core 8 (SQLite), dynamic menu, reports (PDF/CSV).

## Prerequisites
- .NET SDK 8.0 or 8.0.1xx (8.1) installed
- PowerShell 7+ (Windows) or bash (macOS/Linux)

## Quick start
- Windows: `pwsh ./build.ps1 -Run`
- macOS/Linux: `chmod +x ./build.sh && ./build.sh`

The app creates `netprocalc.db` (SQLite), applies EF migrations, seeds demo data, and starts at http://localhost:5000 (https on 5001).

## Switching databases
- SQL Server: change `UseSqlite` to `UseSqlServer` in Program.cs and update `ConnectionStrings:DefaultConnection`.
- PostgreSQL: `UseNpgsql` and add package `Npgsql.EntityFrameworkCore.PostgreSQL`.

## Data import (TPS/ODBC)
See Tools/seed and DatabaseSeeder for CSV-based import. Export TPS via SoftVelocity ODBC to CSV: Parties.csv, Cases.csv, Obligations.csv, Payments.csv, Judgment.csv, Debits.csv.

## Mapping (Clarion -> ASP.NET)
- File → Parties and Cases → /Parties, /Cases
- PowerPack → Family Law → Child Support Calculator → /Calculators/ChildSupport
- Delinquent Support Calculator → /Calculators/DelinquentSupport
- Payment Splitter → /Calculators/PaymentSplitter
- Obligation Amortizer → /Calculators/Amortizer
