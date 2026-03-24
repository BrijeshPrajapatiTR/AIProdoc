# Data import from Clarion TPS/ODBC

Options
1) Topspeed (TPS) ODBC driver
- Install driver on Windows host
- Create a DSN to the folder containing .TPS files
- PowerShell example to pump into SQL Server (or export to CSV)

```powershell
$dsn = 'DSN=ClarionTPS;UID=;PWD='
$tables = @('PARTIES','CASES','OBLIG','PAYMENTS')
$conn = New-Object System.Data.Odbc.OdbcConnection $dsn
$conn.Open()
foreach($t in $tables){
  $cmd = $conn.CreateCommand(); $cmd.CommandText = "SELECT * FROM $t";
  $r = $cmd.ExecuteReader(); $dt = New-Object System.Data.DataTable; $dt.Load($r)
  $path = Join-Path (Get-Location) "$t.csv"; $dt | Export-Csv -NoTypeInformation -Path $path
}
$conn.Close()
```

2) CSV → EF Core
- Convert to CSV as above then write a small importer that maps columns to Entities and bulk-inserts via EF Core.

Switching DB providers
- Change ConnectionStrings:Default in appsettings.json to SQL Server or PostgreSQL
- Run `dotnet ef database update` to create schema
