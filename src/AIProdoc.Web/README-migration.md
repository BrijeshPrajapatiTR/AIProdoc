AIProdoc Clarion -> .NET Migration (Phase 1)

Status (March 24, 2026):
- Created runnable ASP.NET Core 8 MVC shell with menu + browse/update screens.
- Uses SQLite for zero-config local DB; applies migrations automatically.
- Added BusinessRules service to host ported Clarion procedures.
- Next step is to parse procalc.txa and map the Clarion dictionary (tables/keys) to EF models.

Run locally
1) Prereqs: .NET SDK 8.x
2) From repo root:
   - Windows:  scripts\\run.ps1
   - macOS/Linux: scripts/run.sh
   This will restore packages, build, and start the site at http://localhost:5186.

Project layout
- src/AIProdoc.Web: MVC app
- Data: EF Core context
- Models: initial domain types (placeholder)
- Services: business logic stubs (to be replaced with ported Clarion code)

Why net8.0 TFM instead of "8.1"?
- .NET uses major TFMs (net8.0). Patch versions like 8.0.1 are runtime updates, not TFMs.

Next migration steps (planned PRs)
- Import Clarion dictionary from procalc.txa (tables, columns, keys).
- Generate EF Core models and DbContext configuration.
- Create MVC CRUD screens per Clarion browse/update procedures.
- Port non-UI procedures from .CLW into Services.
- Add data import utilities if a Btrieve/Topspeed export is provided.
