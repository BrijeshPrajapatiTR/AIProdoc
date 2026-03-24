param()
Write-Host "Restoring and building NetProcalc23Feb..." -ForegroundColor Cyan
set -e
pushd $PSScriptRoot\..
 dotnet restore
 dotnet build -c Debug
popd
Write-Host "Done. Use: dotnet run --project NetProcalc23Feb.Web" -ForegroundColor Green
