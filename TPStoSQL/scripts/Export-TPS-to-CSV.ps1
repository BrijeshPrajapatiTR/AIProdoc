param(
  [Parameter(Mandatory=$false)] [string]$Config = "./TPStoSQL/config/config.json"
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

function Read-Config($path){
  if(!(Test-Path $path)){ throw "Config not found: $path" }
  $json = Get-Content -Raw -Path $path | ConvertFrom-Json
  # resolve relative paths from repo root
  $repoRoot = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path)
  $resolve = {
    param($p)
    if([string]::IsNullOrWhiteSpace($p)){ return $null }
    if([System.IO.Path]::IsPathRooted($p)){ return (Resolve-Path $p).Path }
    return (Resolve-Path (Join-Path $repoRoot $p)).Path
  }
  $json.SourceDir = & $resolve $json.SourceDir
  $json.CsvDir    = & $resolve $json.CsvDir
  if(!(Test-Path $json.CsvDir)){ New-Item -ItemType Directory -Path $json.CsvDir | Out-Null }
  return $json
}

function New-OdbcConnectionString($cfg){
  $parts = @()
  $parts += "Driver={$($cfg.Odbc.DriverName)}"
  if($cfg.Odbc.Directory){ $parts += "Directory=$($cfg.Odbc.Directory)" }
  if($cfg.Odbc.ConnectionStringExtras){ $parts += $cfg.Odbc.ConnectionStringExtras.Trim(';') }
  return ($parts -join ';') + ';'
}

function Export-TableToCsv($cn, [string]$tablePath, [string]$csvOut){
  $tableName = [System.IO.Path]::GetFileName($tablePath)
  $sql = "SELECT * FROM [$tableName]"
  Write-Host "Exporting $tableName → $csvOut"
  $cmd = New-Object System.Data.Odbc.OdbcCommand($sql, $cn)
  $da  = New-Object System.Data.Odbc.OdbcDataAdapter($cmd)
  $dt  = New-Object System.Data.DataTable
  [void]$da.Fill($dt)

  # Convert BLOBs to base64 strings to preserve binary
  foreach($col in $dt.Columns){
    if($col.DataType -eq [byte[]]){
      foreach($row in $dt.Rows){
        if(-not [System.DBNull]::Value.Equals($row[$col])){
          $row[$col] = [System.Convert]::ToBase64String($row[$col])
        }
      }
    }
  }

  $dt | Export-Csv -NoTypeInformation -Encoding UTF8 -Path $csvOut
}

$cfg = Read-Config $Config
$cnStr = New-OdbcConnectionString $cfg
Write-Host "Using ODBC: $cnStr"

# Build file list
$files = @()
foreach($f in $cfg.Files){
  $files += Get-ChildItem -Path $cfg.SourceDir -Filter $f.Pattern -File -ErrorAction SilentlyContinue
}
if($files.Count -eq 0){ Write-Warning "No TPS/DAT files found in $($cfg.SourceDir)" }

$cn = New-Object System.Data.Odbc.OdbcConnection($cnStr)
$cn.Open()
try{
  foreach($file in $files){
    $csv = Join-Path $cfg.CsvDir ($file.BaseName + '.csv')
    Export-TableToCsv -cn $cn -tablePath $file.FullName -csvOut $csv
  }
}
finally{
  $cn.Close()
}

Write-Host "Export complete. CSVs in $($cfg.CsvDir)"
