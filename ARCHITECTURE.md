# 🏗️ System Architecture

**Petty Cash Management System — Ceylon Electricity Board, Haliela**

---

## Overview

The system is a **Windows desktop application** built on a classic **3-Tier Architecture** pattern. This design separates concerns cleanly, making it easy to test, maintain, and extend each layer independently.

```
┌──────────────────────────────────────────────────────────────┐
│                 PRESENTATION LAYER (WinForms)                │
│                                                              │
│  LoginForm  ·  DashboardForm  ·  ExpenseEntryForm           │
│  ReportViewerForm  ·  AdminSettingsForm  ·  BackupForm      │
│  UserManagementForm  ·  UserEditForm  ·  UserPermissionForm  │
│  BulkExportForm  ·  SinhalaMonthSettingsForm                 │
└─────────────────────────┬────────────────────────────────────┘
                          │  Calls Services
┌─────────────────────────▼────────────────────────────────────┐
│              BUSINESS LOGIC LAYER (Services)                 │
│                                                              │
│  AuthService       ·  ExpenseService   ·  ReportService      │
│  ValidationService ·  AuditService     ·  BackupService      │
│  ExcelExportService ·  BulkExportService                     │
│  PermissionService  ·  UserManagementService                 │
│  NotificationService ·  WhatsAppService                      │
└─────────────────────────┬────────────────────────────────────┘
                          │  Calls Repositories
┌─────────────────────────▼────────────────────────────────────┐
│              DATA ACCESS LAYER (Repositories)                │
│                                                              │
│  DbContext         ·  IRepository<T>  (interface)           │
│  UserRepository    ·  ExpenseRepository  ·  CategoryRepo     │
│  AuditLogRepository  ·  PermissionRepository                 │
└─────────────────────────┬────────────────────────────────────┘
                          │  SQL via System.Data.SQLite
                  ┌───────▼────────┐
                  │   PettyCash.db │
                  │   (SQLite 3)   │
                  └────────────────┘
```

---

## Layer Details

### 1. Presentation Layer — `PettyCash.DesktopApp/Forms/`

All user-facing Windows Forms. Responsibilities:
- Render data from Services
- Capture user input and validate basic UI constraints
- Delegate all business decisions to the Service layer (no SQL here)
- Display `OperationResult` feedback (success/failure messages)

| Form | Purpose |
|------|---------|
| `LoginForm` | Authenticates users; uses `AuthService` + BCrypt |
| `DashboardForm` | Shows monthly summary, progress bar, expense grid |
| `ExpenseEntryForm` | Add / Edit / Delete expenses with BR validation |
| `ReportViewerForm` | Preview, print, and export monthly reports |
| `AdminSettingsForm` | Category management, Sinhala month names, system config |
| `UserManagementForm` | List users; create, activate, deactivate accounts |
| `UserEditForm` | Edit user details and role |
| `UserPermissionForm` | Granular per-user permission toggles (22 keys) |
| `CategoryManagementForm` | Admin tool to add/edit/delete expense categories |
| `BackupForm` | One-click SQLite database backup and full restore |
| `BulkExportForm` | Multi-month XLSX export |

---

### 2. Business Logic Layer — `PettyCash.DesktopApp/Services/`

Pure business logic. No UI references. No direct SQL.

| Service | Responsibility |
|---------|---------------|
| `AuthService` | Login, logout, BCrypt password verification |
| `ExpenseService` | CRUD for expenses, orchestrates validation |
| `ValidationService` | Enforces BR1–BR9 business rules |
| `ReportService` | Monthly summary, data aggregation for reports |
| `AuditService` | Records every user action to `audit_logs` |
| `PermissionService` | Checks per-user permission keys |
| `UserManagementService` | Create/edit/deactivate user accounts |
| `BackupService` | Copies SQLite file to backup location |
| `ExcelExportService` | Generates XLSX via ClosedXML for a single month |
| `BulkExportService` | Generates multi-month XLSX export |

---

### 3. Data Access Layer — `PettyCash.DesktopApp/Repositories/`

All SQL is isolated here. No business logic. Uses the **Repository Pattern**.

| Class | Role |
|-------|------|
| `DbContext` | Opens/closes SQLite connection; auto-creates schema on first run |
| `IRepository(Of T)` | Generic interface — `GetAll`, `GetById`, `Add`, `Update`, `Delete` |
| `UserRepository` | CRUD for `users` table |
| `ExpenseRepository` | CRUD + filtered queries on `expense_entries` |
| `CategoryRepository` | CRUD for `expense_categories` |
| `AuditLogRepository` | Insert-only audit log writes |
| `PermissionRepository` | Read/write per-user permission rows |

---

### 4. Supporting Components

#### Models — `PettyCash.DesktopApp/Models/`
Plain data objects (POCOs) passed between layers.

| Model | Maps To |
|-------|---------|
| `User` | `users` table |
| `Expense` | `expense_entries` table |
| `Category` | `expense_categories` table |
| `AuditLog` | `audit_logs` table |
| `Permission` | `user_permissions` table |
| `OperationResult` | Service return envelope (success, message, data) |
| `ValidationResult` | Rule violation details from `ValidationService` |

#### Utilities — `PettyCash.DesktopApp/Utilities/`

| Utility | Purpose |
|---------|---------|
| `Constants` | System-wide string/numeric constants |
| `SessionManager` | Holds the currently logged-in `User` for the app lifetime |
| `ConfigManager` | Wraps `ConfigurationManager` for App.config reads |
| `Enums` | Shared enumerations (roles, result types) |
| `PermissionKeys` | String constants for all 22 permission keys |
| `ReportHtmlGenerator` | Builds HTML for print preview |
| `SinhalaMonthSettings` | Loads Sinhala month names from the database |

---

## Key Design Decisions

### Decoupled Reporting Architecture

One of the most significant architectural improvements in v1.3.0 was the decoupling of the **Transaction Date** from the **Report Month**.

- **Schema:** Added `report_month` and `report_year` columns to the database.
- **Filtering:** All dashboard and report queries filter by these explicit tracking columns rather than deriving them from the record's entry date.
- **The "December Rule":** This allows Dec 15–31 expenses to be assigned to the **January** report for year-end processing.
- **Universal Logic:** The `ReportService` now dynamically discovers categories in the month's data, ensuring the system is zero-maintenance when new categories are added.

### Repository Pattern
Decouples business logic from SQL. If we ever needed to swap SQLite for PostgreSQL or a REST API, only the repository layer changes.

### SQLite — Zero-Configuration Database
**Why not SQL Server?** After struggling with SQL Server deployment (installer hell, firewall rules, service permissions), we migrated to SQLite. Benefits:
- No server process required
- Database is a single `.db` file in `%AppData%`
- `DbContext` auto-creates the schema on first launch using embedded SQL
- Zero setup for end users

### BCrypt Password Hashing
Passwords are never stored in plain text. `BCrypt.Net-Next` hashes them with a work factor of 12 before storage, and uses constant-time comparison to prevent timing attacks.

### Auto-Schema Initialization
`Program.vb` calls `DbContext.InitializeSchema()` on every startup. This runs `CREATE TABLE IF NOT EXISTS` statements, so the database is always ready even after a fresh install on a new PC — no manual SQL scripts needed.

### OperationResult<T>
Services never throw exceptions to the UI. They return an `OperationResult(Of T)` with `Success`, `Message`, and optional `Data`. Forms inspect this and display friendly feedback.

---

## Data Flow Example: Adding an Expense

```
User fills ExpenseEntryForm
        │
        ▼
ExpenseEntryForm.btnSave_Click()
        │  Creates Expense model from UI fields
        ▼
ExpenseService.AddExpense(expense)
        │  Calls ValidationService.Validate(expense)
        │  ├─ BR1: Monthly total ≤ 25,000?
        │  ├─ BR2: Single bill ≤ 5,000?
        │  ├─ BR3-BR9: (date, category, duplicate bill, etc.)
        │  └─ Returns ValidationResult
        │
        │  If valid → calls ExpenseRepository.Add(expense)
        │  If invalid → returns OperationResult.Failed(message)
        ▼
ExpenseRepository.Add(expense)
        │  Opens DbContext connection
        │  Executes parameterized INSERT
        └─ Returns new expense ID
        │
        ▼
AuditService.Log("ADD_EXPENSE", details)
        │  Writes to audit_logs table
        ▼
Returns OperationResult.OK() to form
        │
ExpenseEntryForm shows success message
```

---

## Database Schema Summary

```sql
users              -- login credentials, role, active flag
expense_entries    -- date, bill_no, category_id, description, amount, month_finalized
expense_categories -- code (E5200 etc.), name, description
user_permissions   -- user_id, permission_key, granted (bool)
audit_logs         -- user_id, action, details, timestamp
sinhala_months     -- month_number, sinhala_name
```

Full schema: [`SQL/schema_sqlite.sql`](SQL/schema_sqlite.sql)

---

## Technology Stack

| Component | Technology | Version |
|-----------|-----------|---------|
| Language | VB.NET | — |
| Framework | .NET 8.0 (Windows Forms) | net8.0-windows |
| Database | SQLite 3 | via System.Data.SQLite 1.0.119 |
| Password Hashing | BCrypt.Net-Next | 4.0.3 |
| Excel Export | ClosedXML | 0.102.3 |
| Configuration | System.Configuration.ConfigurationManager | 8.0.0 |
| IDE | Visual Studio 2022 | — |
| Installer | Inno Setup | — |

---

*For change history see [`CHANGELOG.md`](CHANGELOG.md) · For the development story see [`DEVELOPMENT_JOURNEY.md`](DEVELOPMENT_JOURNEY.md)*
