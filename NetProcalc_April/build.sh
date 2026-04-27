#!/usr/bin/env bash
set -euo pipefail
cd "$(dirname "$0")"
echo "Restoring..."
dotnet restore NetProcalc_April.sln
echo "Building..."
dotnet build NetProcalc_April.sln -c Release --nologo
if [ "${1:-}" = "--run" ]; then
  echo "Running Web..."
  dotnet run --project ./NetProcalc.Web/NetProcalc.Web.csproj --no-build
fi
