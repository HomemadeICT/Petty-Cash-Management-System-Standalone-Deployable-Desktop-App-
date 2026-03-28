# Petty Cash Management System - User Manual

**Application Version:** 1.0  
**Last Updated:** February 9, 2026  
**Target Audience:** Office Staff (Clerks & Regular Users)

---

## 📋 Table of Contents

1. [Getting Started](#getting-started)
2. [Logging In](#logging-in)
3. [Dashboard Overview](#dashboard-overview)
4. [Recording Petty Cash Entries](#recording-petty-cash-entries)
5. [Viewing & Editing Entries](#viewing--editing-entries)
6. [Generating Reports](#generating-reports)
7. [Common Tasks](#common-tasks)
8. [Frequently Asked Questions (FAQ)](#frequently-asked-questions)
9. [Troubleshooting](#troubleshooting)

---

## Getting Started

### System Requirements
- **Windows Version:** Windows 7 SP1 or later (Windows 10 / 11 recommended)
- **RAM:** Minimum 2 GB
- **Disk Space:** 100 MB free space
- **Internet:** Not required (works offline)

### Installing the Application

1. **Run the Installer**
   - Double-click `PettyCashSetup.exe`
   - Follow the installation wizard
   - Choose installation folder (default: `C:\Program Files\PettyCash`)
   - Click "Install"

2. **First-Time Setup**
   - Application will start automatically after installation
   - Database initializes automatically
   - You are ready to log in

3. **Shortcut**
   - Desktop shortcut `Petty Cash Manager` created automatically
   - Or access from Start Menu → Petty Cash Manager

---

## Logging In

### Step 1: Open the Application
- Double-click the application icon on your desktop, or
- Click Start Menu → Search → "Petty Cash" → Click the app

### Step 2: Login Screen
You will see the login screen with two fields:
- **Username** field
- **Password** field

### Step 3: Enter Your Credentials

**For Data Entry Clerks:**
- **Username:** `clerk1` or `clerk2` (as assigned by your supervisor)
- **Password:** Your assigned password

**For Supervisors:**
- **Username:** `admin` (or assigned username)
- **Password:** Your assigned password

> 💡 **Tip:** Your supervisor will provide your username and initial password.

### Step 4: Click "Login"
- If credentials are correct → Main dashboard opens
- If incorrect → Error message appears → Try again

### Forgot Password?
Contact your supervisor (ES Sir) to reset your password.

---

## Dashboard Overview

After login, you'll see the **Main Dashboard** with:

```
┌─────────────────────────────────────────────────┐
│  Petty Cash Management System          [v1.0]  │
│  Logged in as: Clerk1  | February 2026        │
├─────────────────────────────────────────────────┤
│                                               │
│  📊 THIS MONTH'S SUMMARY                      │
│  ├─ Total Spent:        LKR 12,450         │
│  ├─ Monthly Limit:      LKR 25,000         │
│  ├─ Remaining Budget:   LKR 12,550         │
│  └─ Number of Entries:  24                 │
│                                               │
│  📑 CATEGORY BREAKDOWN                       │
│  ├─ Vehicle Parts:      LKR 4,200          │
│  ├─ Office Items:       LKR 3,100          │
│  └─ Staff Treats:       LKR 5,150          │
│                                               │
│  🔴 ALERTS (if any)                          │
│  ├─ ⚠️  High-value bill detected (Bill #1523)│
│  └─ ⚠️  2 bills above LKR 3,000             │
│                                               │
├─────────────────────────────────────────────────┤
│ [+ Add New Entry]  [View Entries]  [Reports]  │
│ [Settings]         [Logout]                   │
└─────────────────────────────────────────────────┘
```

### Dashboard Elements

| Element | Meaning |
|---------|---------|
| **Total Spent** | All expenses recorded this month |
| **Monthly Limit** | Maximum allowed budget (LKR 25,000) |
| **Remaining Budget** | How much you can still spend this month |
| **Category Breakdown** | Expenses grouped by type (Vehicle, Office, Treats) |
| **Alerts** | ⚠️ High-value bills or policy warnings |

---

## Recording Petty Cash Entries

### When to Record an Entry
- ✅ You purchase items with petty cash
- ✅ You submit receipts/bills to the office
- ✅ At end of each day or when making a purchase

### Step-by-Step: Adding a New Entry

#### Step 1: Click "Add New Entry" Button
From the dashboard, click the **+ Add New Entry** button.

The **Expense Entry Form** opens:

```
┌────────────────────────────────────────┐
│  ADD NEW PETTY CASH ENTRY              │
├────────────────────────────────────────┤
│                                        │
│  Bill Date:        [DD/MM/YYYY]       │
│                    📅 (Click to pick)  │
│                                        │
│  Bill Number:      [           ]      │
│                    (e.g., INV-001)     │
│                                        │
│  Category:         [▼ Select...]       │
│                    - Vehicle Parts     │
│                    - Office Items      │
│                    - Staff Treats      │
│                                        │
│  Description:      [                ] │
│                    [                ] │
│                    [                ] │
│                    (min 10 characters) │
│                                        │
│  Amount (LKR):     [              ]   │
│                                        │
│  ────────────────────────────────────  │
│  ✓ VALIDATION CHECKS               │
│  ✓ Amount: LKR 2,450 (OK)           │
│  ✓ Bill: BNK-234 (Unique)           │
│  ⚠️  Amount is high (>3,000)         │
│  ────────────────────────────────────  │
│                                        │
│  [Save Entry]  [Cancel]  [Clear]      │
└────────────────────────────────────────┘
```

#### Step 2: Fill in Bill Date
1. Click the date field
2. Select the date from calendar (when you made the purchase)
3. Click OK

**Important:** You can enter bills from **previous days** (retroactive entries are allowed).

#### Step 3: Enter Bill Number
- **Bill Number:** Unique identifier from the receipt/invoice
- **Format Example:** `INV-001`, `BNK-234`, `RCP-5678`
- **Rule:** Each bill number must be unique within the same month
- ❌ Cannot duplicate: If you try to enter a bill you already recorded, you'll get an error

#### Step 4: Select Category
Click the Category dropdown and choose:

| Category | Use For |
|----------|---------|
| **Vehicle Parts & Maintenance** | Oil, spare parts, repairs, fuel additives, etc. |
| **Office Use Items** | Stationery, files, folders, cleaning supplies, etc. |
| **Staff Treatments & Welfare** | Tea, snacks, lunch treats, office gifts, etc. |

#### Step 5: Enter Description
- **What:** Brief description of what you bought
- **Why:** Why it was purchased
- **Example:**
  ```
  "3L Motor Oil + Fuel Filter for Generator 
   Generator maintenance required for backup power"
  ```
- **Minimum:** 10 characters
- **Tip:** Be descriptive so supervisors understand the purchase

#### Step 6: Enter Amount
- **Amount:** How much you spent (in LKR)
- **Format:** `1500` or `1500.50`
- **Rules:**
  - ✅ Maximum single bill: **LKR 5,000**
  - ✅ Must be greater than zero
  - ⚠️  High-value warning: Bill > LKR 3,000 (Shows warning, NOT blocked)

**Example Amounts:**
- Office stationery: `750`
- Staff snacks: `1200`
- Vehicle maintenance: `4500`

#### Step 7: Review Validation Checks
Before clicking Save, review the validation summary at the bottom:

**✓ Green check** = Passes (OK to save)  
**⚠️ Yellow warning** = Caution (Still OK, but supervisor will see warning)  
**✗ Red error** = Fails (Cannot save)

#### Step 8: Click "Save Entry"
- If all validations pass → Entry saved ✅
- If error → Error message appears → Fix the issue → Try again

**Success Message:**
```
✅ Entry saved successfully!
   Bill #INV-234 | LKR 2,450 | Vehicle Parts
```

### Rules for Entries (What You MUST Know)

| Rule | Details | Consequence |
|------|---------|-------------|
| **BR1: Monthly Limit** | Cannot exceed LKR 25,000 per month | ❌ Entry BLOCKED |
| **BR2: Single Bill Limit** | Each bill must be ≤ LKR 5,000 | ❌ Entry BLOCKED |
| **BR3: Positive Amount** | Amount must be > 0 | ❌ Entry BLOCKED |
| **BR4: High-Value Warning** | Bill > LKR 3,000 triggers warning | ⚠️ Allowed but flagged |
| **BR5: Valid Category** | Must select from 3 categories | ❌ Entry BLOCKED |
| **BR6: Unique Bill Number** | Each bill number unique per month | ❌ Entry BLOCKED |
| **BR7: Valid Date** | Entry date cannot be in future | ❌ Entry BLOCKED |
| **BR8: Description Length** | Minimum 10 characters | ❌ Entry BLOCKED |

---

## Viewing & Editing Entries

### View All Entries

**Step 1:** Click "View Entries" button on dashboard

**Step 2:** You'll see a table:

```
┌──────────────────────────────────────────────────────────┐
│  PETTY CASH ENTRIES - FEBRUARY 2026                      │
├──────────────────────────────────────────────────────────┤
│  Date      │ Bill # │ Category │ Description │ Amount    │
├──────────────────────────────────────────────────────────┤
│ 08-Feb-26  │ INV001 │ Vehicle  │ Motor Oil   │ LKR 2,450 │
│ 07-Feb-26  │ INV002 │ Office   │ File Folders│ LKR 1,200 │
│ 06-Feb-26  │ INV003 │ Welfare  │ Tea & Snacks│ LKR   950 │
│            │        │          │             │           │
│ [Filter by Category ▼] [Sort by Date ▼] [Search: ____]  │
└──────────────────────────────────────────────────────────┘
```

### Edit an Entry

**Step 1:** Find the entry in the list

**Step 2:** Double-click it (or click "Edit" button)

**Step 3:** Edit form opens → Change values → Click "Update"

**Important Rules for Editing:**
- ✅ Can edit date, description, amount
- ✅ Can change category
- ❌ Cannot change bill number (once saved, it's locked)
- ⚠️  Changing amount might trigger monthly limit validation

### Delete an Entry

**Option 1: From View Entries Screen**
1. Find the entry
2. Click "Delete" button
3. Confirm: "Are you sure?" → Click "Yes"
4. Entry is marked as deleted (kept in audit log for records)

**Option 2: From Entry Details**
1. Open the entry for editing
2. Click "Delete" button
3. Confirm deletion

**Note:** Deleted entries are soft-deleted (not permanently removed) for audit purposes.

---

## Generating Reports

### Monthly Report

**Purpose:** Summary of all expenses for the current month, ready for supervisor review and signing.

**Step 1:** Click "Reports" button from dashboard

**Step 2:** Select "Monthly Report"

**Step 3:** Report generates showing:

```
┌──────────────────────────────────────────────┐
│  PETTY CASH MONTHLY REPORT                   │
│  Month: FEBRUARY 2026                        │
├──────────────────────────────────────────────┤
│                                              │
│  SUMMARY                                     │
│  Total Expenses:        LKR 12,450          │
│  Monthly Limit:         LKR 25,000          │
│  Remaining Budget:      LKR 12,550          │
│  Number of Entries:     24                   │
│                                              │
│  CATEGORY-WISE BREAKDOWN                    │
│  ┌────────────────────────────────────────┐ │
│  │ Vehicle Parts:    LKR  4,200 (33.7%)   │ │
│  │ Office Items:     LKR  3,100 (24.9%)   │ │
│  │ Staff Welfare:    LKR  5,150 (41.4%)   │ │
│  └────────────────────────────────────────┘ │
│                                              │
│  TOP BILLS (By Amount)                      │
│  1. INV-045 | Vehicle Parts  | LKR 1,800   │
│  2. INV-038 | Welfare        | LKR 1,500   │
│  3. INV-022 | Office Items   | LKR 1,200   │
│                                              │
│  ALERTS & WARNINGS                          │
│  ⚠️  3 bills exceed LKR 3,000 threshold     │
│  ✓ Monthly limit NOT exceeded               │
│  ✓ Single bill limits respected             │
│                                              │
│  Generated: 09-Feb-2026 at 14:30            │
│  By: Clerk1                                  │
│                                              │
│ [Print] [Save as PDF] [Email to Supervisor] │
└──────────────────────────────────────────────┘
```

**Step 4: Print the Report**
1. Click "[Print]" button
2. Preview appears
3. Click "Print" to print to physical printer
4. Or click "Save as PDF" to save for emailing

**⚠️ Important:** Supervisors must print, sign, and seal all monthly reports.

### High-Value Bills Report

Shows all bills above LKR 3,000 (policy threshold).

**When to Use:** Supervisor reviews to identify high-value purchases for verification.

**Report Contents:**
- Bill number, date, amount
- Category and description
- Who recorded the entry

### Category-Wise Analysis

Shows total spending by category for trend analysis.

**Useful For:**
- Identifying which areas spend most
- Budget planning for next month
- Spotting unusual spending patterns

---

## Common Tasks

### Task 1: Record Today's Purchases

1. Open application
2. Click "+ Add New Entry"
3. Select today's date (or date of purchase)
4. Enter bill details
5. Click "Save Entry"

### Task 2: Check Monthly Budget Status

1. Open dashboard
2. Look at "Remaining Budget" figure
3. If getting close to LKR 25,000, inform supervisor

**Example:**
- Total Spent: LKR 21,000
- Remaining: LKR 4,000 ← **Getting close!**

### Task 3: Prepare Month-End Report

**Due: Last day of month**

1. Click "Reports" → "Monthly Report"
2. Verify all entries are recorded for the month
3. Print the report
4. Give to supervisor for signing and sealing

### Task 4: Find a Specific Entry

1. Click "View Entries"
2. In search box, enter:
   - Bill number (e.g., "INV-001")
   - Or part of description (e.g., "Motor Oil")
3. Click "Search"
4. Results show matching entries

### Task 5: Correct a Mistake

1. Find the wrong entry in list
2. Double-click to edit
3. Change the incorrect field
4. Click "Update"

---

## Frequently Asked Questions (FAQ)

### Q1: Can I enter a bill from yesterday?
**A:** Yes! You can enter bills from any date in the past. The system allows retroactive entries.
- Open the entry form → Click date field → Select past date → Continue normally

### Q2: What if I reach the monthly limit?
**A:** The system will block any new entry that exceeds LKR 25,000 total for the month.
- **Message:** "Monthly limit exceeded. Remaining: LKR X"
- **Solution:** Wait for next month OR supervisor may approve exception

### Q3: What's the difference between a warning and an error?
| Warning (⚠️) | Error (✗) |
|---|---|
| Yellow symbol | Red symbol |
| Entry still saves | Entry BLOCKED |
| Supervisor sees flag | Cannot proceed |
| Example: High-value bill | Example: > 5,000 amount |

### Q4: Can I delete an entry?
**A:** Yes, but it stays in the audit log (cannot be permanently erased). This ensures a complete record for compliance.

### Q5: Forgot my password?
**A:** Contact your supervisor (ES Sir). They can reset it.

### Q6: Can multiple people use this at once?
**A:** Yes. The system supports multiple users logging in simultaneously, each with their own data entry view.

### Q7: Is my password secure?
**A:** Yes. Passwords are encrypted and never stored in plain text.

### Q8: What if the application crashes?
**A:** Your data is automatically saved. When you restart the application, all entries are still there.

### Q9: Do I need internet?
**A:** No. The application works fully offline. Internet is NOT required.

### Q10: Can I change the monthly limit?
**A:** Only supervisors/admins can change business rule settings. Regular users cannot modify limits.

---

## Troubleshooting

### Problem 1: Application Won't Start
**Symptoms:** Double-clicking the icon does nothing.

**Solution:**
1. Restart your computer
2. Try clicking the application again
3. If still not working → Contact IT support

### Problem 2: "Invalid Username or Password"
**Symptoms:** Login screen rejects your credentials.

**Solution:**
1. Check Caps Lock is OFF
2. Verify you typed username correctly
3. Verify password is correct (case-sensitive)
4. If forgotten → Contact supervisor to reset password

### Problem 3: "Monthly Limit Exceeded"
**Symptoms:** Cannot save a new entry.

**Solution:**
1. Check the "Remaining Budget" on dashboard
2. If LKR 0 remaining → Cannot add more entries this month
3. Inform supervisor
4. Wait for next month OR get supervisor approval for exception

### Problem 4: "Bill Number Already Exists"
**Symptoms:** Error when trying to save entry with duplicate bill number.

**Solution:**
1. You already entered this bill number this month
2. Check if it's a typo → Edit the bill number
3. Or, it was truly a duplicate → Use a different bill number
4. Or → Delete the old entry first, then re-enter

### Problem 5: "Database Connection Lost"
**Symptoms:** Error message about database connectivity.

**Solution:**
1. This usually means the database server crashed
2. Contact supervisor or IT support
3. They will restart the database service

### Problem 6: "Description too short"
**Symptoms:** Cannot save entry because description is < 10 characters.

**Solution:**
1. Click in the description field
2. Add more detail (need at least 10 characters total)
3. Example: "Oil" (3 chars) → "Motor Oil Filter" (17 chars) ✅

### Problem 7: Date is grayed out / Won't change
**Symptoms:** Cannot select a different date.

**Solution:**
1. Click the calendar icon next to the date field
2. A date picker will pop up
3. Select your desired date
4. Click "OK"

### Problem 8: Print button not working
**Symptoms:** Click [Print] but nothing happens.

**Solution:**
1. Make sure a printer is connected and turned on
2. Try "Save as PDF" instead (creates a PDF file)
3. You can print the PDF file later

---

## Support & Help

### For Technical Issues:
📧 **Email:** support@petty-cash.internal  
📞 **Phone:** Contact your supervisor

### For Password Resets:
👤 **Contact:** ES Sir (Supervisor) or Admin

### For Feature Questions:
📋 **Contact:** Your supervisor who trained you on the system

---

**End of User Manual**
