#!/usr/bin/env bash
set -euo pipefail
 dotnet restore
 dotnet build -c Release
 dotnet tool update --global dotnet-ef
 pushd NetProcalc23Feb.Web
 dotnet ef database update
 dotnet run
 popd
