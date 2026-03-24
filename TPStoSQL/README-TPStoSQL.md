Clarion TPS/DAT → SQL Server: End‑to‑End Migration Plan

Scope
- Source: Clarion TopSpeed .TPS files (and .DAT if present) from this repo under /TPS.
- Target: Microsoft SQL Server (on‑prem or Azure SQL). Default DB name: ClarionAIProdoc.
- Outcome: One SQL table per TPS/DAT file, with columns inferred from the file’s data dictionary if available (via ODBC), or from field metadata read at runtime. Data loaded 1:1 into schema [clarion].

Two supported paths
1) ODBC‑based (recommended when you have the Clarion/TopSpeed ODBC driver):
   - Uses the official TopSpeed (*.TPS) ODBC driver on Windows to read TPS/DAT directly.
   - Export to CSV, then bulk‑load to SQL Server.

2) Reader‑based (no ODBC):
   - Optional C# utility (tools/TpsToCsv) that uses a TopSpeed reader NuGet to dump tables to CSV.
   - Same bulk‑load step into SQL Server.

Repository layout (this branch)
- TPStoSQL/
  - README-TPStoSQL.md (this file)
  - config/
    - example.config.json (edit/copy to config.json)
  - scripts/
    - Export-TPS-to-CSV.ps1
    - Import-CSV-to-SQL.ps1
  - sql/
    - 1_create_db_and_schema.sql
  - tools/
    - TpsToCsv/ (C# console app; optional)

Prerequisites
- Windows 10/11 or Windows Server with PowerShell 5.1+ (or PowerShell 7 with WindowsCompatibility)
- SQL Server (2017+) or Azure SQL Database, with login that can CREATE DATABASE and BULK INSERT permissions
- If using Path 1 (ODBC): Clarion/TopSpeed ODBC driver installed. Example driver name used: "TopSpeed driver (*.tps)".
- .NET SDK 8.0+ if you plan to build the optional TpsToCsv tool.

Quick start (ODBC path)
1) Copy TPStoSQL/config/example.config.json to TPStoSQL/config/config.json and edit:
   - set SourceDir to the absolute path of your TPS directory (or leave default ./TPS relative to repo root)
   - set SqlServer, Database, Schema, and BatchSize to your environment
   - if your ODBC requires Owner/Password/Dictionary, fill under Odbc.ConnectionStringExtras

2) Open PowerShell in repo root and run:
   - ./TPStoSQL/scripts/Export-TPS-to-CSV.ps1 -Config ./TPStoSQL/config/config.json
   - ./TPStoSQL/scripts/Import-CSV-to-SQL.ps1  -Config ./TPStoSQL/config/config.json

3) Validate row counts and samples in SQL Server.

Quick start (Reader path, no ODBC)
1) Build TpsToCsv:
   - cd TPStoSQL/tools/TpsToCsv
   - dotnet restore
   - dotnet build -c Release
   - dotnet run -- --input "../../TPS" --output "../../_csv"
2) Import CSVs into SQL Server:
   - ./TPStoSQL/scripts/Import-CSV-to-SQL.ps1  -Config ./TPStoSQL/config/config.json -CsvDir "./_csv"

Idempotency and re‑runs
- CSV export overwrites existing CSVs of the same name
- Import script can drop/recreate staging tables; set DropAndRecreate in config

Data typing rules (import script)
- If a column type map is provided in config (per file), the script honors it.
- Otherwise infers from sample rows: integers → INT, decimals → DECIMAL(38,10), dates → DATETIME2, boolean → BIT, else NVARCHAR(max). Long binary fields (BLOB/MEMO) → VARBINARY(max) / NVARCHAR(max) depending on driver metadata.

Large text/BLOBs
- If the ODBC driver exposes BLOBs, they are exported as base64 in CSV; the import script will rehydrate into VARBINARY(max).

Ownership/Dictionary passwords
- Some Clarion apps protect tables with OWNER strings. Place these in Odbc.ConnectionStringExtras (Owner=xyz). If your files require a dictionary or aliasing, see your environment’s settings.

Validation
- Row counting (export vs import)
- Spot check key business tables (e.g., MENULIST, PLIST, QHELP) using SELECT TOP 10 ... ORDER BY (any index columns if known)

Performance tips
- Run on the same machine as SQL Server where possible
- Use a fast local disk for CSV staging
- Increase BatchSize in config for larger files

Rollback
- The import script uses TRUNCATE/CREATE; set DropAndRecreate=false to append instead. Use BEGIN TRAN/COMMIT blocks for controlled runs in production.

License and attribution
- This toolkit includes only our scripts. If you add third‑party ODBC drivers or NuGet packages, follow their licenses.

Support
- Open a PR on the TPStoSQL branch with logs and your config (without secrets) if you need help.
