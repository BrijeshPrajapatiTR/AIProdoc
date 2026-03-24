Clarion → ASP.NET mapping (initial)

- Child support calculator for judges (general tab) → GET/POST /Calculators/ChildSupport (Views/Calculators/ChildSupport.cshtml)
- Child support calculator for judges (adjustment tab) → planned Razor Partial under same view
- Delinquent Support calculator (Name/Obligations/Payments/Debit/Judgment tabs) → Parties/Obligations/Payments CRUD pages (scaffold next) and computation endpoint /api/cases/{id}/balance
- File menu → Navbar File dropdown in _Layout
- New Party → GET/POST /Parties/Create
- Parties and case → planned: /Cases (browse) and /Cases/Edit/{id}
- Obligation Amortizer → planned: /Calculators/ObligationAmortizer

This matrix will be extended as we parse procalc.app procedures and windows.
