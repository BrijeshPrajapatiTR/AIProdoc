#!/usr/bin/env bash
set -euo pipefail
pushd "$(dirname "$0")/.."
 dotnet restore
 dotnet build -c Debug
popd
printf "\nDone. Start with: dotnet run --project NetProcalc23Feb.Web\n"
