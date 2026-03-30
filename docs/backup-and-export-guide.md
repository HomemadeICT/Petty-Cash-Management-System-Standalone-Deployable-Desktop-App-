# Backup & Export Guide

> **Version:** 1.1.0 | **Updated:** February 2026

---

## 🗄 Database Backup

### Overview
The Database Backup feature allows administrators to create **portable SQLite database backups**. This protects against data loss and enables disaster recovery.

### Who Can Use This?
- **Administrators** only (requires `BACKUP_DATABASE` permission)
- The backup button is hidden for users without this permission

### How to Backup

1. On the **Dashboard**, click the orange **🗄 Backup DB** button in the action bar
2. The **Database Backup** form opens
3. **Choose a folder** — the default is `Documents\PettyCashBackups`
   - Click **Browse...** to select a different folder
4. Click **🗄 Backup Now**
5. Wait for the status message — backup may take a few seconds
6. On success, you'll be asked if you want to open the output folder

### Backup File Details

| Property         | Detail                                      |
| ---------------- | ------------------------------------------- |
| Format           | SQLite Database (`.sqlite` / `.sqlite_bak`) |
| Default folder   | `C:\Users\<you>\Documents\PettyCashBackups` |
| Naming pattern   | `PettyCash_Backup_YYYYMMDD_HHmmss.sqlite_bak` |
| Timeout          | Insignificant (very fast)                   |

### Backup History
The lower section of the Backup form shows all backup files in the selected folder.

---

## 🔄 Database Restore

### Overview
Administrators can restore the system to a previous state using any valid backup file.

> [!CAUTION]
> **RESTORE OVERWRITES DATA**: Restoring a database completely replaces your current records with the data from the backup file. This cannot be undone.

### How to Restore
1. Open the **Backup DB** form.
2. Ensure the **Source Folder** is pointing to your backups.
3. Select a backup record from the table.
4. Click **🔄 Restore Selected**.
5. Confirm the warning prompt.
6. The application will notify you on success.

## 📊 Export Report to Excel (XLSX)

### Overview
The Excel Export feature lets any authorized user export a monthly expense report as a professionally formatted `.xlsx` file, suitable for printing, archiving, or sharing with supervisors.

### Who Can Use This?
- Anyone who can **generate reports** (requires `REPORT_GENERATE` permission)

### How to Export

1. From the **Dashboard**, click **Generate Report** for the desired month
2. In the **Report Viewer**, click the green **📊 Export to Excel** button
3. A **Save File dialog** opens with a suggested filename (e.g. `PettyCash_Report_2026_02.xlsx`)
4. Choose a location and click **Save**
5. On success, you'll be asked if you want to open the file

### Excel File Structure

The exported file contains **two sheets**:

#### Sheet 1 — Expense Detail

| Column          | Description                      |
| --------------- | -------------------------------- |
| Date            | Entry date (dd/MM/yyyy)          |
| Bill No.        | Bill reference number            |
| Category Code   | E5200, E5300, E7800, E7510       |
| Category Name   | Human-readable category          |
| Description     | Item description                 |
| Amount (LKR)    | Amount in Sri Lankan Rupees      |

#### Professional Excel Features
The exported files are designed for professional use:
- **Strictly Numeric**: Amounts are exported as numbers (not text), eliminating "green corner" warnings in Excel.
- **Dynamic Formulas**: Grand totals and summary sentences use Excel formulas (e.g., `SUM()`). If you manually change a value in the spreadsheet, the totals and the Sinhala request text will update automatically.
- **Adaptive Paper Layout**: 
    - **Portrait**: Used for standard reports.
    - **Landscape**: Automatically applied if the number of expense categories exceeds the width of standard A4 portrait.
- **Accounting Formatting**: Zero values are displayed as a clean dash (`-`).

- Rows are sorted by date then bill number
- A **Grand Total** row appears at the bottom
- Header row is styled in dark blue with white text
- Alternating row colors for readability

#### Sheet 2 — Category Summary

| Column          | Description                 |
| --------------- | --------------------------- |
| Category Code   | Budget vote code            |
| Category Name   | Human-readable name         |
| Bill Count      | Number of bills             |
| Total (LKR)     | Sum of amounts              |

- Includes total row and **monthly limit / remaining balance** info
- Remaining balance shown in green (positive) or red (over-budget)

---

## Troubleshooting

| Issue                          | Resolution                                             |
| ------------------------------ | ------------------------------------------------------ |
| "Could not determine database" | Verify database connection in App.config                |
| Backup takes too long          | Check SQL Server performance, try during off-hours      |
| Excel export error             | Ensure the folder is writable and not open in Explorer   |
| Button not visible             | User may lack the required permission — contact admin   |
