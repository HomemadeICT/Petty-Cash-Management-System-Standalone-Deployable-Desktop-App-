# 🔨 Installer Build Instructions (SQLite Edition)

> **No SQL Server. No .NET Runtime. ~50 MB single installer.**

---

## Step 1 — Publish the Application

Open **PowerShell** in the **project root** folder and run:

```powershell
dotnet publish "Petty Cash Management System For CEB Haliela.vbproj" `
  -c Release `
  -r win-x64 `
  --self-contained true `
  -o "Installer\Published\PettyCash_SC"
```

This produces a **self-contained** bundle — the target PC needs no .NET installed.

✅ When done you should see:
```
Installer\Published\PettyCash_SC\
  ├── Petty Cash Management System For CEB Haliela.exe
  ├── Petty Cash Management System For CEB Haliela.dll.config
  ├── x64\
  │   └── SQLite.Interop.dll   ← CRITICAL — must be here
  └── ... (300+ runtime files)
```

---

## Step 2 — (Optional) Add Sinhala Font

If you want Iskoola Pota font installed:
- Obtain `Iskoola Pota Regular.ttf` (Windows built-in font or download)
- Copy to `Installer\Prerequisites\Iskoola Pota Regular.ttf`

If you skip this the installer will compile fine — the font line is conditional.

---

## Step 3 — Compile the Installer

**Install Inno Setup 6** if not already installed:  
👉 https://jrsoftware.org/isdl.php

Then either:

**Option A — GUI:**
1. Open Inno Setup Compiler
2. File → Open → `Installer\Setup.iss`
3. Build → Compile (`Ctrl+F9`)

**Option B — Command Line:**
```powershell
& "C:\Program Files (x86)\Inno Setup 6\ISCC.exe" "Installer\Setup.iss"
```

---

## Step 4 — Get Your Installer

The final `.exe` is created at:

```
Installer\Output\PettyCashSetup_v1.2.1.exe
```

**This is the file you distribute.** Copy it to a USB drive, shared folder, or email it.

---

## Step 5 — Test Before Distributing

Test on a **clean Windows PC** (one without Visual Studio / .NET SDK):

1. Run `PettyCashSetup_v1.2.1.exe` as Administrator
2. Follow the wizard (takes ~30 seconds)
3. Launch the app from the Start Menu or Desktop
4. Login: `admin` / `admin123`
5. Add a test expense → Edit it → Delete it (confirms all CRUD works)
6. Check `C:\ProgramData\PettyCashManagement\PettyCash.db` was created

---

## Troubleshooting

| Problem | Fix |
|---------|-----|
| "File not found" on compile | Make sure Step 1 (publish) was completed first |
| "unable to load SQLite.Interop.dll" | The `x64\SQLite.Interop.dll` is missing from publish output — re-run publish |
| Login works but CRUD fails | The `.exe.config` copy step in installer failed — re-install as Administrator |
| Installer won't compile | Ensure Inno Setup 6 is installed (`%ProgramFiles(x86)%\Inno Setup 6\ISCC.exe`) |

---

## Version History

| Version | Date | Notes |
|---------|------|-------|
| 1.2.1 | 2026-03-23 | Current. Deployment fixes, SQLite-only. |
| 1.0.0 | 2026-02-09 | Initial release |
