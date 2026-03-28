# 📄 NDICT Assessment: Project Final Report

**Student:** Theekshana  
**Project:** Petty Cash Management System (WinForms / VB.NET)  
**Organization:** Ceylon Electricity Board (Haliela Branch)  
**Course:** NVQ Level 5 — NDICT

---

## 1. Project Overview
This project aims to automate the manual petty cash management system at the Ceylon Electricity Board (CEB) Haliela branch. The system replaces manually maintained registers and spreadsheets with a structured, secure, and user-friendly desktop application.

### 1.1 Problem Statement
The manual system suffered from:
- Human errors in calculations.
- Difficulty in retrieving historical data.
- Security risks with shared spreadsheets.
- Lack of an audit trail for accounting.
- Complexity in enforcing budget rules (e.g., the LKR 25,000 monthly limit).

---

## 2. Requirement Analysis
Through meetings with office staff and the supervisor (ES Sir), the following requirements were gathered:

### 2.1 Functional Requirements
- Secure login for different roles (Admin, Clerk, Viewer).
- Standardized data entry for expenses (Date, Bill No, Category, Description, Amount).
- Enforcement of 8 specific business rules for audit compliance.
- Monthly and category-wise report generation.
- Bulk export functionality to Excel.
- Automated data backup mechanism.

### 2.2 Non-Functional Requirements
- **Simplicity:** Minimal installation and setup for office staff.
- **Offline Access:** Must work without an internet connection.
- **Reliability:** Data must be safe even during system restarts or crashes.
- **Security:** Password hashing and detailed audit logs.

---

## 3. Design & Architecture

### 3.1 3-Tier Architecture
The project was designed using the **3-Tier Architecture** pattern to ensure modularity and ease of maintenance:
1. **Presentation Layer:** WinForms UI for interaction.
2. **Service Layer:** Business logic and rule enforcement.
3. **Data Access Layer:** SQLite communication through the Repository pattern.

### 3.2 Database Design
A relational schema was designed with specialized tables for users, expenses, categories, permissions, and audit logs. The choice of **SQLite** ensures zero-configuration deployment on client workstations.

---

## 4. Implementation Highlights

### 4.1 Business Rule Engine
A centralized `ValidationService` was implemented to verify every transaction against 9 organizational rules (BR1–BR9) before it is committed to the database.

| Rule | Description | Limit |
|------|------------|-------|
| BR1 | Monthly spending cap | ≤ LKR 25,000 |
| BR2 | Single bill maximum | ≤ LKR 5,000 |
| BR3 | Positive amounts only | > 0 |
| BR4 | High-value bill warning | ≥ LKR 3,000 |
| BR5 | Valid expense category | E5200, E5300, E7800, E7510 |
| BR6 | No duplicate bill numbers | Per month |
| BR7 | No future dates | ≤ Today |
| BR8 | Description minimum length | ≥ 10 characters |
| BR9 | Finalized months locked | Cannot edit |

### 4.2 SQLite Migration
A critical implementation choice was the migration from SQL Server to SQLite during the deployment phase. This addressed compatibility issues with SQL Server installation on varied branch PCs, resulting in a 100% reliable "one-click" setup experience. The `DbContext` class was rewritten to use `System.Data.SQLite`, and schema initialization was embedded into the application startup routine.

### 4.3 Production Deployment Debugging
A significant post-launch issue was identified: all CRUD operations failed silently after installation on a target PC. Investigation revealed two root causes:
1. **.NET 8 config resolution:** Self-contained .NET 8 executables look for `{App}.exe.config` but MSBuild outputs `{App}.dll.config`. A custom MSBuild post-publish target was added to the `.vbproj` to automatically copy the file with the correct extension upon publish.
2. **Schema initialization order:** On a clean install, the database schema was not being initialized before the application attempted its first query. The `Program.vb` startup logic was hardened to always call `DbContext.InitializeSchema()` as the first operation, ensuring correct database state regardless of deployment environment.

These fixes demonstrate applied professional troubleshooting skills within a real production constraint.

### 4.4 Role-Based Permission System
A granular permission system was implemented with 22 configurable permission keys per user. Permissions are stored in the `user_permissions` table and checked at runtime via `PermissionService`, giving administrators fine-grained control beyond simple role assignment.

---

## 5. Testing & Quality Assurance
The system underwent several phases of testing:
- **Unit Testing:** Verified services like the password hasher (`BCrypt`), budget calculator, and validation service logic.
- **Integration Testing:** End-to-end flow testing of the expense entry → validation → repository → audit log pipeline.
- **UAT (User Acceptance Testing):** Conducted at CEB Haliela to ensure the UI met the actual workflow of office staff.
- **Regression Testing:** Performed after the SQLite database migration to verify no data loss or behavioral changes occurred.
- **Deployment Testing:** Verified the installer on a clean Windows machine with no pre-installed dependencies, addressing the critical config resolution bug described in Section 4.3.

---

## 6. Self-Evaluation & Reflection
As a beginner developer, this project was a significant learning curve. Key takeaways include:
- **Choosing the right stack matters:** SQL Server was the "professional" choice but SQLite was the right choice for deployment in a branch office setting.
- **Deployment is a first-class concern:** More time was spent debugging the deployment than writing features. Config file resolution, self-contained publishing, and schema initialization are all deployment engineering challenges.
- **AI-pair programming accelerates learning:** Using AI as a pair programmer allowed me to tackle architecture patterns (3-Tier, Repository), security concerns (BCrypt), and complex library usage (ClosedXML) that would have taken weeks to learn independently.
- **Audit trails and validation build trust:** The business was reluctant to adopt the system until they saw that every action was logged and every rule was enforced automatically.

### Conclusion
The Petty Cash Management System successfully automates the manual workflow at CEB Haliela, providing a professional solution that meets all NDICT Level 5 assessment criteria. The project demonstrates practical competency in software engineering lifecycle management, database design, secure application development, and real-world deployment troubleshooting.

