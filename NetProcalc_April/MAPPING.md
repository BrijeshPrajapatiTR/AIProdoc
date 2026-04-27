Clarion -> ASP.NET routing matrix (initial pass)

Menus (from Expected output screenshots)
- File Menu (Clarion) -> Navbar File dropdown (Layout)
  - Parties and case -> GET /Party (PartyController.Index, Views/Party/Index.cshtml)
  - New Party -> GET /Party/Create, POST /Party/Create (PartyController, Create.cshtml)
- Powerpack -> Placeholder, to be mapped after parsing Clarion .app/.txa

Calculators (planned routes)
- Child support calculator for judges
  - General tab -> GET /Calculators/ChildSupport/General (Razor Page)
  - Adjustment tab -> GET /Calculators/ChildSupport/Adjustments (Razor Page)
- Delinquent Support calculator
  - Name -> GET /Calculators/Delinquent/Name
  - Obligations -> GET /Calculators/Delinquent/Obligations
  - Payments -> GET /Calculators/Delinquent/Payments
  - Debit -> GET /Calculators/Delinquent/Debit
  - Judgment -> GET /Calculators/Delinquent/Judgment
- Obligation Amortizer -> GET /Tools/Amortizer
- Semi-monthly/Biweekly/Weekly amounts -> GET /Tools/PeriodAmount

Notes
- Full procedure mapping will be expanded after parsing procalc.app and procalc.txa to list procedure names and window definitions.
