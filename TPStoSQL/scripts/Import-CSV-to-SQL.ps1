param(
  [Parameter(Mandatory=$false)] [string]$Config = "./TPStoSQL/config/config.json",
  [Parameter(Mandatory=$false)] [string]$CsvDir
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
Add-Type -AssemblyName System.Data

function Read-Config($path){
  if(!(Test-Path $path)){ throw "Config not found: $path" }
  $json = Get-Content -Raw -Path $path | ConvertFrom-Json
  $repoRoot = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path)
  $resolve = {
    param($p)
    if([string]::IsNullOrWhiteSpace($p)){ return $null }
    if([System.IO.Path]::IsPathRooted($p)){ return (Resolve-Path $p).Path }
    return (Resolve-Path (Join-Path $repoRoot $p)).Path
  }
  $json.SourceDir = & $resolve $json.SourceDir
  if(-not $CsvDir){ $json.CsvDir = & $resolve $json.CsvDir } else { $json.CsvDir = & $resolve $CsvDir }
  return $json
}

function Invoke-SqlNonQuery([string]$cnn,[string]$sql){
  $cn = New-Object System.Data.SqlClient.SqlConnection($cnn)
  $cn.Open()
  try{
    $cmd = $cn.CreateCommand(); $cmd.CommandText = $sql; $cmd.CommandTimeout = 0
    [void]$cmd.ExecuteNonQuery()
  }finally{ $cn.Close() }
}

function Get-SqlConnectionString($cfg){
  # Integrated security when connecting to local default instance; otherwise trust what user sets via environment
  $builder = New-Object System.Data.SqlClient.SqlConnectionStringBuilder
  $builder['Data Source'] = $cfg.SqlServer
  $builder['Initial Catalog'] = 'master'
  $builder['Integrated Security'] = $true
  return $builder.ToString()
}

function Ensure-Database-And-Schema($cfg){
  $cnn = Get-SqlConnectionString $cfg
  Invoke-SqlNonQuery $cnn "IF DB_ID('$($cfg.Database)') IS NULL CREATE DATABASE [$($cfg.Database)];"
  $builder = New-Object System.Data.SqlClient.SqlConnectionStringBuilder ($cnn)
  $builder['Initial Catalog'] = $cfg.Database
  $cnnDb = $builder.ToString()
  Invoke-SqlNonQuery $cnnDb "IF SCHEMA_ID('$($cfg.Schema)') IS NULL EXEC('CREATE SCHEMA [$($cfg.Schema)]');"
  return $cnnDb
}

function Infer-Type([string]$value){
  if([string]::IsNullOrEmpty($value)){ return 'NVARCHAR(MAX)' }
  if($value -match '^(TRUE|FALSE|0|1)$'){ return 'BIT' }
  if([int]::TryParse($value, [ref]0)) { return 'INT' }
  if([decimal]::TryParse($value, [ref]0)) { return 'DECIMAL(38,10)' }
  if([datetime]::TryParse($value, [ref](Get-Date))) { return 'DATETIME2(3)' }
  # base64-ish → assume VARBINARY if looks like long and valid
  if($value.Length -gt 64 -and $value -match '^[A-Za-z0-9+/=]+$'){ return 'VARBINARY(MAX)' }
  return 'NVARCHAR(MAX)'
}

function New-Table-From-Csv($cnnDb,[string]$schema,[string]$table,[string]$csv,[hashtable]$overrides){
  $rows = Import-Csv -Path $csv
  if($rows.Count -eq 0){ Write-Warning "CSV empty: $csv"; return }
  $sample = $rows | Select-Object -First 500  # sample up to 500 rows for inference
  $cols = @()
  foreach($k in $rows[0].PSObject.Properties.Name){
    $override = $null
    if($overrides -and $overrides.ContainsKey($k)){ $override = $overrides[$k] }
    if($override){ $cols += "[$k] $override NULL" }
    else{
      $nonNull = $sample | Where-Object { $_.$k -ne $null -and $_.$k -ne '' } | Select-Object -ExpandProperty $k -First 1
      $sqlType = Infer-Type $nonNull
      $cols += "[$k] $sqlType NULL"
    }
  }
  $ddl = "IF OBJECT_ID('[$schema].[$table]','U') IS NULL CREATE TABLE [$schema].[$table] (" + ($cols -join ',') + ");"
  Invoke-SqlNonQuery $cnnDb $ddl
}

function Bulk-Import($cnnDb,[string]$schema,[string]$table,[string]$csv){
  $dt = Import-Csv -Path $csv
  if($dt.Count -eq 0){ return }
  $cn = New-Object System.Data.SqlClient.SqlConnection($cnnDb)
  $cn.Open()
  try{
    if($script:DropAndRecreate){
      $cmd = $cn.CreateCommand(); $cmd.CommandText = "TRUNCATE TABLE [$schema].[$table]"; try{[void]$cmd.ExecuteNonQuery()}catch{}
    }
    $bc = New-Object System.Data.SqlClient.SqlBulkCopy($cn)
    $bc.DestinationTableName = "[$schema].[$table]"
    $bc.BatchSize = $script:BatchSize
    foreach($k in $dt[0].PSObject.Properties.Name){ $bc.ColumnMappings.Add($k,$k) | Out-Null }

    # Build DataTable to coerce VARBINARY columns from base64
    $da = New-Object System.Data.DataTable
    foreach($name in $dt[0].PSObject.Properties.Name){ [void]$da.Columns.Add($name, [string]) }
    foreach($row in $dt){
      $dr = $da.NewRow()
      foreach($name in $dt[0].PSObject.Properties.Name){ $dr[$name] = $row.$name }
      $da.Rows.Add($dr) | Out-Null
    }
    $bc.WriteToServer($da)
  }finally{ $cn.Close() }
}

$cfg = Read-Config $Config
$script:DropAndRecreate = $cfg.DropAndRecreate
$script:BatchSize = $cfg.BatchSize

$cnnDb = Ensure-Database-And-Schema $cfg

# Resolve overrides per-file
$overrides = @{}
if($cfg.TypeOverrides){ $overrides = $cfg.TypeOverrides }

# Iterate CSVs
$csvFiles = Get-ChildItem -Path $cfg.CsvDir -Filter *.csv -File
foreach($csv in $csvFiles){
  $table = $csv.BaseName
  $fileOverrides = @{}
  if($overrides.ContainsKey($csv.Name)){ $fileOverrides = $overrides[$csv.Name] }
  elseif($overrides.ContainsKey($csv.BaseName + '.TPS')){ $fileOverrides = $overrides[$csv.BaseName + '.TPS'] }
  New-Table-From-Csv -cnnDb $cnnDb -schema $cfg.Schema -table $table -csv $csv.FullName -overrides $fileOverrides
  Bulk-Import -cnnDb $cnnDb -schema $cfg.Schema -table $table -csv $csv.FullName
  Write-Host "Imported $($csv.Name) → [$($cfg.Schema)].[$table]"
}

Write-Host "Import complete into database $($cfg.Database)."
