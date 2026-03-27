TPS → SQL (SQL Server) conversion plan using TPSparser

Overview
- Source branch: TPSBranch (working branch: TPStoSQL)
- Input folder: TPS/
- Output folder: sql_output/
- Target SQL dialect: SQL Server (MSSQL)
- Tool: TPSparser CLI (replace the executable name and flags below if they differ in your setup)

Assumptions
- Executable path: C:\\Tools\\TPSparser\\TPSparser.exe (Windows) or ./TPSparser/tpsparser (Git Bash)
- CLI accepts: -i <input.tps> -o <output.sql> --dialect mssql

Windows PowerShell (recommended)
1) Ensure you are on the TPStoSQL branch based on TPSBranch
   git fetch origin TPSBranch
   git checkout TPStoSQL

2) Create output folder (if not present)
   New-Item -ItemType Directory -Path sql_output -Force | Out-Null

3) Convert all .TPS files to .sql (SQL Server dialect)
   Get-ChildItem -Path .\TPS -Filter *.tps | ForEach-Object {
     & "C:\\Tools\\TPSparser\\TPSparser.exe" -i $_.FullName -o ("sql_output\\" + ($_.BaseName + ".sql")) --dialect mssql
   }

Windows Command Prompt (cmd.exe)
1) Ensure you are on the TPStoSQL branch based on TPSBranch
   git fetch origin TPSBranch
   git checkout TPStoSQL

2) Create output folder
   if not exist sql_output mkdir sql_output

3) Convert
   for %%F in (TPS\*.tps) do "C:\\Tools\\TPSparser\\TPSparser.exe" -i "%%F" -o "sql_output\%%~nF.sql" --dialect mssql

Git Bash (Windows) or Bash (macOS/Linux)
1) Ensure you are on the TPStoSQL branch based on TPSBranch
   git fetch origin TPSBranch
   git checkout TPStoSQL

2) Create output folder
   mkdir -p sql_output

3) Convert
   for f in TPS/*.tps; do ./TPSparser/tpsparser -i "$f" -o "sql_output/$(basename "${f%.*}").sql" --dialect mssql; done

Commit and push the generated .sql files
   git add sql_output/*.sql
   git commit -m "Converted TPS files to SQL"
   git push -u origin TPStoSQL

Notes
- If TPSparser uses different flags, keep the loop structure and swap the executable/flags accordingly.
- If any .TPS files require schema metadata, ensure TPSparser is configured to emit CREATE TABLE/INSERT statements for SQL Server.
- After push, open a PR from TPStoSQL to TPSBranch or main as preferred.
