# 🗄️ Database Documentation

This document details the database structure for the **Petty Cash Management System**.

---

## 🏗️ Database Engine
- **Engine:** SQLite 3
- **File Name:** `PettyCash.db`
- **Location:** Managed by `DbContext.vb` (defaults to Application Data folder)
- **Migration Story:** Migrated from SQL Server to SQLite in v1.1.5 to provide a simplified, zero-configuration deployment for CEB branches.

---

## 📊 Table Definitions

### 1. `users`
Stores user accounts and authentication data.
| Column | Type | Description |
|--------|------|-------------|
| `id` | INTEGER (PK) | Unique user ID |
| `username` | TEXT (Unique) | Login name |
| `password_hash` | TEXT | BCrypt hashed password |
| `role_id` | INTEGER | FK to roles table |
| `is_active` | BOOLEAN | Account status |

### 2. `entries`
Stores all petty cash transactions.
| Column | Type | Description |
|--------|------|-------------|
| `id` | INTEGER (PK) | Entry ID |
| `entry_date` | TEXT | Date of expense (YYYY-MM-DD) |
| `bill_number` | TEXT | Reference bill number |
| `category_id` | INTEGER | FK to categories table |
| `description` | TEXT | Item details |
| `amount` | REAL | Transaction amount in LKR |
| `created_by` | INTEGER | FK to users table |

### 3. `categories`
Stores expense categories and their monthly limits.
| Column | Type | Description |
|--------|------|-------------|
| `id` | INTEGER (PK) | Category ID |
| `code` | TEXT | e.g. E5200, E5300 |
| `name` | TEXT | Human readable name |
| `monthly_limit` | REAL | Spending cap for this category |

### 4. `audit_logs`
Stores a history of every significant action in the system.
| Column | Type | Description |
|--------|------|-------------|
| `id` | INTEGER (PK) | Log ID |
| `timestamp` | TEXT | When it happened |
| `user_id` | INTEGER | Who did it |
| `action` | TEXT | e.g. LOGIN, ADD_EXPENSE, DELETE_USER |
| `details` | TEXT | JSON or text details of the action |

---

## 🛠️ Typical Queries

### Get Monthly Total
```sql
SELECT SUM(amount) 
FROM entries 
WHERE strftime('%Y-%m', entry_date) = '2026-03';
```

### Search Audit Log
```sql
SELECT * FROM audit_logs 
ORDER BY timestamp DESC 
LIMIT 50;
```

---

## 📦 Maintenance & Backups
SQLite is a single file. Backups are performed by performing a simple file copy of `PettyCash.db` to a backup location. The application handles this automatically via the **Backup DB** feature.

**Last Updated:** March 5, 2026
