
-- ============================================================================
-- schema_mysql.sql - Database Schema for Petty Cash Management System
-- Version: 2.0 (MySQL)
-- Database: MySQL 8.0+
-- ============================================================================
-- How to run:
--   mysql -u root -p < schema_mysql.sql
--   OR open this file in MySQL Workbench and execute.
-- ============================================================================

CREATE DATABASE IF NOT EXISTS PettyCashDB
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_unicode_ci;

USE PettyCashDB;

-- ============================================================================
-- TABLE: users
-- ============================================================================
CREATE TABLE IF NOT EXISTS users (
    user_id      INT AUTO_INCREMENT PRIMARY KEY,
    username     VARCHAR(50)  NOT NULL UNIQUE,
    password_hash VARCHAR(255) NOT NULL,
    full_name    VARCHAR(100) NOT NULL,
    role         VARCHAR(20)  NOT NULL,
    email        VARCHAR(100) NULL,
    whatsapp_no  VARCHAR(20)  NULL,
    created_at   DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    is_active    TINYINT(1)   NOT NULL DEFAULT 1,
    CONSTRAINT chk_role CHECK (role IN ('Admin', 'Clerk', 'Viewer'))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE INDEX idx_users_username ON users(username);

-- ============================================================================
-- TABLE: petty_cash_categories
-- ============================================================================
CREATE TABLE IF NOT EXISTS petty_cash_categories (
    category_id   INT AUTO_INCREMENT PRIMARY KEY,
    category_code VARCHAR(10)  NOT NULL UNIQUE,
    category_name VARCHAR(100) NOT NULL,
    created_at    DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ============================================================================
-- TABLE: petty_cash_items
-- ============================================================================
CREATE TABLE IF NOT EXISTS petty_cash_items (
    item_id        INT AUTO_INCREMENT PRIMARY KEY,
    description    VARCHAR(500) NOT NULL,
    category_code  VARCHAR(10)  NOT NULL,
    default_amount DECIMAL(10,2) NULL,
    created_at     DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (category_code) REFERENCES petty_cash_categories(category_code)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ============================================================================
-- TABLE: petty_cash_entries
-- ============================================================================
CREATE TABLE IF NOT EXISTS petty_cash_entries (
    entry_id      INT AUTO_INCREMENT PRIMARY KEY,
    entry_date    DATE          NOT NULL,
    bill_no       VARCHAR(50)   NOT NULL,
    category_code VARCHAR(10)   NOT NULL,
    description   VARCHAR(500)  NOT NULL,
    amount        DECIMAL(10,2) NOT NULL,
    created_by    INT           NOT NULL,
    created_at    DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at    DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    is_deleted    TINYINT(1)    NOT NULL DEFAULT 0,
    deleted_at    DATETIME      NULL,
    CONSTRAINT chk_amount CHECK (amount > 0 AND amount <= 5000),
    FOREIGN KEY (category_code) REFERENCES petty_cash_categories(category_code),
    FOREIGN KEY (created_by) REFERENCES users(user_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE INDEX idx_entries_date    ON petty_cash_entries(entry_date);
CREATE INDEX idx_entries_deleted ON petty_cash_entries(is_deleted);
-- Note: MySQL does not support filtered (partial) unique indexes.
-- Bill-number uniqueness per month is enforced at application level in ValidationService.
CREATE INDEX idx_entries_billno  ON petty_cash_entries(bill_no, entry_date);

-- ============================================================================
-- TABLE: monthly_limits
-- ============================================================================
CREATE TABLE IF NOT EXISTS monthly_limits (
    limit_id              INT AUTO_INCREMENT PRIMARY KEY,
    year                  INT           NOT NULL,
    month                 INT           NOT NULL,
    max_total             DECIMAL(10,2) NOT NULL DEFAULT 25000.00,
    single_bill_limit     DECIMAL(10,2) NOT NULL DEFAULT 5000.00,
    high_value_threshold  DECIMAL(10,2) NOT NULL DEFAULT 3000.00,
    UNIQUE KEY uq_year_month (year, month),
    CONSTRAINT chk_month CHECK (month BETWEEN 1 AND 12)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ============================================================================
-- TABLE: notification_preferences
-- ============================================================================
CREATE TABLE IF NOT EXISTS notification_preferences (
    pref_id          INT AUTO_INCREMENT PRIMARY KEY,
    user_id          INT        NOT NULL UNIQUE,
    email_enabled    TINYINT(1) NOT NULL DEFAULT 1,
    whatsapp_enabled TINYINT(1) NOT NULL DEFAULT 0,
    updated_at       DATETIME   NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (user_id) REFERENCES users(user_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ============================================================================
-- TABLE: notification_log
-- ============================================================================
CREATE TABLE IF NOT EXISTS notification_log (
    notification_id INT AUTO_INCREMENT PRIMARY KEY,
    user_id         INT          NOT NULL,
    type            VARCHAR(50)  NOT NULL,
    message         TEXT         NOT NULL,
    channel         VARCHAR(20)  NOT NULL,
    sent_at         DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    delivered       TINYINT(1)   NOT NULL DEFAULT 0,
    CONSTRAINT chk_channel CHECK (channel IN ('Email', 'WhatsApp')),
    FOREIGN KEY (user_id) REFERENCES users(user_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ============================================================================
-- TABLE: audit_log
-- ============================================================================
CREATE TABLE IF NOT EXISTS audit_log (
    audit_id         INT AUTO_INCREMENT PRIMARY KEY,
    user_id          INT          NOT NULL,
    action_type      VARCHAR(50)  NOT NULL,
    table_name       VARCHAR(50)  NULL,
    record_id        INT          NULL,
    details          TEXT         NULL,
    action_timestamp DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (user_id) REFERENCES users(user_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE INDEX idx_audit_timestamp ON audit_log(action_timestamp);
CREATE INDEX idx_audit_user      ON audit_log(user_id);
CREATE INDEX idx_audit_action    ON audit_log(action_type);

-- ============================================================================
-- TABLE: monthly_reports
-- ============================================================================
CREATE TABLE IF NOT EXISTS monthly_reports (
    report_id    INT AUTO_INCREMENT PRIMARY KEY,
    year         INT          NOT NULL,
    month        INT          NOT NULL,
    status       VARCHAR(20)  NOT NULL DEFAULT 'Draft',
    finalized_by INT          NULL,
    finalized_at DATETIME     NULL,
    created_at   DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UNIQUE KEY uq_report_year_month (year, month),
    CONSTRAINT chk_report_status CHECK (status IN ('Draft', 'Submitted', 'Finalized')),
    CONSTRAINT chk_report_month CHECK (month BETWEEN 1 AND 12),
    FOREIGN KEY (finalized_by) REFERENCES users(user_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ============================================================================
-- TABLE: permissions
-- ============================================================================
CREATE TABLE IF NOT EXISTS permissions (
    permission_id  INT AUTO_INCREMENT PRIMARY KEY,
    permission_key VARCHAR(100) NOT NULL UNIQUE,
    display_name   VARCHAR(200) NOT NULL,
    description    TEXT         NULL,
    category       VARCHAR(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ============================================================================
-- TABLE: role_default_permissions
-- ============================================================================
CREATE TABLE IF NOT EXISTS role_default_permissions (
    role           VARCHAR(20)  NOT NULL,
    permission_key VARCHAR(100) NOT NULL,
    PRIMARY KEY (role, permission_key),
    FOREIGN KEY (permission_key) REFERENCES permissions(permission_key)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ============================================================================
-- TABLE: user_permissions (overrides)
-- ============================================================================
CREATE TABLE IF NOT EXISTS user_permissions (
    user_id        INT          NOT NULL,
    permission_key VARCHAR(100) NOT NULL,
    is_granted     TINYINT(1)   NOT NULL DEFAULT 1,
    granted_by     INT          NOT NULL,
    granted_at     DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (user_id, permission_key),
    FOREIGN KEY (user_id) REFERENCES users(user_id),
    FOREIGN KEY (permission_key) REFERENCES permissions(permission_key)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ============================================================================
-- SEED DATA: Default Categories
-- ============================================================================
INSERT IGNORE INTO petty_cash_categories (category_code, category_name) VALUES
('E5200', 'Vehicle Parts'),
('E5300', 'Office Items'),
('E7800', 'Physical Hardware'),
('E7510', 'Treatments & Staff');

-- ============================================================================
-- SEED DATA: Default Admin User
-- Password: admin123 (BCrypt hashed) - CHANGE ON FIRST LOGIN!
-- ============================================================================
INSERT IGNORE INTO users (username, password_hash, full_name, role, email, is_active) VALUES
('admin', '$2a$11$vZCHqdK.mJFqT7lFgYT9yO5xkp7YPnLRk.7EqfPZ6BzQ5N3QHxMGO', 'System Administrator', 'Admin', NULL, 1);

-- ============================================================================
-- SEED DATA: Permissions (20 items)
-- ============================================================================
INSERT IGNORE INTO permissions (permission_key, display_name, description, category) VALUES
-- Expense Management
('EXPENSE_ADD', 'Add Expense', 'Create new expense entries', 'Expense Management'),
('EXPENSE_EDIT', 'Edit Expense', 'Modify existing expense entries', 'Expense Management'),
('EXPENSE_DELETE', 'Delete Expense', 'Soft-delete expense entries', 'Expense Management'),
('EXPENSE_VIEW', 'View Expenses', 'View expense list on dashboard', 'Expense Management'),

-- Reports
('REPORT_GENERATE', 'Generate Report', 'Generate monthly reports', 'Reports'),
('REPORT_PRINT', 'Print Report', 'Print generated reports', 'Reports'),
('REPORT_FINALIZE', 'Finalize Report', 'Finalize monthly reports (locks month)', 'Reports'),

-- Dashboard
('DASHBOARD_VIEW', 'View Dashboard', 'View dashboard with totals and categories', 'Dashboard'),
('DASHBOARD_NAVIGATE', 'Navigate Months', 'Navigate between different months', 'Dashboard'),

-- User Management
('USER_CREATE', 'Create User', 'Register new users', 'User Management'),
('USER_EDIT', 'Edit User', 'Edit user details and role', 'User Management'),
('USER_DEACTIVATE', 'Deactivate User', 'Deactivate/reactivate user accounts', 'User Management'),
('USER_RESET_PASSWORD', 'Reset Password', 'Reset another user''s password', 'User Management'),
('USER_MANAGE_PERMISSIONS', 'Manage Permissions', 'Assign/revoke per-user permissions', 'User Management'),

-- Settings & Notifications
('SETTINGS_VIEW', 'View Settings', 'View admin/notification settings', 'Settings'),
('SETTINGS_EDIT', 'Edit Settings', 'Edit notification/system settings', 'Settings'),

-- Audit
('AUDIT_VIEW', 'View Audit Log', 'View system audit log', 'Audit'),

-- Category & Item Management
('CATEGORY_MANAGE', 'Manage Categories', 'Add/edit/delete expense categories', 'Category & Item'),
('ITEM_MANAGE', 'Manage Items', 'Add/edit/delete item library entries', 'Category & Item'),

-- Self-Service
('SELF_CHANGE_PASSWORD', 'Change Own Password', 'Change own password', 'Self-Service');

-- ============================================================================
-- SEED DATA: Role Default Permissions
-- ============================================================================
-- Admin gets ALL permissions
INSERT IGNORE INTO role_default_permissions (role, permission_key)
SELECT 'Admin', permission_key FROM permissions;

-- Clerk permissions
INSERT IGNORE INTO role_default_permissions (role, permission_key) VALUES
('Clerk', 'EXPENSE_ADD'),
('Clerk', 'EXPENSE_EDIT'),
('Clerk', 'EXPENSE_VIEW'),
('Clerk', 'REPORT_GENERATE'),
('Clerk', 'REPORT_PRINT'),
('Clerk', 'DASHBOARD_VIEW'),
('Clerk', 'DASHBOARD_NAVIGATE'),
('Clerk', 'ITEM_MANAGE'),
('Clerk', 'SELF_CHANGE_PASSWORD');

-- Viewer permissions (read-only)
INSERT IGNORE INTO role_default_permissions (role, permission_key) VALUES
('Viewer', 'EXPENSE_VIEW'),
('Viewer', 'REPORT_GENERATE'),
('Viewer', 'REPORT_PRINT'),
('Viewer', 'DASHBOARD_VIEW'),
('Viewer', 'DASHBOARD_NAVIGATE'),
('Viewer', 'AUDIT_VIEW'),
('Viewer', 'SELF_CHANGE_PASSWORD');


-- ============================================================================
-- SEED DATA: Monthly Limits for Current Year
-- MySQL does not have T-SQL WHILE loops at top level; use a procedure.
-- ============================================================================
DROP PROCEDURE IF EXISTS seed_monthly_limits;

DELIMITER //
CREATE PROCEDURE seed_monthly_limits()
BEGIN
    DECLARE v_year INT DEFAULT YEAR(CURDATE());
    DECLARE v_month INT DEFAULT 1;
    WHILE v_month <= 12 DO
        INSERT IGNORE INTO monthly_limits (year, month, max_total, single_bill_limit, high_value_threshold)
        VALUES (v_year, v_month, 25000.00, 5000.00, 3000.00);
        SET v_month = v_month + 1;
    END WHILE;
END //
DELIMITER ;

CALL seed_monthly_limits();
DROP PROCEDURE IF EXISTS seed_monthly_limits;

-- ============================================================================
-- SEED DATA: Sample Items for Quick Entry
-- ============================================================================
INSERT IGNORE INTO petty_cash_items (description, category_code, default_amount) VALUES
('Brake pads replacement', 'E5200', 1500.00),
('Engine oil change',      'E5200', 2000.00),
('Tire puncture repair',   'E5200',  500.00),
('Battery replacement',    'E5200', 3500.00),
('A4 paper ream',          'E5300',  800.00),
('Printer ink cartridge',  'E5300', 2500.00),
('Pens and stationery',    'E5300',  350.00),
('Stapler and staples',    'E5300',  250.00),
('Hammer',                 'E7800',  400.00),
('Screwdriver set',        'E7800',  600.00),
('Electrical tape',        'E7800',  150.00),
('Light bulbs',            'E7800',  300.00),
('First aid supplies',     'E7510', 1000.00),
('Staff refreshments',     'E7510',  500.00);

SELECT 'PettyCashDB schema created successfully. Default admin login: admin / admin123' AS Status;
