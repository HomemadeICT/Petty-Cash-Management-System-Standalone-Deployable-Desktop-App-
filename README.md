# 💰 Petty Cash Management System
### Ceylon Electricity Board — Haliela Branch

[![Build Status](https://github.com/HomemadeICT/Petty-Cash-Management--CEB-Haliela/actions/workflows/build.yml/badge.svg)](https://github.com/HomemadeICT/Petty-Cash-Management--CEB-Haliela/actions)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)
[![.NET 8.0](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![Language: VB.NET](https://img.shields.io/badge/Language-VB.NET-green.svg)](#)
[![SQLite](https://img.shields.io/badge/Database-SQLite-lightblue.svg)](#)
[![Status: Portfolio](https://img.shields.io/badge/Status-Portfolio%20Project-informational.svg)](#about-this-project)
[![CodeQL Analysis](https://github.com/HomemadeICT/Petty-Cash-Management--CEB-Haliela/actions/workflows/codeql-analysis.yml/badge.svg)](https://github.com/HomemadeICT/Petty-Cash-Management--CEB-Haliela/security/code-scanning)

> A desktop application built to manage petty cash expenses for CEB Haliela, featuring role-based access, business rule enforcement, reporting, and audit logging.

> **⚡ Built with AI Assistance** — This project was developed as a beginner programmer with pair programming guidance.
>
> **⚠️ Demo Credentials** — Default login is `admin` / `admin123` for testing. **Change these immediately in production.**

---

## 📸 Overview

| Feature | Description |
|---------|-------------|
| **Platform** | Windows Desktop (WinForms) |
| **Language** | VB.NET (.NET 8.0) |
| **Database** | SQLite (Zero-configuration, 100% Offline) |
| **Architecture** | 3-Tier (Presentation → Business Logic → Data Access) |
| **Auth** | Role-based with BCrypt password hashing |

---

## ✨ Features

### Core Functionality
- 📝 **Expense Management** — Add, edit, delete petty cash entries with full validation
- 📊 **Dashboard** — Monthly summary with progress bar, category breakdown, and data grid
- 📋 **Reporting** — Generate, print, and export monthly reports to Excel (XLSX)
- ⬇️ **Bulk Export** — Export multiple months to a single formatted spreadsheet
- 🗄️ **Database Backup** — One-click SQLite database backup

### Business Rules (BR1–BR9)
| Rule | What It Does | Limit |
|------|-------------|-------|
| BR1 | Monthly spending cap | ≤ LKR 25,000 |
| BR2 | Single bill maximum | ≤ LKR 5,000 |
| BR3 | Positive amounts only | > 0 |
| BR4 | High-value bill warning | ≥ LKR 3,000 |
| BR5 | Valid expense category | E5200, E5300, E7800, E7510 |
| BR6 | No duplicate bill numbers | Per month |
| BR7 | No future dates | ≤ Today |
| BR8 | Description minimum length | ≥ 10 characters |
| BR9 | Finalized months locked | Cannot edit |

### Security & Access Control

| Role | What They Can Do |
|------|-----------------|
| **Admin** | Full access — manage users, categories, settings, finalize reports |
| **Clerk** | Add/edit expenses, generate draft reports |
| **Viewer** | Read-only access to dashboard and reports |

### Additional Features
- 🔍 **Audit Trail** — Every action is logged with user, timestamp, and details
- 👥 **User Management** — Create, edit, deactivate accounts with granular permissions
- 🇱🇰 **Sinhala Month Names** — Configurable Sinhala language support for reports
- 🔐 **Permission System** — 22+ configurable permission keys per role

---

## 🏗️ System Architecture

```
┌─────────────────────────────────────────────────┐
│         PRESENTATION LAYER (WinForms)           │
│  LoginForm · DashboardForm · ExpenseEntryForm   │
│  ReportViewerForm · AdminSettingsForm · ...     │
└──────────────────────┬──────────────────────────┘
                       │
┌──────────────────────▼──────────────────────────┐
│       BUSINESS LOGIC LAYER (Services)           │
│  AuthService · ExpenseService · ReportService   │
│  ValidationService · AuditService · ...         │
└──────────────────────┬──────────────────────────┘
                       │
┌──────────────────────▼──────────────────────────┐
│     DATA ACCESS LAYER (Repositories)            │
│  UserRepo · ExpenseRepo · CategoryRepo          │
│  AuditLogRepo · PermissionRepo · DbContext      │
└──────────────────────┬──────────────────────────┘
                       │
               ┌───────▼───────┐
               │  SQLite DB    │
               │ PettyCash.db  │
               └───────────────┘
```

---

## 📁 Project Structure

```
├── PettyCash.DesktopApp/          ← Source Code
│   ├── Forms/                     ← 8 Windows Forms (UI)
│   ├── Services/                  ← 11 Business Logic Services
│   ├── Repositories/              ← 7 Data Access Classes
│   ├── Models/                    ← 7 Entity Models
│   ├── Utilities/                 ← 7 Helper Classes
│   ├── App.config                 ← Connection & Settings
│   └── Program.vb                 ← Application Entry Point
│
├── SQL/                           ← Database Schema
│   ├── schema_sqlite.sql          ← Current (SQLite)
│   └── schema_mysql.sql           ← Historical (MySQL/SQL Server)
│
├── docs/                          ← Full Documentation Suite
│   ├── user-manual.md
│   ├── administrator-guide.md
│   ├── technical-documentation.md
│   ├── database-documentation.md
│   ├── installation-guide.md
│   ├── troubleshooting-guide.md
│   └── ndict-assessment/          ← NVQ Level 5 Assessment
│
├── Assets/                        ← Branding, Icons
├── Installer/                     ← Inno Setup Scripts
│
├── DEVELOPMENT_JOURNEY.md         ← My Honest Story
├── CHANGELOG.md                   ← Version History
└── README.md                      ← You Are Here
```

---

## 🚀 Getting Started

### Option 1: Install from Installer
1. Run `PettyCashSetup.exe`
2. Follow the wizard
3. The database is created automatically on first launch

### Option 2: Build from Source
1. Open `Petty Cash Management System For CEB Haliela.sln` in Visual Studio 2022
2. Restore NuGet packages (`System.Data.SQLite`, `BCrypt.Net-Next`, `ClosedXML`)
3. Build → Build Solution (`Ctrl+Shift+B`)
4. Run (`F5`)

### Default Login
```
Username: admin
Password: admin123
```
⚠️ **Change the password immediately via Admin Settings!**

---

## 📖 Documentation

| Document | For Whom | Description |
|----------|----------|-------------|
| [Quick Start Guide](docs/quick-start-guide.md) | Everyone | 2-minute overview |
| [User Manual](docs/user-manual.md) | Office Staff | Step-by-step usage guide |
| [Administrator Guide](docs/administrator-guide.md) | Supervisors | User & system management |
| [Technical Documentation](docs/technical-documentation.md) | Developers | Architecture & code details |
| [Database Documentation](docs/database-documentation.md) | DBAs | Schema, queries, maintenance |
| [Installation Guide](docs/installation-guide.md) | IT Staff | Setup & deployment |
| [Troubleshooting Guide](docs/troubleshooting-guide.md) | Everyone | Common issues & fixes |
| [Development Journey](DEVELOPMENT_JOURNEY.md) | Assessors | My honest development story |
| [Architecture](ARCHITECTURE.md) | Developers | Detailed 3-tier design, data flow & patterns |

---

## 🧑‍💻 About This Project

This project was built as part of my **On-the-Job Training (OJT)** at Ceylon Electricity Board, Haliela Branch, and serves as my **NVQ Level 5 NDICT** course assessment project.

### Honest Disclosure
I am a **beginner programmer**. This project was built with significant help from **AI pair programming** (Google Gemini/Antigravity AI). The AI helped me:
- Design the 3-tier architecture
- Write VB.NET code I couldn't write on my own yet
- Debug deployment issues
- Migrate from SQL Server to SQLite when things broke

I document my full journey — including every struggle and failure — in [DEVELOPMENT_JOURNEY.md](DEVELOPMENT_JOURNEY.md).

### What I Learned
- How real-world software development works (it's messy!)
- The difference between "it works on my machine" and "it works everywhere"
- Why database choice matters for deployment
- How to use AI tools effectively as a learning partner
- That fixing deployment bugs can take longer than writing the code

---

## 🤝 Contributing & Community

This project welcomes **feedback, suggestions, and bug reports**, but is **not currently accepting pull requests** as it's a portfolio project.

### Getting Involved

- 🐛 **Found a Bug?** → [Open an Issue](../../issues) with a clear description
- 💡 **Have a Suggestion?** → [Discussions](../../discussions) or [Issues](../../issues)
- 📖 **Need Help?** → Check [Troubleshooting Guide](docs/troubleshooting-guide.md)
- 📬 **Give Feedback** → Use [Issues](../../issues) with the `feedback` label

### GitHub Best Practices

- **Commit Messages**: Use conventional format (`feat: add export`, `fix: validation bug`, `docs: update guide`)
- **Issue Labels**: Use provided labels (bug, enhancement, documentation, help-wanted)
- **Pull Requests**: Use provided templates in `.github/pull_request_template.md`
- **Code of Conduct**: See [CODE_OF_CONDUCT.md](CODE_OF_CONDUCT.md)
- **Security**: See [SECURITY.md](SECURITY.md) for vulnerability reporting

### CI/CD & Automation

- ✅ **Build & Test**: Automated on every push via GitHub Actions
- 🔍 **Code Analysis**: CodeQL security scanning enabled
- 📦 **Dependencies**: Automated dependency updates via Dependabot
- 🏷️ **Releases**: Follow [Release Process](.github/RELEASE.md)

---

## 🛠️ Tech Stack

| Component | Technology |
|-----------|-----------|
| Language | VB.NET |
| Framework | .NET 8.0 (Windows Forms) |
| Database | SQLite 3 |
| IDE | Visual Studio 2022 |
| Installer | Inno Setup |
| Password Hashing | BCrypt.Net-Next |
| Excel Export | ClosedXML |
| AI Assistant | Google Gemini / Antigravity |

---

## 📞 Contact

- **Developer:** Theekshana
- **Organization:** Ceylon Electricity Board — Haliela Branch
- **Supervisor:** ES Sir
- **Course:** NVQ Level 5 — NDICT (National Diploma in ICT)

---

**Last Updated:** March 23, 2026
