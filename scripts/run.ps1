param([switch]$NoBuild)
$ErrorActionPreference = 'Stop'
if (-not $NoBuild) { dotnet restore; dotnet build -c Debug }
dotnet ef migrations add Init --project src/AIProdoc.Web --output-dir Data/Migrations -c AIProdoc.Web.Data.AppDbContext -s src/AIProdoc.Web -v 2>$null | Out-Null
# Ignore error if migration exists
try { dotnet ef database update -p src/AIProdoc.Web -s src/AIProdoc.Web } catch {}
dotnet run --project src/AIProdoc.Web
