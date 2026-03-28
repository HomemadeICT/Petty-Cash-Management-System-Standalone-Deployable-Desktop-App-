# 🤝 Contributing to Petty Cash Management System

Thank you for your interest in contributing! As this is a portfolio project and personal assessment piece, I am not currently accepting pull requests, but I am open to feedback and suggestions.

---

## 🛠️ Development Setup

1. **Clone the Repo:**
   ```bash
   git clone https://github.com/HomemadeICT/Petty-Cash-Management--CEB-Haliela.git
   ```

2. **Prerequisites:**
   - Visual Studio 2022
   - .NET 8.0 SDK

3. **Open Solution:**
   Open `Petty Cash Management System For CEB Haliela.sln`.

4. **NuGet Packages:**
   Packages are restored automatically via `dotnet restore`:
   - `System.Data.SQLite`
   - `BCrypt.Net-Next`
   - `ClosedXML`
   - `System.Configuration.ConfigurationManager`

5. **Architecture Reference:**
   Read [`ARCHITECTURE.md`](ARCHITECTURE.md) for a full explanation of the 3-tier design and data flow.

---

## 📜 Coding Standards

- **Language:** VB.NET
- **Pattern:** 3-Tier Architecture (Forms → Services → Repositories).
- **Naming:** PascalCase for Classes and Public Methods, camelCase with underscore (`_variable`) for private fields.
- **Documentation:** Use XML comments for complex methods.

---

## 🐞 Bug Reports

If you find a bug, please open an **Issue** with:
- A clear description of the problem.
- Steps to reproduce the error.
- Any relevant logs or screenshots.

---

## 💡 Future Enhancements

I am planning the following features for future versions:
- [ ] WhatsApp notifications for supervisors.
- [ ] PDF export support.
- [ ] Dashboard charts for spending trends.
- [ ] Receipt image scanning.

---

**Theekshana**  
OJT Developer, CEB Haliela
