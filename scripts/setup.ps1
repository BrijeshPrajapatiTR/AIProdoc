param()
Write-Host "Restoring .NET tools and packages..."
dotnet restore NetProcalc23Feb.sln
Write-Host "Creating SQLite database if not exists..."
dotnet run --project src/NetProcalc23Feb.Web/NetProcalc23Feb.Web.csproj -- --initdb
Write-Host "Done. Now run: dotnet run --project src/NetProcalc23Feb.Web/NetProcalc23Feb.Web.csproj" -ForegroundColor Green
