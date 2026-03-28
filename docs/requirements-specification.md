# Requirements Specification

**Document Version:** 1.2.0  
**Last Updated:** March 5, 2026  
**Status:** Approved

---

## Functional Requirements

### FR1: Expense Entry

**Priority:** HIGH  
**User Role:** Clerk

#### FR1.1: Record Expense Details
The system shall allow users to enter the following information for each expense:

| Field | Type | Required | Validation |
|-------|------|----------|------------|
| **Date** | Date | Yes | Cannot be future date, within current month |
| **Bill Number** | Text (50 chars) | Yes | Unique within month |
| **Amount** | Decimal (10,2) | Yes | > 0, ≤ 5000 |
| **Category Code** | Dropdown | Yes | E5200, E5300, or E7800 |
| **Description** | Text (500 chars) | Yes | Min 10 characters |

#### FR1.2: Real-Time Validation
- System shall validate bill amount ≤ LKR 5,000 before saving
- System shall check monthly total will not exceed LKR 25,000
- System shall display error messages for validation failures
- System shall prevent duplicate bill numbers within same month

#### FR1.3: Save and Edit
- System shall save valid expenses to database immediately
- System shall allow editing of expenses within current month only
- System shall log all edits with timestamp and user

---

### FR2: Calculations

**Priority:** HIGH  
**User Role:** System (Automatic)

#### FR2.1: Category-Wise Totals
System shall automatically calculate subtotals for each category:
- Total for E5200 (Vehicle parts)
- Total for E5300 (Office items)
- Total for E7800 (Physical Hardware stuff for workplace
E 7510 - Treatments and staff

#### FR2.2: Monthly Totals
System shall calculate:
- **Grand Total:** Sum of all expenses in current month
- **Remaining Balance:** 25,000 - Grand Total
- **Number of Bills:** Count of expense entries

#### FR2.3: Real-Time Updates
- Calculations shall update immediately after any expense entry/edit
- Updates shall be visible to all connected users within 2 seconds

---

### FR3: Validation & Warnings

**Priority:** HIGH  
**User Role:** System (Automatic)

#### FR3.1: Hard Limits (Blocking)

| Rule | Condition | Action |
|------|-----------|--------|
| **Single Bill Limit** | Amount > LKR 5,000 | Block entry, display error |
| **Monthly Limit** | Total + New Amount > LKR 25,000 | Block entry, display remaining balance |

#### FR3.2: Soft Warnings (Non-Blocking)

| Rule | Condition | Action |
|------|-----------|--------|
| **High-Value Bill Warning** | 2nd+ bill between LKR 3,000-5,000 | Show warning, allow entry, notify supervisor |

---

### FR4: Monthly Report Generation

**Priority:** HIGH  
**User Role:** Clerk, ES Sir

#### FR4.1: Generate Draft Report
- System shall generate monthly report for selected month
- Report shall include:
  - Organization header (CEB - Haliela)
  - Month and year
  - Table of all expenses (Date, Bill No, Category, Description, Amount)
  - Category-wise subtotals
  - Grand total
  - Remaining balance
  - Footer with signature lines (Prepared by, Checked by, Date, Official Seal)

#### FR4.2: Edit Draft
- Clerk shall be able to edit draft before finalization
- Edits shall be limited to current month only

#### FR4.3: Print Report
- System shall provide print-friendly formatting
- Printed report shall match official manual format
- Report shall fit on A4 paper
- Headers and footers shall appear on every page for multi-page reports

#### FR4.4: Finalize Report
- ES Sir shall be able to mark report as "Finalized"
- Finalized reports shall be locked from editing
- System shall record finalization timestamp and user

---

### FR5: Notification System

**Priority:** MEDIUM  
**User Role:** System (Automatic)

#### FR5.1: Notification Channels
System shall support:
- **Email notifications**
- **WhatsApp notifications** (future enhancement)

#### FR5.2: Notification Preferences
- ES Sir shall be able to configure notification preferences
- Options: Email only, WhatsApp only, Both, None
- Preferences shall be editable at any time

#### FR5.3: Overuse Notifications
System shall send immediate notification when:
- Monthly allowance exceeded
- Single bill limit violation attempted
- 2nd+ bill in LKR 3,000-5,000 range entered

Notification shall include:
- Event type
- Month
- Bill number (if applicable)
- Amount
- Current total
- Remaining balance

#### FR5.4: Monthly Report Notifications
- System shall send notification on month-end (configurable date)
- Notification shall include:
  - Total monthly expenditure
  - Category-wise totals
  - Remaining balance
  - Link to view/download report (desktop: report ID to open)

---

### FR6: User Management

**Priority:** HIGH  
**User Role:** ES Sir (Admin)

#### FR6.1: User Roles
System shall support two user roles:

| Role | Permissions |
|------|-------------|
| **Admin** | Full access: configure settings, manage users, finalize reports, delete entries |
| **Clerk** | Limited access: entry, edit own entries, generate drafts |

#### FR6.2: Authentication
- System shall require username and password login
- Passwords shall be hashed (bcrypt or similar)
- Failed login attempts shall be logged
- Session timeout after 30 minutes of inactivity (desktop: configurable)

#### FR6.3: First-Time Setup
- On first run (Server installation), system shall create default admin user
- Admin shall set password on first login
- Admin shall create clerk user account

---

### FR7: Audit Trail

**Priority:** MEDIUM  
**User Role:** ES Sir (Admin)

#### FR7.1: Log Events
System shall log:
- All expense entries (create, edit, delete)
- Report generation and finalization
- User logins
- Setting changes
- Notification delivery



#### FR7.2: View Audit Log
- Admin shall be able to view complete audit trail
- Logs shall be searchable by date range, user, action type
- Logs shall be exportable (future enhancement)

---

## Non-Functional Requirements

### NFR1: Usability

#### NFR1.1: Ease of Use
- Clerk shall be able to enter expense in ≤ 30 seconds
- ES Sir shall generate monthly report in ≤ 2 minutes
- UI shall use clear labels and validation messages

#### NFR1.2: Training
- New users shall become proficient in ≤ 1 hour of training
- System shall include help text/tooltips for complex fields

### NFR2: Performance

#### NFR2.1: Response Time
- Expense entry save: ≤ 3 second
- Report generation: ≤ 5 seconds for 100 entries
- Calculation updates: ≤ 2000ms

#### NFR2.2: Capacity
- System shall handle ≥ 500 expense entries per month
- System shall store ≥ 5 years of historical data

### NFR3: Reliability



#### NFR3.1: Data Integrity
- No data loss on power failure (SQLite transaction integrity)
- Daily automated backups
- Backup retention: 30 days minimum

### NFR4: Security

#### NFR4.1: Authentication
- password requirements (simple is fine because this is a mini project)
- Password change capability
- Admin account cannot be deleted

#### NFR4.2: Authorization
- Role-based access control strictly enforced
- Clerk cannot access admin functions
- Audit log cannot be edited by any user

#### NFR4.3: Data Protection
- Database connections internal (SQLite file-based)
- Passwords hashed (never stored plain text)
- Sensitive data (amounts) not exposed in logs

### NFR5: Maintainability

#### NFR5.1: Code Quality
- Well-commented code
- Modular design (3-tier architecture for VB.NET)
- Consistent naming conventions

#### NFR5.2: Documentation
- Complete system documentation (this doc set)
- Inline code comments
- Database schema documentation

### NFR6: Portability (VB.NET Desktop)

#### NFR6.1: Windows Compatibility
- Support Windows 10 and Windows 11
- Support both 64-bit architectures

#### NFR6.2: Installation
- Single installer EXE
- No manual configuration required
- Embedded SQLite (no server required)

---

## User Stories

### Clerk Perspective

**US1:** As a clerk, I want to enter expenses quickly so that I can maintain records without disrupting my work.

**Acceptance Criteria:**
- Entry form opens in ≤ 2 second
- Tab order logical (Date → Bill No → Amount → Category → Description)
- Dropdown for category (no typing)
- Save button clearly visible

---

**US2:** As a clerk, I want the system to stop me if I exceed limits so that I avoid policy violations.

**Acceptance Criteria:**
- Error message appears immediately on validation failure
- Message clearly states the limit and current value
- Entry cannot be saved until corrected

---

**US3:** As a clerk, I want to generate monthly reports easily so that ES Sir can review financial status.

**Acceptance Criteria:**
- One-click report generation
- Preview before printing
- Print directly from application

---

### ES Sir Perspective

**US4:** As ES Sir, I want to receive automatic notifications when limits are approached so that I can take timely action.

**Acceptance Criteria:**
- Notification arrives within 5 minutes of event
- Message is clear and non-technical
- Includes actionable information (amounts, totals)

---

**US5:** As ES Sir, I want to print professional-looking reports so that I can submit them to finance department with my signature and seal.

**Acceptance Criteria:**
- Report format matches current manual format
- Prints correctly on standard A4 paper
- Includes all required headers and footers
- Signature and seal areas clearly marked

---

**US6:** As ES Sir, I want to configure notification preferences so that I choose how I receive alerts.

**Acceptance Criteria:**
- Settings page accessible from admin menu
- Options: Email, WhatsApp, Both, None
- Changes take effect immediately
- Test notification button available

---

## Assumptions

1. **Network Availability:** Office LAN is stable and available during work hours
2. **Single Currency:** All transactions in LKR
3. **Month Definition:** Calendar month (1st to last day)
4. **Category Codes:** Fixed set, no custom codes needed
5. **Printer Access:** Standard office printers compatible with Windows printing
6. **Database Hosting:** Clerk PC remains on during work hours

---

## Out of Scope

The following features are explicitly **NOT** included in this version:

- ❌ Mobile application
- ❌ Web browser access
- ❌ Multiple currency support
- ❌ Custom category codes
- ❌ Integration with accounting systems
- ❌ SMS notifications
- ❌ Receipt/invoice image uploading
- ❌ Multi-location support
- ❌ Advanced reporting (charts, graphs)
- ✅ Export to Excel (XLSX) — **Implemented in v1.1.0**

---

