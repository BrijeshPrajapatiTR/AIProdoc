param([switch]$Run)
$ErrorActionPreference = 'Stop'
Write-Host 'Restoring and building...'
dotnet restore
dotnet build -c Release
if ($Run) {
  dotnet tool update --global dotnet-ef
  Push-Location NetProcalc23Feb.Web
  if (-not (Test-Path ./Migrations)) {
    Write-Host 'Creating InitialCreate migration...'
    dotnet ef migrations add InitialCreate
  }
  dotnet ef database update
  dotnet run
  Pop-Location
}
