# Petty Cash Management System - Technical Documentation

**Application Version:** 1.2.0  
**Framework:** VB.NET (.NET 8.0)  
**Database:** SQLite (Local/Offline)  
**Last Updated:** March 5, 2026  
**Target Audience:** Developers & System Maintainers

---

## 📋 Table of Contents

1. [System Architecture](#system-architecture)
2. [Technology Stack](#technology-stack)
3. [Project Structure](#project-structure)
4. [Database Schema](#database-schema)
5. [Core Components](#core-components)
6. [Business Rules Implementation](#business-rules-implementation)
7. [API & Integration Points](#api--integration-points)
8. [Development Setup](#development-setup)
9. [Deployment Guide](#deployment-guide)
10. [Code Standards](#code-standards)

---

## System Architecture

### 3-Tier Architecture Overview

```
┌─────────────────────────────────────────────────────┐
│         PRESENTATION LAYER (UI)                     │
│  Windows Forms / WinForms                          │
│  ├─ LoginForm                                      │
│  ├─ MainDashboard                                  │
│  ├─ ExpenseEntryForm                               │
│  ├─ ReportViewForm                                 │
│  └─ SettingsForm                                   │
└─────────────────────────────────────────────────────┘
                        ↓↑
┌─────────────────────────────────────────────────────┐
│       BUSINESS LOGIC LAYER (Services)               │
│  Domain Logic & Workflows                          │
│  ├─ AuthenticationService                          │
│  ├─ ExpenseService                                 │
│  ├─ ValidationService                              │
│  ├─ ReportService                                  │
│  ├─ NotificationService                            │
│  └─ AuditService                                   │
└─────────────────────────────────────────────────────┘
                        ↓↑
┌─────────────────────────────────────────────────────┐
│     DATA ACCESS LAYER (Repositories)                │
│  Database Operations                               │
│  ├─ UserRepository                                 │
│  ├─ ExpenseRepository                              │
│  ├─ AuditLogRepository                             │
│  ├─ CategoryRepository                             │
│  ├─ DbContext                                      │
│  └─ ConnectionManager                              │
└─────────────────────────────────────────────────────┘
                        ↓↑
┌─────────────────────────────────────────────────────┐
│         DATABASE LAYER (SQLite)                     │
│  ├─ Users Table                                    │
│  ├─ Petty Cash Entries Table                       │
│  ├─ Categories Table                               │
│  ├─ Audit Log Table                                │
│  ├─ Settings Table                                 │
│  └─ PettyCash.db file                              │
└─────────────────────────────────────────────────────┘
```

### Design Patterns Used

| Pattern | Usage |
|---------|-------|
| **Repository Pattern** | Data access abstraction |
| **Service Layer Pattern** | Business logic centralization |
| **Dependency Injection** | Loose coupling between layers |
| **Factory Pattern** | Object creation |
| **Observer Pattern** | Notification system |
| **Strategy Pattern** | Multiple validation rules |

---

## Technology Stack

### Required Components

```
DEVELOPMENT ENVIRONMENT
├─ .NET Framework 4.8 (or .NET 6.0+)
├─ Visual Studio 2019 / 2022
├─ C# / VB.NET language support
└─ NuGet Package Manager

RUNTIME
├─ .NET Runtime 4.8 (or 6.0+ on target machines)
├─ Windows 7 SP1 or later
├─ Windows Presentation Foundation (WPF) - if used
└─ GDI+ (included with Windows)

DATABASE
├─ SQLite 3.x
├─ System.Data.SQLite NuGet Package
└─ No external server installation required

REPORTING
├─ RDLC Report Engine
├─ Report Viewer Control
└─ PDF Export Capability
```

### Third-Party Libraries

| Library | Purpose | Version |
|---------|---------|---------|
| **Entity Framework** | ORM for database access | 6.0+ |
| **Dapper** | Micro-ORM for complex queries | 2.0+ |
| **NLog** | Logging framework | 4.7+ |
| **BCrypt.Net** | Password hashing | 0.1.1+ |
| **Newtonsoft.Json** | JSON serialization | 13.0+ |
| **Crystal Reports** | Advanced reporting | 13.0+ (optional) |

---

## Project Structure

### Recommended Folder Organization

```
PettyCash.DesktopApp/
│
├── Forms/                          (UI Layer)
│   ├── Forms/
│   │   ├── LoginForm.vb
│   │   ├── LoginForm.Designer.vb
│   │   ├── DashboardForm.vb
│   │   ├── DashboardForm.Designer.vb
│   │   ├── ExpenseEntryForm.vb
│   │   ├── ExpenseEntryForm.Designer.vb
│   │   ├── ReportViewerForm.vb
│   │   ├── SettingsForm.vb
│   │   └── UserManagementForm.vb
│   │
│   └── UserControls/
│       ├── ValidationErrorPanel.vb
│       ├── ReportPreviewPanel.vb
│       └── CategorySelectorControl.vb
│
├── Services/                       (Business Logic Layer)
│   ├── AuthService.vb
│   │   └── Methods:
│   │       - AuthenticateUser()
│   │       - ChangePassword()
│   │       - ValidateToken()
│   │
│   ├── ExpenseService.vb
│   │   └── Methods:
│   │       - AddExpense()
│   │       - UpdateExpense()
│   │       - DeleteExpense()
│   │       - GetMonthlyTotal()
│   │       - GetCategoryTotal()
│   │
│   ├── ValidationService.vb
│   │   └── Methods:
│   │       - ValidateBusinessRules()
│   │       - CheckMonthlyLimit()
│   │       - CheckBillLimit()
│   │       - CheckDescription()
│   │       - CheckCategory()
│   │
│   ├── ReportService.vb
│   │   └── Methods:
│   │       - GenerateMonthlyReport()
│   │       - GenerateCategoryReport()
│   │       - GenerateHighValueReport()
│   │       - ExportToPDF()
│   │
│   └── AuditService.vb
│       └── Methods:
│           - LogAction()
│           - GetAuditLog()
│           - GetUserActivity()
│
├── Repositories/                   (Data Access Layer)
│   ├── IRepository.vb              (Interface)
│   ├── BaseRepository.vb           (Common implementation)
│   ├── UserRepository.vb
│   ├── ExpenseRepository.vb
│   ├── CategoryRepository.vb
│   ├── AuditLogRepository.vb
│   └── DbContext.vb
│
├── Models/                         (Entities & DTOs)
│   ├── User.vb
│   ├── Expense.vb
│   ├── Category.vb
│   ├── AuditLog.vb
│   ├── ValidationResult.vb
│   ├── MonthlyReportDTO.vb
│   └── ExceptionModel.vb
│
├── Utilities/                      (Helper Classes)
│   ├── ConfigManager.vb
│   ├── Logger.vb
│   ├── EncryptionHelper.vb
│   ├── DateTimeHelper.vb
│   ├── ExceptionHandler.vb
│   ├── Constants.vb
│   └── Enums.vb
│
├── Resources/
│   ├── Images/
│   │   ├── logo.png
│   │   ├── icons/
│   │   │   ├── add.png
│   │   │   ├── edit.png
│   │   │   └─ delete.png
│   │   └── backgrounds/
│   │
│   ├── Localization/
│   │   ├── en-US.resx
│   │   └── si-LK.resx                (Sinhala)
│   │
│   └── Styles/
│       └── ApplicationTheme.xaml
│
├── Configuration/
│   ├── App.config
│   ├── DatabaseConfig.xml
│   └── BusinessRulesConfig.json
│
├── Tests/                          (Unit Tests)
│   ├── ValidationServiceTests.vb
│   ├── ExpenseServiceTests.vb
│   ├── AuthServiceTests.vb
│   └── ReportServiceTests.vb
│
├── Properties/
│   ├── AssemblyInfo.vb
│   ├── Resources.resx
│   └── Settings.settings
│
├── bin/                            (Compiled Output)
│   ├── Debug/
│   │   ├── PettyCash.exe
│   │   ├── PettyCash.pdb
│   │   └── ...dlls
│   │
│   └── Release/
│       └── (Same structure for Release build)
│
├── obj/                            (Build Artifacts)
│   └── (Temporary build files)
│
├── PettyCash.DesktopApp.vbproj     (Project File)
├── App.vb                          (Application Entry Point)
├── Program.vb                      (Main Sub)
└── README.md
```

### Key Folders Explained

| Folder | Purpose | Contains |
|--------|---------|----------|
| **Forms** | User interface | Windows Forms, controls |
| **Services** | Business logic | Validation, calculations, workflows |
| **Repositories** | Data access | Database queries, CRUD ops |
| **Models** | Data structures | Entity classes, DTOs |
| **Utilities** | Helper functions | Config, logging, encryption |
| **Resources** | Static assets | Images, localization, themes |
| **Tests** | Unit tests | Test cases for services |

---

## Database Schema

### Tables Overview

```sql
USERS
├─ user_id (PK)
├─ username (UNIQUE)
├─ password_hash
├─ full_name
├─ role (Enum: Admin, Supervisor, User)
├─ email
├─ is_active
├─ created_at
└─ updated_at

PETTY_CASH_ENTRIES
├─ entry_id (PK)
├─ entry_date
├─ bill_no (UQ with year/month)
├─ description
├─ category_code (FK)
├─ amount
├─ created_by (FK → users)
├─ created_at
├─ updated_at
├─ is_deleted
├─ deleted_at (soft delete)
└─ Indexes: idx_date, idx_deleted

PETTY_CASH_CATEGORIES
├─ category_id (PK)
├─ category_code (UQ)
├─ category_name
└─ created_at

PETTY_CASH_ITEMS
├─ item_id (PK)
├─ description
├─ category_code (FK)
├─ default_amount
└─ created_at

AUDIT_LOG
├─ log_id (PK)
├─ user_id (FK)
├─ action_type
├─ table_name
├─ record_id
├─ details
├─ action_timestamp
└─ Indexes: idx_timestamp, idx_action

SETTINGS
├─ setting_key (PK)
├─ setting_value
├─ setting_type
└─ updated_at
```

### ER Diagram

```
[USERS] 1 ──── M [PETTY_CASH_ENTRIES]
  user_id        created_by

[PETTY_CASH_CATEGORIES] 1 ──── M [PETTY_CASH_ENTRIES]
  category_code          category_code

[PETTY_CASH_CATEGORIES] 1 ──── M [PETTY_CASH_ITEMS]
  category_code          category_code

[USERS] 1 ──── M [AUDIT_LOG]
  user_id        user_id
```

### Core Queries (Examples)

```sql
-- Get monthly total
SELECT SUM(amount) FROM petty_cash_entries
WHERE YEAR(entry_date) = 2026
  AND MONTH(entry_date) = 2
  AND is_deleted = 0;

-- Get category breakdown
SELECT category_code, SUM(amount) as total
FROM petty_cash_entries
WHERE entry_date >= '2026-02-01'
  AND is_deleted = 0
GROUP BY category_code;

-- Get high-value bills
SELECT * FROM petty_cash_entries
WHERE amount > 3000
  AND YEAR(entry_date) = 2026
  ORDER BY amount DESC;
```

---

## Core Components

### AuthService

Handles user authentication and session management.

```vb
Public Class AuthService
    Private _userRepository As UserRepository
    
    Public Function AuthenticateUser(username As String, password As String) As AuthResult
        ' 1. Retrieve user by username
        ' 2. Hash provided password
        ' 3. Compare with stored hash
        ' 4. Generate session token
        ' 5. Log login attempt
        ' 6. Return result (success/fail)
    End Function
    
    Public Sub ChangePassword(userId As Integer, newPassword As String)
        ' 1. Validate password strength
        ' 2. Hash new password
        ' 3. Update in database
        ' 4. Log password change
    End Sub
    
    Public Function ValidateToken(token As String) As Boolean
        ' Check if token is valid and not expired
    End Function
End Class
```

### ExpenseService

Manages petty cash entry operations.

```vb
Public Class ExpenseService
    Public Function AddExpense(expense As Expense) As OperationResult
        ' 1. Validate business rules
        ' 2. Check monthly limit
        ' 3. Check bill limit
        ' 4. Check for duplicate bill number
        ' 5. Save to database
        ' 6. Trigger notifications if needed
        ' 7. Return result
    End Function
    
    Public Function UpdateExpense(entry_id As Integer, updatedExpense As Expense) As OperationResult
        ' 1. Fetch original expense
        ' 2. Re-validate business rules
        ' 3. Update database
        ' 4. Log audit
        ' 5. Return result
    End Function
    
    Public Function GetMonthlyTotal(year As Integer, month As Integer) As Decimal
        ' Calculate sum of all entries for given month
    End Function
End Class
```

### ValidationService

Enforces all 8 business rules.

```vb
Public Class ValidationService
    
    Public Function ValidateExpense(expense As Expense) As ValidationResult
        ' Run all business rule checks
        ' Return: ValidationResult with errors/warnings
    End Function
    
    Private Function CheckMonthlyLimit(amount As Decimal, year As Integer, month As Integer) As Boolean
        ' BR1: Monthly limit (LKR 25,000)
    End Function
    
    Private Function CheckBillLimit(amount As Decimal) As Boolean
        ' BR2: Single bill limit (LKR 5,000)
    End Function
    
    Private Function CheckPositiveAmount(amount As Decimal) As Boolean
        ' BR3: Amount > 0
    End Function
    
    ' ... more validation methods for BR4-BR8
    
End Class
```

### ReportService

Generates reports for viewing and exporting.

```vb
Public Class ReportService
    
    Public Function GenerateMonthlyReport(year As Integer, month As Integer) As MonthlyReportDTO
        ' 1. Query all entries for month
        ' 2. Calculate totals and breakdowns
        ' 3. Identify high-value bills
        ' 4. Build report object
        ' 5. Return DTO
    End Function
    
    Public Sub ExportToPDF(report As MonthlyReportDTO, filePath As String)
        ' 1. Format report for PDF
        ' 2. Generate PDF using library
        ' 3. Save to file
    End Sub
    
End Class
```

---

## Business Rules Implementation

### Rule Evaluation Flow

```
User submits Expense
        ↓
ValidationService.ValidateExpense()
        ├─ BR1: Check Monthly Limit
        │   └─ Query SUM(amount) for month
        │   └─ If total + new > 25000 → ERROR
        │
        ├─ BR2: Check Single Bill Limit
        │   └─ If amount > 5000 → ERROR
        │
        ├─ BR3: Check Positive Amount
        │   └─ If amount <= 0 → ERROR
        │
        ├─ BR4: Check High-Value Warning
        │   └─ If amount > 3000 → WARNING
        │
        ├─ BR5: Check Category Valid
        │   └─ Must be: E5200, E5300, or E7800 → ERROR
        │
        ├─ BR6: Check Unique Bill
        │   └─ Query for duplicate in same month
        │   └─ If exists → ERROR
        │
        ├─ BR7: Check Date Valid
        │   └─ Cannot be future date → ERROR
        │
        └─ BR8: Check Description Length
            └─ Must be >= 10 chars → ERROR

Return ValidationResult
├─ IsValid: true/false
├─ Errors: List of blocking errors
└─ Warnings: List of soft warnings

If IsValid = true
        ↓
ExpenseService.AddExpense()
        ↓
ExpenseRepository.Save()
        ↓
✓ Entry saved in DB
```

### Rule Configuration

```json
{
  "businessRules": {
    "BR1": {
      "enabled": true,
      "ruleType": "hardLimit",
      "value": 25000,
      "description": "Monthly limit",
      "lastModified": "2026-02-01"
    },
    "BR2": {
      "enabled": true,
      "ruleType": "hardLimit",
      "value": 5000,
      "description": "Single bill limit"
    },
    "BR4": {
      "enabled": true,
      "ruleType": "softWarning",
      "value": 3000,
      "description": "High-value warning threshold"
    }
  }
}
```

---

## API & Integration Points

### Service Methods (Public Interface)

```vb
' AUTHENTICATION
AuthService.AuthenticateUser(username, password) → AuthResult
AuthService.ChangePassword(userId, newPassword) → OperationResult

' EXPENSE MANAGEMENT
ExpenseService.AddExpense(expense) → OperationResult
ExpenseService.UpdateExpense(entryId, expense) → OperationResult
ExpenseService.DeleteExpense(entryId) → OperationResult
ExpenseService.GetExpense(entryId) → Expense
ExpenseService.GetMonthlyExpenses(year, month) → List(Of Expense)
ExpenseService.GetMonthlyTotal(year, month) → Decimal

' VALIDATION
ValidationService.ValidateExpense(expense) → ValidationResult
ValidationService.CheckBusinessRules(expense) → List(Of ValidationError)

' REPORTING
ReportService.GenerateMonthlyReport(year, month) → MonthlyReportDTO
ReportService.GenerateCategoryReport(year, month) → CategoryReportDTO
ReportService.ExportToPDF(report, filePath) → Boolean

' AUDIT
AuditService.LogAction(action, userId, details) → Void
AuditService.GetAuditLog(filters) → List(Of AuditEntry)

' CONFIGURATION
ConfigManager.GetSetting(key) → String
ConfigManager.SetSetting(key, value) → Void
ConfigManager.GetBusinessRule(ruleId) → BusinessRule
```

---

## Development Setup

### Prerequisites

1. **Install Visual Studio 2022**
   - Include .NET Desktop Development workload
   - Include VB.NET language support

2. **Install SQL Server Express 2019+**
   - Download from Microsoft
   - Install with Windows Authentication or Mixed Auth
   - Enable TCP/IP protocol

3. **Install .NET Runtime**
   - .NET Framework 4.8 or .NET 6.0+
   - Download from microsoft.com/net

### Project Setup

```bash
# Clone repository
git clone https://github.com/yourepo/petty-cash.git
cd PettyCash.DesktopApp

# Open solution
start PettyCash.sln  (or double-click)

# Restore NuGet packages
# (Visual Studio does this automatically)

# Update database
# Run SQL migration scripts in sql/ folder
```

### Database Setup

```sql
-- ConnectionString in App.config
<add name="DefaultConnection" 
     connectionString="Server=.\SQLEXPRESS;Database=PettyCash;
     Integrated Security=true;" />

-- Create database
CREATE DATABASE PettyCash;

-- Run schema script
-- File: sql/schema.sql
```

### Build & Run

```
Visual Studio:
1. Build → Build Solution (Ctrl+Shift+B)
2. Debug → Start Debugging (F5)
3. Login with admin/password

Command Line:
dotnet build
dotnet run
```

---

## Deployment Guide

### Build Release Version

```bash
# Clean previous build
dotnet clean --configuration Release

# Build release
dotnet build --configuration Release

# Output: bin/Release/PettyCash.exe
```

### Create Installer

Using WiX Toolset or similar:
1. Package executable
2. Include database schema
3. Configure installation wizard
4. Set startup shortcut
5. Create uninstaller

### Deployment Steps

1. **Server Setup**
   - Install SQL Server Express
   - Create database
   - Run schema.sql

2. **Client Deployment**
   - Run installer
   - Verify database connection
   - Create user accounts
   - Test application

3. **Verification**
   - Test login
   - Add test entry
   - Verify database saved entry
   - Print test report

---

## Code Standards

### Naming Conventions

```vb
' Classes: PascalCase
Public Class ExpenseService

' Methods: PascalCase
Public Function GetMonthlyTotal()

' Variables: camelCase
Dim monthlyTotal As Decimal

' Constants: UPPER_SNAKE_CASE
Const MAX_MONTHLY_LIMIT As Decimal = 25000

' Private fields: _camelCase
Private _userRepository As UserRepository
```

### Code Organization

```vb
Public Class ExpenseService
    ' 1. Constants
    Private Const MAX_BILL_LIMIT As Decimal = 5000
    
    ' 2. Private Fields
    Private _expenseRepository As ExpenseRepository
    
    ' 3. Constructor
    Public Sub New(repository As ExpenseRepository)
        _expenseRepository = repository
    End Sub
    
    ' 4. Public Methods
    Public Function AddExpense(expense As Expense) As OperationResult
        ' Implementation
    End Function
    
    ' 5. Private/Helper Methods
    Private Function ValidateExpense(expense As Expense) As Boolean
        ' Implementation
    End Function
End Class
```

### Error Handling

```vb
Try
    Dim result = _expenseService.AddExpense(expense)
    If Not result.IsSuccess Then
        MessageBox.Show(result.ErrorMessage)
    End If
Catch ex As DatabaseException
    Logger.Error("Database error", ex)
    MessageBox.Show("Database connection failed")
Catch ex As Exception
    Logger.Error("Unexpected error", ex)
    MessageBox.Show("An unexpected error occurred")
End Try
```

### Documentation Comments

```vb
''' <summary>
''' Adds a new petty cash expense to the system.
''' </summary>
''' <param name="expense">The expense to add</param>
''' <returns>OperationResult indicating success/failure</returns>
''' <remarks>
''' Validates against all business rules before saving.
''' Throws ValidationException if rules violated.
''' </remarks>
Public Function AddExpense(expense As Expense) As OperationResult
    ' Implementation
End Function
```

---

**End of Technical Documentation**
