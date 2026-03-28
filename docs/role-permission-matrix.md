# Role Permission Matrix

**Petty Cash Management System**  
**Version:** 2.0 (Permission System)  
**Last Updated:** February 10, 2026

---

## Overview

This document is the **single source of truth** for all role-permission mappings in the Petty Cash Management System. It defines what each user role can do and which UI elements they can access.

---

## User Roles

| Role | Purpose | Count |
|------|---------|-------|
| **Admin** | Full system access - manages users, settings, and all data operations | 1-2 users |
| **Clerk** | Data entry and reporting - full expense management and report generation | 1+ users |
| **Viewer** | Read-only access - view expenses, reports, and audit logs | 0+ users |

---

## Permission Categories

The system has **20 permissions** across **7 categories**:

1. **Expense Management** (4 permissions)
2. **Reports** (3 permissions)
3. **Dashboard** (2 permissions)
4. **User Management** (5 permissions)
5. **Settings** (2 permissions)
6. **Audit** (1 permission)
7. **Category & Item** (2 permissions)
8. **Self-Service** (1 permission)

---

## Default Permission Matrix

| Permission Key | Display Name | Admin | Clerk | Viewer |
|----------------|--------------|:-----:|:-----:|:------:|
| **Expense Management** |
| `EXPENSE_ADD` | Add Expense | ✓ | ✓ | ✗ |
| `EXPENSE_EDIT` | Edit Expense | ✓ | ✓ | ✗ |
| `EXPENSE_DELETE` | Delete Expense | ✓ | ✓ | ✗ |
| `EXPENSE_VIEW` | View Expenses | ✓ | ✓ | ✓ |
| **Reports** |
| `REPORT_GENERATE` | Generate Report | ✓ | ✓ | ✓ |
| `REPORT_PRINT` | Print Report | ✓ | ✓ | ✓ |
| `REPORT_FINALIZE` | Finalize Report | ✓ | ✓ | ✗ |
| **Dashboard** |
| `DASHBOARD_VIEW` | View Dashboard | ✓ | ✓ | ✓ |
| `DASHBOARD_NAVIGATE` | Navigate Months | ✓ | ✓ | ✓ |
| **User Management** |
| `USER_CREATE` | Create User | ✓ | ✗ | ✗ |
| `USER_EDIT` | Edit User | ✓ | ✗ | ✗ |
| `USER_DEACTIVATE` | Deactivate User | ✓ | ✗ | ✗ |
| `USER_RESET_PASSWORD` | Reset Password | ✓ | ✗ | ✗ |
| `USER_MANAGE_PERMISSIONS` | Manage Permissions | ✓ | ✗ | ✗ |
| **Settings** |
| `SETTINGS_VIEW` | View Settings | ✓ | ✗ | ✗ |
| `SETTINGS_EDIT` | Edit Settings | ✓ | ✗ | ✗ |
| **Audit** |
| `AUDIT_VIEW` | View Audit Log | ✓ | ✗ | ✓ |
| **Category & Item** |
| `CATEGORY_MANAGE` | Manage Categories | ✓ | ✓ | ✗ |
| `ITEM_MANAGE` | Manage Items | ✓ | ✓ | ✗ |
| **Self-Service** |
| `SELF_CHANGE_PASSWORD` | Change Own Password | ✓ | ✓ | ✓ |

**Summary:**
- **Admin:** All 20 permissions
- **Clerk:** 12 permissions (full data entry + reporting)
- **Viewer:** 7 permissions (read-only)

---

## Form Access by Role

| Form | Admin | Clerk | Viewer | Notes |
|------|:-----:|:-----:|:------:|-------|
| `LoginForm` | ✓ | ✓ | ✓ | All users must login |
| `DashboardForm` | ✓ | ✓ | ✓ | Main hub - buttons gated by permissions |
| `ExpenseEntryForm` | ✓ | ✓ | ✗ | Add/Edit expenses |
| `ReportViewerForm` | ✓ | ✓ | ✓ | View/print reports |
| `AdminSettingsForm` | ✓ | ✗ | ✗ | Notification settings |
| `UserManagementForm` | ✓ | ✗ | ✗ | Manage users |
| `UserEditForm` | ✓ | ✗ | ✗ | Add/Edit user details |
| `UserPermissionForm` | ✓ | ✗ | ✗ | Manage per-user permissions |

---

## Dashboard Button Visibility

The `DashboardForm` dynamically shows/hides buttons based on permissions:

| Button | Permission Required | Admin | Clerk | Viewer |
|--------|---------------------|:-----:|:-----:|:------:|
| `btnAddEntry` | `EXPENSE_ADD` | ✓ | ✓ | ✗ |
| `btnEditEntry` | `EXPENSE_EDIT` | ✓ | ✓ | ✗ |
| `btnDeleteEntry` | `EXPENSE_DELETE` | ✓ | ✓ | ✗ |
| `btnGenerateReport` | `REPORT_GENERATE` | ✓ | ✓ | ✓ |
| `btnPrevMonth` | `DASHBOARD_NAVIGATE` | ✓ | ✓ | ✓ |
| `btnNextMonth` | `DASHBOARD_NAVIGATE` | ✓ | ✓ | ✓ |
| `btnSettings` | `SETTINGS_VIEW` | ✓ | ✗ | ✗ |
| `btnAuditLog` | `AUDIT_VIEW` | ✓ | ✗ | ✓ |
| `btnManageUsers` | `USER_CREATE` OR `USER_EDIT` | ✓ | ✗ | ✗ |

---

## User Workflows by Role

### Admin Workflow
```
Login → Dashboard → Full Access to All Features
├─ Add/Edit/Delete Expenses
├─ Generate/Print/Finalize Reports
├─ Manage Users (Create, Edit, Deactivate, Reset Password)
├─ Manage Permissions (Grant/Revoke per user)
├─ View/Edit Settings (Notifications)
└─ View Audit Log
```

### Clerk Workflow
```
Login → Dashboard → Data Entry & Reporting
├─ Add/Edit/Delete Expenses
├─ Generate/Print/Finalize Reports
├─ Manage Item Library
├─ Navigate Historical Data
└─ Change Own Password
```

### Viewer Workflow
```
Login → Dashboard → Read-Only Access
├─ View Expenses (current/historical)
├─ Generate/Print Reports
├─ View Audit Log
├─ Navigate Historical Data
└─ Change Own Password
```

---

## Permission Override System

Admins can grant or revoke individual permissions for specific users via the `UserPermissionForm`. This creates a **user permission override** that takes precedence over role defaults.

**Example:**
- A Clerk user can be granted `AUDIT_VIEW` permission (normally only Admin/Viewer)
- A Viewer user can be granted `EXPENSE_ADD` permission (normally only Admin/Clerk)

**Effective Permission Logic:**
```
IF user has explicit permission override THEN
    use override value (granted/revoked)
ELSE
    use role default permission
END IF
```

---

## Database Implementation

### Tables
- `permissions` - Master list of all 20 permissions
- `role_default_permissions` - Default permissions per role
- `user_permissions` - Per-user overrides

### Stored Procedures
- `sp_GetUserPermissions(@UserId)` - Returns effective permission keys for a user
- `sp_HasPermission(@UserId, @PermissionKey)` - Checks if user has a specific permission

### View
- `vw_user_effective_permissions` - Shows all users with their effective permissions and sources

---

## Code References

| Component | File | Purpose |
|-----------|------|---------|
| Permission Keys | `PermissionKeys.vb` | Constants for all permission keys |
| Permission Service | `PermissionService.vb` | Permission checking and management |
| Permission Repository | `PermissionRepository.vb` | Database access for permissions |
| Session Manager | `SessionManager.vb` | `HasPermission()` method for UI checks |
| Migration Script | `migration_permissions.sql` | Database schema and seed data |

---

## Business Rules

1. **Admin Protection:** Cannot deactivate the last active Admin user
2. **Self-Service:** All users can change their own password
3. **Permission Inheritance:** Users inherit role defaults unless overridden
4. **Cache Invalidation:** Permission cache is cleared when:
   - User role changes
   - User permissions are modified
   - User is deactivated/reactivated

---

## Testing Checklist

- [ ] Admin can access all forms and buttons
- [ ] Clerk can add/edit/delete expenses and generate/finalize reports
- [ ] Clerk cannot access user management or settings
- [ ] Viewer can only view data and generate reports
- [ ] Viewer cannot modify any data
- [ ] Permission overrides work correctly
- [ ] Dashboard buttons show/hide based on permissions
- [ ] Forms validate permissions on load
