-- ============================================================================
-- SetupDatabase.sql - Tables & Seed Data Only
-- Petty Cash Management System
-- ============================================================================
-- NOTE: This script is executed by the application via ADO.NET.
-- Database creation and SQL login are handled separately in Program.vb.
-- Do NOT include USE statements or CREATE DATABASE here.
-- ============================================================================

SET QUOTED_IDENTIFIER ON;
SET ANSI_NULLS ON;

-- ============================================================================
-- Tables
-- ============================================================================

-- Users
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[users]') AND type in (N'U'))
BEGIN
    CREATE TABLE users (
        user_id INT IDENTITY(1,1) PRIMARY KEY,
        username NVARCHAR(50) NOT NULL UNIQUE,
        password_hash NVARCHAR(255) NOT NULL,
        full_name NVARCHAR(100) NOT NULL,
        role NVARCHAR(20) NOT NULL CHECK (role IN ('Admin', 'Clerk', 'Viewer')),
        email NVARCHAR(100) NULL,
        whatsapp_no NVARCHAR(20) NULL,
        created_at DATETIME NOT NULL DEFAULT GETDATE(),
        is_active BIT NOT NULL DEFAULT 1
    );
    CREATE INDEX idx_users_username ON users(username);
END
GO

-- Categories
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[petty_cash_categories]') AND type in (N'U'))
BEGIN
    CREATE TABLE petty_cash_categories (
        category_id INT IDENTITY(1,1) PRIMARY KEY,
        category_code NVARCHAR(10) NOT NULL UNIQUE,
        category_name NVARCHAR(100) NOT NULL,
        created_at DATETIME NOT NULL DEFAULT GETDATE()
    );
END
GO

-- Items
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[petty_cash_items]') AND type in (N'U'))
BEGIN
    CREATE TABLE petty_cash_items (
        item_id INT IDENTITY(1,1) PRIMARY KEY,
        description NVARCHAR(500) NOT NULL,
        category_code NVARCHAR(10) NOT NULL,
        default_amount DECIMAL(18,2) NULL,
        created_at DATETIME NOT NULL DEFAULT GETDATE(),
        FOREIGN KEY (category_code) REFERENCES petty_cash_categories(category_code)
    );
END
GO

-- Entries
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[petty_cash_entries]') AND type in (N'U'))
BEGIN
    CREATE TABLE petty_cash_entries (
        entry_id INT IDENTITY(1,1) PRIMARY KEY,
        entry_date DATE NOT NULL,
        bill_no NVARCHAR(50) NOT NULL,
        category_code NVARCHAR(10) NOT NULL,
        description NVARCHAR(500) NOT NULL,
        amount DECIMAL(18,2) NOT NULL CHECK (amount > 0 AND amount <= 100000),
        created_by INT NOT NULL,
        created_at DATETIME NOT NULL DEFAULT GETDATE(),
        updated_at DATETIME NOT NULL DEFAULT GETDATE(),
        is_deleted BIT NOT NULL DEFAULT 0,
        deleted_at DATETIME NULL,
        FOREIGN KEY (category_code) REFERENCES petty_cash_categories(category_code),
        FOREIGN KEY (created_by) REFERENCES users(user_id)
    );
    CREATE INDEX idx_entries_date ON petty_cash_entries(entry_date);
    CREATE INDEX idx_entries_deleted ON petty_cash_entries(is_deleted);
END
GO

-- Permissions
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[permissions]') AND type in (N'U'))
BEGIN
    CREATE TABLE permissions (
        permission_id   INT IDENTITY(1,1) PRIMARY KEY,
        permission_key  NVARCHAR(50) NOT NULL UNIQUE,
        display_name    NVARCHAR(100) NOT NULL,
        description     NVARCHAR(255) NULL,
        category        NVARCHAR(50) NOT NULL
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[role_default_permissions]') AND type in (N'U'))
BEGIN
    CREATE TABLE role_default_permissions (
        role            NVARCHAR(20) NOT NULL,
        permission_key  NVARCHAR(50) NOT NULL,
        PRIMARY KEY (role, permission_key),
        FOREIGN KEY (permission_key) REFERENCES permissions(permission_key)
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[user_permissions]') AND type in (N'U'))
BEGIN
    CREATE TABLE user_permissions (
        user_id         INT NOT NULL,
        permission_key  NVARCHAR(50) NOT NULL,
        is_granted      BIT NOT NULL DEFAULT 1,
        granted_by      INT NOT NULL,
        granted_at      DATETIME NOT NULL DEFAULT GETDATE(),
        PRIMARY KEY (user_id, permission_key),
        FOREIGN KEY (user_id) REFERENCES users(user_id),
        FOREIGN KEY (permission_key) REFERENCES permissions(permission_key),
        FOREIGN KEY (granted_by) REFERENCES users(user_id)
    );
END
GO

-- Audit Log
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[audit_log]') AND type in (N'U'))
BEGIN
    CREATE TABLE audit_log (
        audit_id INT IDENTITY(1,1) PRIMARY KEY,
        user_id INT NOT NULL,
        action_type NVARCHAR(50) NOT NULL,
        table_name NVARCHAR(50) NULL,
        record_id INT NULL,
        details NVARCHAR(MAX) NULL,
        action_timestamp DATETIME NOT NULL DEFAULT GETDATE(),
        FOREIGN KEY (user_id) REFERENCES users(user_id)
    );
    CREATE INDEX idx_audit_timestamp ON audit_log(action_timestamp);
END
GO

-- ============================================================================
-- Views & Stored Procedures
-- ============================================================================

IF EXISTS (SELECT * FROM sys.views WHERE name = 'vw_user_effective_permissions')
    DROP VIEW vw_user_effective_permissions;
GO

CREATE VIEW vw_user_effective_permissions AS
SELECT 
    u.user_id, u.username, u.full_name, u.role,
    p.permission_key, p.display_name, p.category,
    CASE 
        WHEN up.is_granted IS NOT NULL THEN up.is_granted
        WHEN rdp.permission_key IS NOT NULL THEN 1
        ELSE 0
    END AS has_permission,
    CASE 
        WHEN up.is_granted IS NOT NULL THEN 'User Override'
        WHEN rdp.permission_key IS NOT NULL THEN 'Role Default'
        ELSE 'Not Granted'
    END AS permission_source
FROM users u
CROSS JOIN permissions p
LEFT JOIN user_permissions up ON u.user_id = up.user_id AND p.permission_key = up.permission_key
LEFT JOIN role_default_permissions rdp ON u.role = rdp.role AND p.permission_key = rdp.permission_key
WHERE u.is_active = 1;
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_GetUserPermissions')
    DROP PROCEDURE sp_GetUserPermissions;
GO

CREATE PROCEDURE sp_GetUserPermissions @UserId INT AS
BEGIN
    SELECT DISTINCT p.permission_key
    FROM permissions p
    LEFT JOIN user_permissions up ON p.permission_key = up.permission_key AND up.user_id = @UserId
    LEFT JOIN users u ON u.user_id = @UserId
    LEFT JOIN role_default_permissions rdp ON rdp.role = u.role AND rdp.permission_key = p.permission_key
    WHERE (up.is_granted = 1) OR (up.permission_key IS NULL AND rdp.permission_key IS NOT NULL)
END;
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_HasPermission')
    DROP PROCEDURE sp_HasPermission;
GO

CREATE PROCEDURE sp_HasPermission @UserId INT, @PermissionKey NVARCHAR(50) AS
BEGIN
    DECLARE @HasPermission BIT = 0;
    SELECT @HasPermission = 1 FROM permissions p
    LEFT JOIN user_permissions up ON p.permission_key = up.permission_key AND up.user_id = @UserId
    LEFT JOIN users u ON u.user_id = @UserId
    LEFT JOIN role_default_permissions rdp ON rdp.role = u.role AND rdp.permission_key = p.permission_key
    WHERE p.permission_key = @PermissionKey AND ((up.is_granted = 1) OR (up.permission_key IS NULL AND rdp.permission_key IS NOT NULL));
    SELECT ISNULL(@HasPermission, 0) AS HasPermission;
END;
GO

-- ============================================================================
-- Seed Data
-- ============================================================================

IF NOT EXISTS (SELECT * FROM petty_cash_categories)
BEGIN
    INSERT INTO petty_cash_categories (category_code, category_name) VALUES
    ('E5200', 'Vehicle Parts'),
    ('E5300', 'Office Items'),
    ('E7800', 'Physical Hardware'),
    ('E7510', 'Treatments & Staff');
END
GO

IF NOT EXISTS (SELECT * FROM permissions)
BEGIN
    INSERT INTO permissions (permission_key, display_name, description, category) VALUES
    ('EXPENSE_ADD', 'Add Expense', 'Create new expense entries', 'Expense Management'),
    ('EXPENSE_EDIT', 'Edit Expense', 'Modify existing expense entries', 'Expense Management'),
    ('EXPENSE_DELETE', 'Delete Expense', 'Soft-delete expense entries', 'Expense Management'),
    ('EXPENSE_VIEW', 'View Expenses', 'View expense list on dashboard', 'Expense Management'),
    ('REPORT_GENERATE', 'Generate Report', 'Generate monthly reports', 'Reports'),
    ('REPORT_PRINT', 'Print Report', 'Print generated reports', 'Reports'),
    ('REPORT_FINALIZE', 'Finalize Report', 'Finalize monthly reports', 'Reports'),
    ('DASHBOARD_VIEW', 'View Dashboard', 'View dashboard', 'Dashboard'),
    ('DASHBOARD_NAVIGATE', 'Navigate Months', 'Navigate between months', 'Dashboard'),
    ('USER_CREATE', 'Create User', 'Register new users', 'User Management'),
    ('USER_EDIT', 'Edit User', 'Edit user details and role', 'User Management'),
    ('USER_DEACTIVATE', 'Deactivate User', 'Deactivate user accounts', 'User Management'),
    ('USER_RESET_PASSWORD', 'Reset Password', 'Reset another user password', 'User Management'),
    ('USER_MANAGE_PERMISSIONS', 'Manage Permissions', 'Assign/revoke permissions', 'User Management'),
    ('SETTINGS_VIEW', 'View Settings', 'View settings', 'Settings'),
    ('SETTINGS_EDIT', 'Edit Settings', 'Edit settings', 'Settings'),
    ('AUDIT_VIEW', 'View Audit Log', 'View system audit log', 'Audit'),
    ('CATEGORY_MANAGE', 'Manage Categories', 'Add/edit/delete categories', 'Category & Item'),
    ('ITEM_MANAGE', 'Manage Items', 'Add/edit/delete items', 'Category & Item'),
    ('SELF_CHANGE_PASSWORD', 'Change Own Password', 'Change own password', 'Self-Service');

    -- Admin defaults (all permissions)
    INSERT INTO role_default_permissions (role, permission_key)
    SELECT 'Admin', permission_key FROM permissions;

    -- Clerk defaults
    INSERT INTO role_default_permissions (role, permission_key) VALUES
    ('Clerk', 'EXPENSE_ADD'), ('Clerk', 'EXPENSE_EDIT'), ('Clerk', 'EXPENSE_VIEW'),
    ('Clerk', 'REPORT_GENERATE'), ('Clerk', 'REPORT_PRINT'),
    ('Clerk', 'DASHBOARD_VIEW'), ('Clerk', 'DASHBOARD_NAVIGATE'),
    ('Clerk', 'ITEM_MANAGE'), ('Clerk', 'SELF_CHANGE_PASSWORD');
END
GO
