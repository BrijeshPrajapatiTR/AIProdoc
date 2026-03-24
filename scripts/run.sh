#!/usr/bin/env bash
set -euo pipefail
if ! command -v dotnet >/dev/null 2>&1; then echo "Install .NET 8 SDK"; exit 1; fi
(dotnet restore && dotnet build -c Debug)
# create migration if not exists
if ! ls src/AIProdoc.Web/Data/Migrations/*_Init*.cs >/dev/null 2>&1; then 
  dotnet tool update --global dotnet-ef >/dev/null 2>&1 || true
  dotnet ef migrations add Init --project src/AIProdoc.Web --output-dir Data/Migrations -c AIProdoc.Web.Data.AppDbContext -s src/AIProdoc.Web || true
fi
(dotnet ef database update -p src/AIProdoc.Web -s src/AIProdoc.Web) || true
DOTNET_URLS=${DOTNET_URLS:-http://localhost:5186}
export DOTNET_URLS
exec dotnet run --project src/AIProdoc.Web
