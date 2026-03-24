param([switch]$NoRun)

# ensure local tools for ef
if (-not (Test-Path .config/dotnet-tools.json)) {
  dotnet new tool-manifest | Out-Null
}

dotnet tool install dotnet-ef --version 8.0.4 --local --no-cache --ignore-failed-sources | Out-Null

dotnet restore

dotnet build

# create migration if none exist
$migDir = "src/ProCalc.Web/Migrations"
$migs = Get-ChildItem $migDir -Filter "*.cs" -ErrorAction SilentlyContinue
if (-not $migs) {
  dotnet ef migrations add Initial --project src/ProCalc.Web --startup-project src/ProCalc.Web
}

# update database
 dotnet ef database update --project src/ProCalc.Web --startup-project src/ProCalc.Web

if (-not $NoRun) {
  dotnet run --project src/ProCalc.Web
}
