AIProdoc migration workspace

This repository contains Clarion artifacts (procalc.app / .cwproj / .txa). A new branch `dotnetAI` hosts a runnable ASP.NET Core 8 migration target (src/AIProdoc.Web) with menu + browse/update forms and BusinessRules stubs.

Quick start (March 24, 2026)
- Windows:  scripts\\run.ps1
- macOS/Linux: scripts/run.sh
Then open http://localhost:5186

Notes
- Target framework: net8.0 (the correct TFM for .NET 8 LTS). There is no net8.1 TFM; patch versions are runtime updates.
- Next: we will parse procalc.txa and wire up domain models and procedures.
