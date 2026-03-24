param(
  [string]$SolutionName = "NetProcalc23Feb",
  [string]$DbProvider = "sqlite"
)

$ErrorActionPreference = "Stop"

# Create folders
New-Item -ItemType Directory -Force -Path src | Out-Null

# Write projects that already exist in src/; restore and create DB
Write-Host "Restoring packages..."
dotnet restore src/$SolutionName.Web/$SolutionName.Web.csproj

Write-Host "Ensuring database exists..."
dotnet run --project src/$SolutionName.Web/$SolutionName.Web.csproj -- --initdb

Write-Host "Done. Start the app with: dotnet run --project src/$SolutionName.Web/$SolutionName.Web.csproj"