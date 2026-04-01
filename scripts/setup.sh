#!/usr/bin/env bash
set -euo pipefail
printf "Restoring packages...\n"
dotnet restore NetProcalc23Feb.sln
printf "Creating SQLite database if not exists...\n"
dotnet run --project src/NetProcalc23Feb.Web/NetProcalc23Feb.Web.csproj -- --initdb
printf "Done. Now run: dotnet run --project src/NetProcalc23Feb.Web/NetProcalc23Feb.Web.csproj\n"
