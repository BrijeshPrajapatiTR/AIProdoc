Data import from Clarion TPS / ODBC to RDBMS

1) Inventory Clarion files (.TPS) in the deployment. For ODBC sources, export to CSV via Clarion app or ODBC driver.
2) Choose target DB: SQL Server (prod), Sqlite (dev). Create tables mirroring Domain entities.
3) Use open-source TPS reader (e.g., TopSpeedSharp) or export to CSV via Clarion. Then run the import tool below.

Example import with CsvHelper
- Create CSVs: Parties.csv, Cases.csv, Obligations.csv, Payments.csv, Judgments.csv
- Run: dotnet run --project NetProcalc23Feb.Web --import ./data

Web Program.cs can be extended to detect --import and bulk load.

Mappings
- Parties: FirstName, LastName, Role
- Cases: CaseNumber, County, OpenedOn
- Obligations: CaseId ref, Type, Amount, Frequency, StartDate, EndDate, InterestRateAnnual
- Payments: CaseId ref, PaidOn, Amount
- Judgments: CaseId ref, Title, Principal, InterestRateAnnual, AsOf

Validation
- Use FluentValidation (to be added per-entity) for required fields and ranges.
