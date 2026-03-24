NetProcalc23Feb — ASP.NET Core 8 solution migrated from Clarion

Projects
- NetProcalc23Feb.Domain — Domain entities.
- NetProcalc23Feb.Application — Services, parsers, AutoMapper profiles.
- NetProcalc23Feb.Web — MVC + Razor Pages app, EF Core Sqlite for metadata (menus/procedures).

Framework note: .NET 8.1 uses the net8.0 TFM; this solution targets net8.0 and runs on 8.0/8.1 runtimes.

Quick start
1) Prereqs: .NET SDK 8, EF Core tools (dotnet tool restore will install), Sqlite3 optional.
2) From repo root, run: scripts/setup.ps1 (Windows) or scripts/setup.sh (macOS/Linux)
   - This builds, applies EF migrations, runs the web app, and POSTs /admin/import-txa to parse procalc.txa and seed menus and procedures.
3) Open http://localhost:5249 to see sections/menus. Click through to procedures to view routes.

Clarion → ASP.NET mapping
- Parser reads procalc.txa (Clarion TXA export) and extracts:
  - Procedures: name and type
  - Menu items: captions, parent sections, linked procedure
- A generated mapping matrix is available at runtime via /procedures and the home page tiles. Route format: /procedures/{ProcedureName}.

Data import guidance (TPS/ODBC → RDBMS)
- Use SoftVelocity TopSpeed ODBC driver to export TPS tables to CSV, then:
  - SqlServer: BULK INSERT ... FROM 'file.csv' WITH (FORMAT = 'CSV');
  - Sqlite (default): .mode csv; .import file.csv TableName
- For direct ODBC copy, use SqlServer Linked Server to ODBC and SELECT INTO target tables.
- Add concrete EF Core entities/tables matching Clarion dictionaries; hook validators in Application.

Next steps to reach full feature parity
- Implement controller/page per procedure discovered, guided by business rules from .APP/.TXA. Use Application services for rules; wire routes to specific pages.
- Replace placeholder Details view logic with actual screens for Browse/Update/Report types.
- Extend ClarionTxAParser to read fields, keys, constraints from TXA to build strong-typed models.

Scripts
- scripts/setup.ps1 or setup.sh: build, migrate, run, import.

