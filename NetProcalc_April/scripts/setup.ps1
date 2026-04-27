param([switch]$NoRestore)
Write-Host "Setting up NetProcalc_April..." -ForegroundColor Cyan
if(-not $NoRestore){ dotnet restore "$PSScriptRoot/.." }
dotnet build "$PSScriptRoot/../NetProcalc23Feb.Web/NetProcalc23Feb.Web.csproj" -c Debug
