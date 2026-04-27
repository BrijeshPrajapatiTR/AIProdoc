#!/usr/bin/env bash
set -euo pipefail
SOLUTION_NAME="${1:-NetProcalc23Feb}"

echo "Restoring packages..."
dotnet restore src/${SOLUTION_NAME}.Web/${SOLUTION_NAME}.Web.csproj

echo "Ensuring database exists..."
dotnet run --project src/${SOLUTION_NAME}.Web/${SOLUTION_NAME}.Web.csproj -- --initdb

echo "Done. Start the app with: dotnet run --project src/${SOLUTION_NAME}.Web/${SOLUTION_NAME}.Web.csproj"