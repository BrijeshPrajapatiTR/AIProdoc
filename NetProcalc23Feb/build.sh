#!/usr/bin/env bash
set -euo pipefail
 dotnet restore
 dotnet build -c Release
 dotnet tool update --global dotnet-ef
 pushd NetProcalc23Feb.Web
 if [ ! -d "Migrations" ]; then
   echo "Creating InitialCreate migration..."
   dotnet ef migrations add InitialCreate
 fi
 dotnet ef database update
 dotnet run
 popd
