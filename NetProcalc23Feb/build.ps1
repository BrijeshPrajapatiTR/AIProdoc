param([switch]$Run)
$ErrorActionPreference = 'Stop'
Write-Host 'Restoring and building...'
dotnet restore
dotnet build -c Release
if ($Run) {
  dotnet tool update --global dotnet-ef
  Push-Location NetProcalc23Feb.Web
  dotnet ef database update
  dotnet run
  Pop-Location
}
