# 📜 Changelog

All notable changes to the Petty Cash Management System will be documented in this file.

## [1.3.0] - 2026-03-28 (Current)
### ✨ Added
- **Dynamic Category Management:** Admins can now Add, Edit, and Delete expense categories directly from the UI.
- **Database Restore:** Full disaster recovery capability allowing admins to restore the database from any `.sqlite_bak` file.
- **Universal Reporting:** Both HTML and Excel reports now automatically adjust to any number of used categories (no more hardcoding).

### 🚀 Improved
- **Professional Excel Exports:**
  - Implemented real-time `SUM()` formulas and dynamic summary sentences.
  - Enforced strict `Decimal` numeric types for all amounts (eliminated "green corner" text warnings).
  - Added **Adaptive Layout**: Automatically switches to Landscape mode if the report contains many categories.
- **Data Integrity:**
  - Fixed "Missing Record" bug by decoupling reports from specific category IDs.
  - Implemented **BR11 (December Cross-Month Rule)**: Expenses from Dec 15–31 can now be reported in January.
  - Relaxed validation to allow "User Freedom" when editing past months.

### 📝 Documentation
- Created `docs/bug-reporting-guide.md`.
- Consolidated all "After Deployed" notes into official documentation set.
- Reorganized `docs/` for GitHub portfolio presentation.
- Updated **Inno Setup Script (Setup.iss)** to v1.3.0 with modern feature highlights.

## [1.2.1] - 2026-03-23
### 🐛 Fixed
- **Deployment bug:** CRUD operations failed on target PCs after a clean install.
  - Added MSBuild post-publish target to copy `{AssemblyName}.dll.config → {AssemblyName}.exe.config` so .NET 8 self-contained apps can read their config file.
  - Hardened `Program.vb` startup to always call `DbContext.InitializeSchema()` before loading any form, ensuring the SQLite schema exists on any machine.
  - Added startup diagnostic log file (`startup.log`) written to `AppData` to capture silent failures for future troubleshooting.

### 📝 Documentation
- Added root-level `ARCHITECTURE.md` with full 3-tier system explainer and data flow diagram.
- Expanded `DEVELOPMENT_JOURNEY.md` with Phase 5 deployment fix story.
- Removed stale `packages.config` (project uses SDK-style `<PackageReference>` elements).

## [1.2.0] - 2026-03-05
### ✨ Added
- New professional documentation suite for NVQ Assessment.
- `docs/` directory with merged User, Admin, and Technical guides.
- `DEVELOPMENT_JOURNEY.md` documenting the project's evolution and struggles.
- `.gitignore` for clean project management.

### 🧹 Changed
- Cleaned up root directory (removed 40+ build logs and temp files).
- Moved duplicate orphaned VB files to respective project directories.
- Restructured `Installer/` directory to remove binary bloat.

## [1.1.5] - 2026-03-05
### 🔄 Migrated
- **Database Migration:** Migrated from SQL Server Express to **SQLite**!
- Updated `DbContext.vb` to support SQLite zero-configuration.
- Updated `Program.vb` with automatic schema initialization logic.
- Simplified `App.config` for local data storage in `AppData`.

### 🚀 Improved
- Startup logic: Added auto-reset for admin password during testing.
- Error handling: Detailed startup diagnostics for SQLite connections.

## [1.1.0] - 2026-02-24
### ✨ Added
- **Database Backup:** Admin-only feature to create `.bak` backups (SQL Server).
- **Excel Export:** Professional `.xlsx` export using ClosedXML.
- Bulk export dialog for selecting multiple months.

### 📝 Documentation
- Created `BACKUP_AND_EXPORT_GUIDE.md`.

## [1.0.0] - 2026-02-09
### 🎉 Initial Release
- Core petty cash accounting features.
- 3-Tier architecture implementation.
- Role-based authentication (Admin/Clerk/Viewer).
- Business rule validation (BR1-BR8).
- Audit logging for all actions.
- Text-based report generation and printing.
