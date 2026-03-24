ProCalc (.NET 8) - Clarion to ASP.NET migration skeleton

Prereqs
- .NET SDK 8.0.401 or newer
- PowerShell 7+ (Windows/macOS/Linux)

Quick start
1) cd dotnet
2) pwsh ./dev.ps1
   - restores tools and packages
   - creates initial EF Core migration
   - runs database update
   - launches the site (https://localhost:7253)

Manual
- dotnet restore
- dotnet build
- dotnet ef migrations add Initial --project src/ProCalc.Web --startup-project src/ProCalc.Web
- dotnet ef database update --project src/ProCalc.Web --startup-project src/ProCalc.Web
- dotnet run --project src/ProCalc.Web

Project structure
- src/ProCalc.Core: domain entities and EF Core DbContext
- src/ProCalc.Web: Razor Pages UI + Web API controllers

Notes
- Uses SQLite file procalc.db for easy local runs
- Seeds demo data via DbInitializer
- Navigation mimics Clarion browse/update flows for Customers, Products, Orders

Next steps to reach full parity
- Map remaining Clarion tables/fields from procalc.txa into EF entities
- Bring Clarion business rules into FluentValidation or handler logic
- Add reports (RDL or RazorPDF) if needed
