param([switch]$Run)
Push-Location $PSScriptRoot
Write-Host "Restoring..." -ForegroundColor Cyan
dotnet restore NetProcalc_April.sln
Write-Host "Building..." -ForegroundColor Cyan
dotnet build NetProcalc_April.sln -c Release -warnaserror-
if ($Run) {
  Write-Host "Running Web..." -ForegroundColor Green
  dotnet run --project .\NetProcalc.Web\NetProcalc.Web.csproj --no-build
}
Pop-Location
