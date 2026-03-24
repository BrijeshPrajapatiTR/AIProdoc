#!/usr/bin/env bash
set -euo pipefail
cd "$(dirname "$0")/.."
command -v dotnet >/dev/null 2>&1 || { echo 'dotnet SDK is required'; exit 1; }
 dotnet restore NetProcalc23Feb/NetProcalc23Feb.sln
 dotnet build NetProcalc23Feb/NetProcalc23Feb.sln -c Release
 dotnet tool restore
 pushd NetProcalc23Feb/NetProcalc23Feb.Web
 if [ ! -d Migrations ]; then dotnet ef migrations add InitialCreate; fi
 dotnet ef database update
 popd
 dotnet run --project NetProcalc23Feb/NetProcalc23Feb.Web --no-build &
 sleep 5
 curl -X POST http://localhost:5249/admin/import-txa | jq .
