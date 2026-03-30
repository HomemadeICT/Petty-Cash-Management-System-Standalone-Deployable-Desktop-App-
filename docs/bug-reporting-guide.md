# 🐞 Bug Reporting Guide

This guide provides instructions for users and administrators on how to report technical issues (bugs) within the Petty Cash Management System.

---

## 🏗️ Before Reporting

1.  **Check Documentation**: Verify if the "bug" is actually a documented business rule (e.g., the Dec 15–31 rule).
2.  **Restart the App**: Close and reopen the application to see if the issue persists.
3.  **Check Permissions**: Verify that your user role (Clerk or Admin) has the permission to perform the action.

---

## 📝 How to Report

To help our technical team fix the issue quickly, please provide the following details:

### 1. Title
A brief, one-sentence description of the problem.
*Example: "Dashboard fails to load for January 2026"*

### 2. Steps to Reproduce
Tell us exactly what you were doing when the error occurred.
1. Open the application and log in as Admin.
2. Click on the Month selector and choose "January 2026".
3. Wait for 2 seconds.
4. **Result**: The application displays a "Database Error" message.

### 3. Expected vs. Actual Result
*   **Expected**: I should see the expense list for January 2026.
*   **Actual**: An error popup appears and the list stays empty.

### 4. Environment
- **Operating System**: (e.g., Windows 10, Windows 11)
- **User Role**: (e.g., Clerk, Admin)
- **Reporting Month**: (The month you were viewing when it happened)

---

## 📎 Attachments (Very Important)

> [!IMPORTANT]
> **Screenshots**: Please take a screenshot of the error message or the broken UI. Use the "Print Screen" key or Snipping Tool.

> [!TIP]
> **Database Backups**: If the error is related to missing data, please provide the most recent backup file from `Documents\PettyCashBackups`.

---

## 📬 Where to Send Reports

Please forward your bug reports to the System Administrator or the designated IT contact person at CEB Haliela.

---

## 🏠 System Log Files

If requested by IT, you can find the application log files in:
`C:\Users\<YourUsername>\AppData\Local\PettyCashSystem\Logs`
