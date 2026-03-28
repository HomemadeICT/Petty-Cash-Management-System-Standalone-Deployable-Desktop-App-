# Petty Cash Management System - Administrator Guide

**Application Version:** 1.0  
**Last Updated:** February 9, 2026  
**Target Audience:** Supervisors & System Administrators

---

## 📋 Table of Contents

1. [Administrative Overview](#administrative-overview)
2. [User Management](#user-management)
3. [Business Rules Configuration](#business-rules-configuration)
4. [System Settings](#system-settings)
5. [Monitoring & Audit Logs](#monitoring--audit-logs)
6. [Reports & Analysis](#reports--analysis)
7. [Backup & Maintenance](#backup--maintenance)
8. [Troubleshooting](#troubleshooting)

---

## Administrative Overview

### Admin Responsibilities

As an administrator, you have access to:
- ✅ User account management
- ✅ Business rule configuration
- ✅ System settings and preferences
- ✅ Complete audit logs
- ✅ Advanced reporting
- ✅ Database backup & recovery
- ✅ System diagnostics

### Access Levels

```
┌─────────────────────────────────────────────────┐
│ ROLE HIERARCHY                                  │
├─────────────────────────────────────────────────┤
│ ADMIN (Full Access)                             │
│ ├─ Create/Edit/Delete Users                    │
│ ├─ Configure Business Rules                    │
│ ├─ View All Audit Logs                         │
│ ├─ System Settings                             │
│ └─ Backup/Restore Database                     │
│                                                 │
│ SUPERVISOR (Review & Approve)                  │
│ ├─ View all User Entries                       │
│ ├─ Approve High-Value Exceptions               │
│ ├─ Generate Reports                            │
│ └─ View Own Audit Log                          │
│                                                 │
│ USER/CLERK (Data Entry)                        │
│ ├─ Add/Edit Own Entries                        │
│ ├─ View Own Entries                            │
│ └─ Generate Own Reports                        │
└─────────────────────────────────────────────────┘
```

---

## User Management

### View All Users

**Menu:** Settings → User Management → List Users

Displays all active users in the system:

```
┌──────────────────────────────────────────────────────────┐
│  USER MANAGEMENT                                         │
├──────────────────────────────────────────────────────────┤
│ Name          │ Username  │ Role       │ Status │ Action │
├──────────────────────────────────────────────────────────┤
│ John Silva    │ jsil001   │ Clerk      │ Active │ [Edit] │
│ Mary Woods    │ mwood001  │ Clerk      │ Active │ [Edit] │
│ ES Sir        │ admin     │ Admin      │ Active │ [Edit] │
│ Jayasinghe    │ supervisor│ Supervisor │ Active │ [Edit] │
└──────────────────────────────────────────────────────────┘
```

### Create a New User

**Step 1:** Click "Settings" → "User Management" → "Create User"

**Step 2:** Fill in User Details Form:

```
┌───────────────────────────────────────────┐
│  CREATE NEW USER                          │
├───────────────────────────────────────────┤
│                                           │
│  Full Name:     [                    ]   │
│                 (e.g., "John Silva")     │
│                                           │
│  Username:      [                    ]   │
│                 (e.g., "jsil001")        │
│                 [Check Availability]     │
│                                           │
│  Email:         [                    ]   │
│                 (e.g., john@ceb.lk)      │
│                                           │
│  Role:          [▼ Select Role]          │
│                 ├─ Clerk (Data Entry)    │
│                 ├─ Supervisor (Review)   │
│                 └─ Admin (Full Access)   │
│                                           │
│  Initial Password: [Generate Random]    │
│                 [Show] __________________ │
│                                           │
│  Status:        [○ Active   ○ Inactive]  │
│                                           │
│  [Create User] [Clear] [Cancel]          │
└───────────────────────────────────────────┘
```

**Step 3:** Click "Create User"

**Step 4:** System generates:
- ✅ Username (confirmed unique)
- ✅ Temporary random password
- ✅ User record in database

**Step 5:** Provide to New User:
- Username
- Temporary password
- Tell them to change password on first login

### Edit User Details

**Step 1:** Click "Settings" → "User Management" → Find user in list

**Step 2:** Click "[Edit]" button next to user name

**Step 3:** Edit Form opens - Change:
- Full name
- Email
- Role
- Status (Active/Inactive)

**Cannot Change:** Username (locked after creation)

**Step 4:** Click "Save"

### Reset User Password

**Scenario:** User forgot their password

**Step 1:** Go to "Settings" → "User Management"

**Step 2:** Find the user in the list

**Step 3:** Click "[Reset Password]" button

**Step 4:** Confirmation: "Generate new temporary password?"

**Step 5:** System generates new password → Provide to user

**Step 6:** User must change on next login

### Deactivate a User

**Scenario:** User left the department, on leave, or terminated.

**Step 1:** Edit the user

**Step 2:** Change Status from "Active" to "Inactive"

**Step 3:** Click "Save"

**Effect:**
- ✓ User cannot log in anymore
- ✓ Old entries remain (not deleted)
- ✓ Can reactivate if user returns

### Delete a User

**⚠️ WARNING - PERMANENT ACTION**

Only use if user created by mistake or test account.

**Step 1:** Edit user

**Step 2:** Click "[Delete User]" button

**Step 3:** Confirm: "Are you sure? Cannot undo this action."

**Step 4:** Click "Yes" to permanently delete

**Effect:**
- ✗ User permanently removed
- ✗ Old entries become orphaned (show "Unknown User")
- ✗ Cannot recover

---

## Business Rules Configuration

### Understanding Business Rules

The system enforces 8 core business rules to maintain financial control:

| Rule | Current Value | Type | Impact |
|------|---|---|---|
| **BR1:** Monthly Limit | LKR 25,000 | Hard Limit | ❌ Blocks over-limit entries |
| **BR2:** Single Bill Limit | LKR 5,000 | Hard Limit | ❌ Rejects bills > LKR 5,000 |
| **BR3:** Positive Amount | > 0 | Hard Limit | ❌ Rejects zero/negative |
| **BR4:** High-Value Warning | LKR 3,000 | Soft Warning | ⚠️ Allows but flags |
| **BR5:** Category Validation | 3 categories | Hard Limit | ❌ Must choose valid category |
| **BR6:** Unique Bill Number | Per month | Hard Limit | ❌ No duplicate bills per month |
| **BR7:** Date Validation | No future dates | Hard Limit | ❌ Cannot enter future date |
| **BR8:** Description Min Length | 10 characters | Hard Limit | ❌ Description too short |

### View Current Rules

**Menu:** Settings → Business Rules → View Rules

Displays current configuration:

```
┌──────────────────────────────────────────────────┐
│  BUSINESS RULES CONFIGURATION                    │
├──────────────────────────────────────────────────┤
│                                                  │
│  BR1: Monthly Limit                              │
│       Current: LKR 25,000                       │
│       Type: HARD LIMIT (Blocking)               │
│       Last Modified: 01-Feb-2026 by Admin       │
│       [Edit]                                     │
│                                                  │
│  BR2: Single Bill Limit                          │
│       Current: LKR 5,000                        │
│       Type: HARD LIMIT (Blocking)               │
│       Last Modified: 01-Feb-2026 by Admin       │
│       [Edit]                                     │
│                                                  │
│  BR4: High-Value Warning Threshold               │
│       Current: LKR 3,000                        │
│       Type: SOFT WARNING (Allows but flags)     │
│       Last Modified: 01-Feb-2026 by Admin       │
│       [Edit]                                     │
│                                                  │
└──────────────────────────────────────────────────┘
```

### Modify a Business Rule

**⚠️ IMPORTANT:** Only modify rules with management approval.

**Step 1:** Click "Settings" → "Business Rules" → "[Edit]" next to rule

**Step 2:** Modify Value Form:

```
┌───────────────────────────────────┐
│  EDIT BUSINESS RULE: BR1           │
├───────────────────────────────────┤
│                                   │
│  Rule: Monthly Limit              │
│  Description: Maximum petty cash  │
│              allowance per month  │
│                                   │
│  Current Value: LKR 25,000        │
│  New Value:     [           ]     │
│                                   │
│  Reason for Change:               │
│  [                              ] │
│  [                              ] │
│  (e.g., "Budget increased for    │
│   Q2 2026 per management memo")   │
│                                   │
│  Approval:      [○ Awaiting]      │
│                                   │
│  [Save] [Cancel]                  │
└───────────────────────────────────┘
```

**Step 3:** Enter new value

**Step 4:** Document reason for change

**Step 5:** Click "Save"

**Effect:**
- Rule updated immediately
- All new entries use new rule value
- Change logged in audit trail

### Common Rule Modifications

#### Scenario 1: Increase Monthly Limit
**Business Reason:** Budget increase for high-demand period

1. Go to BR1 (Monthly Limit)
2. Change from LKR 25,000 to LKR 35,000 (example)
3. Document: "Budget increase for Q2 2026 per MD approval"
4. Save

#### Scenario 2: Adjust High-Value Warning
**Business Reason:** More sensitive monitoring needed

1. Go to BR4 (High-Value Warning)
2. Change from LKR 3,000 to LKR 2,000
3. Document reason
4. Save
5. All new bills > LKR 2,000 will trigger warning

#### Scenario 3: Enforce Stricter Single Bill Limit
**Business Reason:** Financial control tightening

1. Go to BR2 (Single Bill Limit)
2. Change from LKR 5,000 to LKR 3,000
3. Document: "New procurement policy effective Feb 2026"
4. Save

---

## System Settings

### Configure Application Settings

**Menu:** Settings → System Configuration

```
┌────────────────────────────────────────────┐
│  SYSTEM CONFIGURATION                      │
├────────────────────────────────────────────┤
│                                            │
│  GENERAL SETTINGS                          │
│  ├─ Organization Name:                    │
│  │  [Ceylon Electricity Board - Haliela]  │
│  │                                        │
│  ├─ Fiscal Year Start:                    │
│  │  [January 1] or [Other: ____]          │
│  │                                        │
│  └─ Default Currency:                     │
│     [LKR (Sri Lankan Rupee)]              │
│                                            │
│  NOTIFICATION SETTINGS                    │
│  ├─ Email Notifications: [✓] Enabled      │
│  ├─ High-Value Bill Alerts: [✓] Enabled   │
│  ├─ Monthly Report Reminder: [✓] Enabled  │
│  │  (Sent on: [Last day of month ▼])     │
│  │                                        │
│  └─ Admin Email:                          │
│     [admin@ceb.lk____________]            │
│                                            │
│  REPORT SETTINGS                          │
│  ├─ Default Report Format: [PDF ▼]        │
│  ├─ Include Company Header: [✓]           │
│  └─ Include Approval Signature Line: [✓] │
│                                            │
│  DATABASE SETTINGS                        │
│  ├─ Database Server:                      │
│  │  [localhost (Current Machine)]         │
│  │                                        │
│  ├─ Backup Frequency: [Daily ▼]           │
│  ├─ Backup Location:                      │
│  │  [C:\PettyCash\Backups]                │
│  │                                        │
│  └─ [Test Connection] ✓ Connected         │
│                                            │
│  [Save Settings] [Restore Defaults]       │
└────────────────────────────────────────────┘
```

### Change Organization Name

1. Click "System Configuration"
2. Find "Organization Name" field
3. Edit the value
4. Click "Save Settings"
5. Changes appear in all reports

### Enable/Disable Notifications

**High-Value Bill Alerts:**
1. Go to "Notification Settings"
2. Check/Uncheck "High-Value Bill Alerts"
3. Save
4. When enabled, supervisor notified of bills > LKR 3,000

**Monthly Report Reminder:**
1. Go to "Notification Settings"
2. Check "Monthly Report Reminder"
3. Set send date (e.g., "Last day of month")
4. Save

---

## Monitoring & Audit Logs

### View Audit Logs

**Menu:** Settings → Audit Logs

Shows complete record of all system activities:

```
┌────────────────────────────────────────────────────────┐
│  AUDIT LOG VIEWER                                      │
├────────────────────────────────────────────────────────┤
│ Date/Time    │ User      │ Action │ Record │ Details   │
├────────────────────────────────────────────────────────┤
│ 09-Feb 14:30 │ Clerk1    │ CREATE │Entry  │ Bill INV-234
│              │           │        │       │ LKR 2,450  │
│ 09-Feb 14:25 │ Supervisor│ VIEW   │Entry  │ Bill INV-233
│              │           │        │       │ (viewed)   │
│ 08-Feb 10:15 │ Admin     │ UPDATE │User   │ Clerk1     │
│              │           │        │       │ (activated)│
│ 08-Feb 09:44 │ Clerk1    │ DELETE │Entry  │ Bill INV-200
│              │           │        │       │ (soft del) │
│                                                        │
│ [Filter by User ▼] [Filter by Action ▼]              │
│ [Date Range: 01-Feb to 09-Feb] [Search ____]         │
│                                                        │
│ [Export to CSV]                                        │
└────────────────────────────────────────────────────────┘
```

### Understanding Audit Log Entries

| Action | Meaning |
|--------|---------|
| **CREATE** | New record created |
| **UPDATE** | Record modified |
| **DELETE** | Record soft-deleted (soft-deleted, recoverable) |
| **LOGIN** | User logged in |
| **LOGOUT** | User logged out |
| **VIEW** | User accessed a record |
| **APPROVE** | Supervisor approved entry/report |
| **CONFIG_CHANGE** | System setting changed |

### Investigate User Activity

**Scenario:** Need to check what Clerk1 did today

**Step 1:** Go to "Audit Logs"

**Step 2:** Click "Filter by User" → Select "Clerk1"

**Step 3:** Set Date Range: Select today

**Step 4:** View all actions by that user → Identify suspicious activity

### Detect Unauthorized Changes

**Scenario:** Entry amount seems to have changed unexpectedly

**Step 1:** Find entry in "View Entries"

**Step 2:** Click entry → Click "View Audit Trail"

**Step 3:** See all modifications to this entry:
- Original values
- Who changed it
- When changed
- What changed

**Example:**
```
Entry INV-234 Modifications:
08-Feb 10:00 | Clerk1    | Created | Amount: LKR 2,450
08-Feb 14:22 | Supervisor| Updated | Amount: LKR 3,450
                         | Reason: "Corrected per receipt"
```

---

## Reports & Analysis

### Generate Monthly Report (Admin View)

More detailed than user view:

**Step 1:** Menu: Reports → Monthly Report

**Step 2:** Select Month/Year

**Step 3:** Report shows:
- All entries (including who entered)
- Category breakdown
- High-value bills
- User-wise summary
- Approval status

### Generate User Activity Report

Shows each user's contributions:

```
┌──────────────────────────────────────────────┐
│  USER ACTIVITY REPORT - FEBRUARY 2026        │
├──────────────────────────────────────────────┤
│                                              │
│ Clerk1                                       │
│ ├─ Entries Added: 16                        │
│ ├─ Total Amount: LKR 8,500                  │
│ ├─ Avg Bill: LKR 531                        │
│ └─ Last Entry: 09-Feb 14:30                 │
│                                              │
│ Clerk2                                       │
│ ├─ Entries Added: 8                         │
│ ├─ Total Amount: LKR 3,950                  │
│ ├─ Avg Bill: LKR 494                        │
│ └─ Last Entry: 08-Feb 16:15                 │
│                                              │
│ TOTALS                                       │
│ ├─ Total Entries: 24                        │
│ ├─ Total Amount: LKR 12,450                 │
│ └─ Avg Per Entry: LKR 519                   │
│                                              │
└──────────────────────────────────────────────┘
```

### Generate Variance Analysis

Compares actual spending vs budgeted amount:

- Budget: LKR 25,000
- Actual: LKR 12,450
- Variance: LKR 12,550 (50.2%) **Under budget** ✓

---

## Backup & Maintenance

### Automatic Backup

The system performs automatic daily backups:
- **Time:** 11:59 PM each day
- **Location:** `C:\PettyCash\Backups`
- **Format:** SQL database backup file

### Manual Backup

**Step 1:** Menu: Settings → Backup & Recovery → Create Backup

**Step 2:** Confirmation: "Create backup now?"

**Step 3:** Click "Yes"

**Step 4:** System creates backup file:
- **Name:** `petty_cash_backup_20260209_143045.bak`
- **Size:** ~5-10 MB
- **Location:** Backups folder

### Restore from Backup

**⚠️ CAREFUL - This deletes current data and restores old data**

**Step 1:** Menu: Settings → Backup & Recovery → Restore

**Step 2:** Select backup file from list

```
┌──────────────────────────────────────────┐
│  SELECT BACKUP TO RESTORE                │
├──────────────────────────────────────────┤
│ File Name                  │ Date/Time   │
├──────────────────────────────────────────┤
│ backup_20260209_143045.bak │ 09-Feb 2:30 │
│ backup_20260208_235959.bak │ 08-Feb 11:59│
│ backup_20260207_235945.bak │ 07-Feb 11:59│
│ [Select one]               │             │
│ [Restore] [Cancel]                       │
└──────────────────────────────────────────┘
```

**Step 3:** Select desired backup

**Step 4:** Click "Restore"

**Step 5:** Confirmation: "WARNING: This will overwrite current data. Continue?"

**Step 6:** Click "Yes" to confirm

**Step 7:** System restores from backup (takes 1-2 minutes)

### Database Maintenance

**Check Database Integrity:**
- Menu: Settings → Diagnostics → Check Database
- Reports any corruption or errors
- If errors found → Try automatic repair OR restore from backup

**Optimize Database:**
- Menu: Settings → Maintenance → Optimize Database
- Improves performance
- Safe to run monthly

---

## Troubleshooting

### Problem 1: Cannot Create User - "Username Already Exists"
**Cause:** Username is already taken

**Solution:**
1. Choose a different username
2. Example: "jsil002" instead of "jsil001"
3. Or check if user already exists in system

### Problem 2: Business Rule Change Not Taking Effect
**Cause:** Cached data or restart needed

**Solution:**
1. Save the rule change
2. All users should close and restart application
3. Rule takes effect on new entries (old entries unaffected)

### Problem 3: Audit Log Not Showing Recent Activity
**Cause:** No filter applied or needs refresh

**Solution:**
1. Click "Refresh" button
2. Check filters aren't hiding results
3. Verify date range includes current date

### Problem 4: Backup File Very Large (>100 MB)
**Cause:** Database has accumulated too much history

**Solution:**
1. Normal data growth - not a problem
2. Reduce backup retention (keep only 30 days)
3. Menu: Settings → Backup Settings → Retention Policy
4. Set to "Keep last 30 days"

### Problem 5: Database Connection Error
**Cause:** Database service stopped or crashed

**Solution:**
1. Menu: Settings → Diagnostics → Test Connection
2. If fails: Database service is down
3. Restart the database:
   - Right-click Start Menu → Services
   - Find "SQL Server" or relevant service
   - Click "Restart"
4. Test connection again

### Problem 6: Reports Not Printing
**Cause:** Printer not configured or offline

**Solution:**
1. Check if printer is powered on and connected
2. Try printing to PDF instead (Settings → Backup to file)
3. Check Windows Printer Settings
4. Try different printer

---

## Regular Maintenance Checklist

### Daily Tasks
- ✓ Review high-value bill alerts
- ✓ Check system logs for errors

### Weekly Tasks
- ✓ Verify backups completed successfully
- ✓ Check database connection status
- ✓ Review audit logs for anomalies

### Monthly Tasks
- ✓ Review monthly reports
- ✓ Approve all entries for signing
- ✓ Optimize database
- ✓ Check user activity
- ✓ Verify business rules are appropriate

### Quarterly Tasks
- ✓ Archive old backups (keep for 90 days)
- ✓ Review and update business rules if needed
- ✓ Review user access (deactivate unused accounts)

### Annually Tasks
- ✓ Complete system audit
- ✓ Plan for system upgrades
- ✓ Review and document all changes made during year
- ✓ Backup configuration and settings

---

**End of Administrator Guide**
