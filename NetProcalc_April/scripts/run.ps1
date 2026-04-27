$ErrorActionPreference = 'Stop'
$root = Split-Path $MyInvocation.MyCommand.Path -Parent
$web = Join-Path $root '..\NetProcalc23Feb.Web\NetProcalc23Feb.Web.csproj'
dotnet run --project $web --launch-profile "http"
