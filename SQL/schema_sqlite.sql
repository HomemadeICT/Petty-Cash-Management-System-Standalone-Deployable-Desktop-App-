-- ============================================================================
-- schema_sqlite.sql - Database Schema and Seed Data (SQLite Edition)
-- Petty Cash Management System — CEB Haliela
-- ============================================================================

-- 1. Users Table
CREATE TABLE IF NOT EXISTS users (
    user_id INTEGER PRIMARY KEY AUTOINCREMENT,
    username TEXT NOT NULL UNIQUE,
    password_hash TEXT NOT NULL,
    full_name TEXT NOT NULL,
    role TEXT NOT NULL DEFAULT 'Viewer',
    email TEXT,
    whatsapp_no TEXT,
    created_at TEXT DEFAULT CURRENT_TIMESTAMP,
    is_active INTEGER DEFAULT 1 -- 1 = Active, 0 = Inactive
);

-- 2. Expense Categories Table
CREATE TABLE IF NOT EXISTS petty_cash_categories (
    category_id INTEGER PRIMARY KEY AUTOINCREMENT,
    category_code TEXT NOT NULL UNIQUE,
    category_name TEXT NOT NULL,
    created_at TEXT DEFAULT CURRENT_TIMESTAMP
);

-- 3. Monthly Limits Table
CREATE TABLE IF NOT EXISTS monthly_limits (
    limit_id INTEGER PRIMARY KEY AUTOINCREMENT,
    month INTEGER NOT NULL,
    year INTEGER NOT NULL,
    monthly_limit DECIMAL(10,2) NOT NULL DEFAULT 25000.00,
    single_bill_limit DECIMAL(10,2) NOT NULL DEFAULT 5000.00,
    is_finalized INTEGER DEFAULT 0,
    updated_at TEXT DEFAULT CURRENT_TIMESTAMP,
    UNIQUE(month, year)
);

-- 4. Petty Cash Entries Table
CREATE TABLE IF NOT EXISTS petty_cash_entries (
    entry_id INTEGER PRIMARY KEY AUTOINCREMENT,
    entry_date TEXT NOT NULL,
    bill_no TEXT NOT NULL,
    category_code TEXT NOT NULL,
    description TEXT NOT NULL,
    amount DECIMAL(10,2) NOT NULL,
    report_month INTEGER,
    report_year INTEGER,
    created_by INTEGER NOT NULL,
    created_at TEXT DEFAULT CURRENT_TIMESTAMP,
    updated_at TEXT DEFAULT CURRENT_TIMESTAMP,
    is_deleted INTEGER DEFAULT 0,
    deleted_at TEXT,
    FOREIGN KEY (created_by) REFERENCES users(user_id),
    FOREIGN KEY (category_code) REFERENCES petty_cash_categories(category_code)
);

-- 5. Audit Log Table
CREATE TABLE IF NOT EXISTS audit_log (
    audit_id INTEGER PRIMARY KEY AUTOINCREMENT,
    user_id INTEGER NOT NULL,
    action_type TEXT NOT NULL,
    table_name TEXT,
    record_id INTEGER,
    details TEXT,
    action_timestamp TEXT DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (user_id) REFERENCES users(user_id)
);

-- 6. Permissions Table
CREATE TABLE IF NOT EXISTS permissions (
    permission_id INTEGER PRIMARY KEY AUTOINCREMENT,
    permission_key TEXT NOT NULL UNIQUE,
    description TEXT
);

-- 7. Role Default Permissions Table
CREATE TABLE IF NOT EXISTS role_default_permissions (
    role_permission_id INTEGER PRIMARY KEY AUTOINCREMENT,
    role_name TEXT NOT NULL,
    permission_id INTEGER NOT NULL,
    is_granted INTEGER DEFAULT 1,
    FOREIGN KEY (permission_id) REFERENCES permissions(permission_id),
    UNIQUE(role_name, permission_id)
);

-- 8. User Specific Permissions Table (Overrides)
CREATE TABLE IF NOT EXISTS user_permissions (
    user_permission_id INTEGER PRIMARY KEY AUTOINCREMENT,
    user_id INTEGER NOT NULL,
    permission_id INTEGER NOT NULL,
    is_granted INTEGER NOT NULL,
    FOREIGN KEY (user_id) REFERENCES users(user_id),
    FOREIGN KEY (permission_id) REFERENCES permissions(permission_id),
    UNIQUE(user_id, permission_id)
);

-- ============================================================================
-- SEED DATA
-- ============================================================================

-- Initial Categories
INSERT OR IGNORE INTO petty_cash_categories (category_code, category_name) VALUES 
('E5200', 'Vehicle Parts'),
('E5300', 'Office Items'),
('E7800', 'Physical Hardware'),
('E7510', 'Treatments & Staff');

-- Initial Permissions (keys must match PermissionKeys.vb constants)
INSERT OR IGNORE INTO permissions (permission_key, description) VALUES 
('EXPENSE_ADD',              'Can add new petty cash entries'),
('EXPENSE_EDIT',             'Can edit existing entries'),
('EXPENSE_DELETE',           'Can remove entries'),
('EXPENSE_VIEW',             'Can view expense entries'),
('REPORT_GENERATE',          'Can generate and view reports'),
('REPORT_PRINT',             'Can print reports'),
('REPORT_FINALIZE',          'Can lock monthly reports'),
('REPORT_EXPORT_EXCEL',      'Can export reports to Excel'),
('DASHBOARD_VIEW',           'Can access the dashboard'),
('DASHBOARD_NAVIGATE',       'Can navigate between months'),
('USER_CREATE',              'Can create new users'),
('USER_EDIT',                'Can edit user details'),
('USER_DEACTIVATE',          'Can deactivate users'),
('USER_RESET_PASSWORD',      'Can reset user passwords'),
('USER_MANAGE_PERMISSIONS',  'Can manage user permissions'),
('SETTINGS_VIEW',            'Can view admin settings'),
('SETTINGS_EDIT',            'Can change system settings'),
('AUDIT_VIEW',               'Can view the audit log'),
('CATEGORY_MANAGE',          'Can manage categories'),
('ITEM_MANAGE',              'Can manage items library'),
('SELF_CHANGE_PASSWORD',     'Can change own password'),
('BACKUP_DATABASE',          'Can backup and restore the database');

-- Role Defaults
-- Admin: All Permissions
INSERT OR IGNORE INTO role_default_permissions (role_name, permission_id, is_granted)
SELECT 'Admin', permission_id, 1 FROM permissions;

-- Clerk: Basic Operations
INSERT OR IGNORE INTO role_default_permissions (role_name, permission_id, is_granted)
SELECT 'Clerk', permission_id, 1 FROM permissions 
WHERE permission_key IN ('EXPENSE_ADD', 'EXPENSE_EDIT', 'EXPENSE_VIEW', 'REPORT_GENERATE', 'REPORT_PRINT', 'REPORT_EXPORT_EXCEL', 'DASHBOARD_VIEW', 'DASHBOARD_NAVIGATE', 'SELF_CHANGE_PASSWORD');

-- Viewer: Read Only
INSERT OR IGNORE INTO role_default_permissions (role_name, permission_id, is_granted)
SELECT 'Viewer', permission_id, 1 FROM permissions 
WHERE permission_key IN ('EXPENSE_VIEW', 'REPORT_GENERATE', 'REPORT_PRINT', 'DASHBOARD_VIEW', 'DASHBOARD_NAVIGATE', 'SELF_CHANGE_PASSWORD');

-- Default Admin User (admin / admin123)
-- Note: Password is reset to admin123 on startup via Program.vb
INSERT OR IGNORE INTO users (username, password_hash, full_name, role, is_active)
VALUES ('admin', '$2a$11$placeholder_will_be_reset_on_startup', 'System Administrator', 'Admin', 1);

-- ============================================================================
-- END OF SCHEMA
-- ============================================================================
