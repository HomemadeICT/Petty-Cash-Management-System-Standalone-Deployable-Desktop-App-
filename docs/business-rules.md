# MVP Scope & Business Rules

**Project:** Petty Cash Management System  
**Version:** 1.2.0  
**Last Updated:** March 5, 2026

---

## MVP Objectives

1. **Track Petty Cash** - Record all expenses with categories
2. **Enforce Limits** - Validate against business rules
3. **Generate Reports** - Monthly printable reports
4. **Audit Trail** - Log all actions for accountability
5. **Local Deployment** - Zero-configuration SQLite setup

---

## Success Criteria

| Criteria | Target |
|----------|--------|
| Expense entry time | < 1 minute |
| Monthly report generation | < 5 seconds |
| System uptime | 99% (working hours) |
| User adoption | 100% (2 users) |

---

## In Scope (MVP)

### Core Features
- [x] User authentication (Admin, Clerk roles)
- [x] Expense entry with validation
- [x] Monthly report generation and printing
- [x] Audit logging for all actions
- [x] Dashboard with monthly overview

### Technical
- [x] SQLite database
- [x] 3-tier architecture
- [x] Local offline deployment (No LAN required)
- [x] Inno Setup installer

---

## Out of Scope (Future)

### Phase 2
- [ ] WhatsApp notifications
- [ ] Multi-year comparison reports
- [ ] Receipt photo attachments
- [x] Document export (Excel XLSX)
- [ ] Document export (PDF)
- [ ] Dashboard analytics/charts

### Phase 3
- [ ] Supplier management
- [ ] Budget planning
- [ ] Approval workflows
- [ ] Mobile companion app
- [ ] Cloud backup

---

## Business Rules

### BR1: Monthly Limit
**Rule:** Total expenses per month ≤ LKR 25,000  
**Type:** Hard limit (blocks entry)  
**Message:** "Monthly limit of LKR 25,000 exceeded"

### BR2: Single Bill Limit
**Rule:** Individual bill ≤ LKR 5,000  
**Type:** Hard limit  
**Message:** "Bill exceeds maximum of LKR 5,000"

### BR3: Positive Amount
**Rule:** Amount must be > 0  
**Type:** Hard limit  
**Message:** "Amount must be greater than zero"

### BR4: High-Value Warning
**Rule:** Warning on 2nd+ bill ≥ LKR 3,000/month  
**Type:** Soft warning (allows entry)  
**Message:** "Multiple high-value bills this month"

### BR5: Valid Category
**Rule:** Category must be E5200, E5300, E7800, or E7510  
**Type:** Hard limit  
**Message:** "Invalid category code"

### BR6: Unique Bill Number
**Rule:** Bill number unique per month  
**Type:** Hard limit  
**Message:** "Bill number already exists this month"

### BR7: No Future Dates
**Rule:** Entry date ≤ today  
**Type:** Hard limit  
**Message:** "Cannot enter future dates"

### BR8: Description Length
**Rule:** Description ≥ 10 characters  
**Type:** Hard limit  
**Message:** "Description must be at least 10 characters"

### BR9: Finalized Month Lock
**Rule:** No edits to finalized months  
**Type:** Hard limit  
**Message:** "This month has been finalized"

---

## Assumptions

1. Maximum 2 concurrent users
2. SQLite sufficient for data volume
3. LAN always available during working hours
4. Single reporting officer workflow
5. Categories are fixed (no dynamic additions in MVP)

---

## Constraints

1. Budget: Minimal (using free tools)
2. Timeline: 8 weeks total
3. Users: 2 (Clerk + ES Sir)
4. Hardware: 2 existing PCs on LAN
5. OS: Windows 10/11 only

---

## Risks

| Risk | Mitigation |
|------|------------|
| LAN failure | Fall back to local entry, sync later |
| Database issues | Daily automated backups |
| User resistance | Involve users in testing |
| Scope creep | Strict MVP boundary |

---

**Next Steps:**
1. Create WinForms UI manually
2. Connect forms to existing services
3. Test end-to-end workflow
4. Deploy on LAN
