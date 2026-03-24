param([switch]$RecreateDb)
$ErrorActionPreference = 'Stop'
Push-Location $PSScriptRoot/..
if (-not (Get-Command dotnet -ErrorAction SilentlyContinue)) { throw 'dotnet SDK is required' }
Write-Host 'Restoring...' ; dotnet restore NetProcalc23Feb/NetProcalc23Feb.sln
Write-Host 'Building...' ; dotnet build NetProcalc23Feb/NetProcalc23Feb.sln -c Release
if ($RecreateDb) { if (Test-Path netprocalc.db) { Remove-Item netprocalc.db -Force } }
Write-Host 'Running EF migrations (ensure design-time tools installed)'
dotnet tool restore
Push-Location NetProcalc23Feb/NetProcalc23Feb.Web
if (-not (Test-Path .\Migrations)) { dotnet ef migrations add InitialCreate }
dotnet ef database update
Pop-Location
Write-Host 'Seeding from Clarion TXA via API...'
dotnet run --project NetProcalc23Feb/NetProcalc23Feb.Web --no-build &
Start-Sleep -Seconds 5
Invoke-RestMethod -Method Post -Uri http://localhost:5249/admin/import-txa | ConvertTo-Json -Depth 5
Pop-Location
