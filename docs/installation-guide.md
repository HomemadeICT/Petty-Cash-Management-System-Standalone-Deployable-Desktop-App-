# 🚀 Installation & Setup Guide

This guide explains how to install and setup the **Petty Cash Management System** for the first time. Thanks to the **SQLite migration**, setup is now a "one-click" experience.

---

## 💻 System Requirements

### Operating System
- Windows 10 or Windows 11 (64-bit recommended)

### Software Requirements
- **.NET 8.0 Runtime** (The installer will prompt if missing)
- **Database:** No installation required (SQLite is self-contained)

---

## 🛠️ Step 1: Running the Installer

1. Locate the `PettyCashSetup.exe` file.
2. Double-click to run the setup wizard.
3. Follow the on-screen instructions to choose the installation folder.
4. Click **Install**.
5. Once finished, check **Launch Petty Cash Management System** and click **Finish**.

---

## 📋 Step 2: First-Time Login

When the application launches for the first time:
1. It will automatically create a new database file (`PettyCash.db`) if it doesn't exist.
2. It will seed the default administrator account.
3. Use the following credentials to log in:
   - **Username:** `admin`
   - **Password:** `admin123`

> [!IMPORTANT]
> Change the default password immediately after logging in via the **Settings** menu.

---

## 💾 Step 3: Database & Backups

### Database Location
The database is a single file named `PettyCash.db`. By default, it is stored in the application's data directory (usually `%AppData%\PettyCashManagement`).

### Taking Backups
To backup your data:
1. Log in as an **Admin**.
2. Click the orange **🗄 Backup DB** button on the Dashboard.
3. Select a safe folder (e.g., an external drive or cloud folder).
4. Click **Backup Now**.

---

## 📁 Developing from Source

If you want to build the project from source code:

1. **Clone the repository:**
   `git clone https://github.com/yourusername/PettyCashProject.git`

2. **Open in Visual Studio:**
   Open `Petty Cash Management System For CEB Haliela.sln`.

3. **Restore Packages:**
   The project uses NuGet for **System.Data.SQLite**, **BCrypt.Net-Next**, and **ClosedXML**.

4. **Build & Run:**
   Press **F5** or click **Start**.

---

## ❓ Troubleshooting

### Application fails to start
- **Cause:** Missing .NET 8.0 Runtime.
- **Solution:** Download and install the [.NET 8.0 Desktop Runtime](https://dotnet.microsoft.com/download/dotnet/8.0).

### "Database is locked" error
- **Cause:** Another program or another instance of the app is using the database.
- **Solution:** Close all instances of the application and try again.

---

**Last Updated:** March 5, 2026
