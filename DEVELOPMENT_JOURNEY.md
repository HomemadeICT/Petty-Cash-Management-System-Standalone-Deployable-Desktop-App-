# 🚀 Development Journey: An Honest Beginner's Story

This document tells the real story of how this project came to be. It's not a story of a seasoned expert, but a story of a **beginner developer** learning to build real-world software with the help of **AI pair programming**.

---

## 📅 Timeline & Evolution

### Phase 1: The First Step (Feb 9, 2026)
I started with an ambitious goal but limited VB.NET experience. Working with **Antigravity AI**, we designed a **3-tier architecture** and built the first version using **SQL Server Express**. 
- **The High:** Seeing the login screen work for the first time!
- **The Low:** Realizing how complicated setting up SQL Server on other machines would be.

### Phase 2: Feature Frenzy (Mid Feb)
Once the core was working, I wanted to make the app actually useful for the Haliela branch. We added:
- **Backup System:** Because I was terrified of losing data.
- **Excel Export:** So my supervisor could use the data in his own spreadsheets.
- **Sinhala Support:** Essential for our local office needs.
- **Bulk Export:** A major accomplishment that allows exporting a whole year of data at once.

### Phase 3: The "Installer Hell" (Late Feb – Early March)
This was my biggest struggle. I tried to build an installer, but SQL Server was a nightmare to deploy. 
- **The Struggle:** I spent days debugging connection strings, firewall rules, and "Server not found" errors on different PCs.
- **The Lesson:** "It works on my machine" is the most dangerous phrase in programming.

### Phase 4: The Great Migration (March 5, 2026)
After hitting a wall with SQL Server deployment, I made a bold choice with AI guidance: **Move to SQLite**.
- **The Change:** We stripped out the SQL Server dependencies and replaced them with SQLite.
- **The Result:** No more database installation! The app just works as soon as you run it. It was a massive technical debt cleanup.

---

## 🧑‍💻 Learning with AI

I want to be 100% honest: **I didn't write every line of this code alone.** I used AI as my mentor and partner.

### How I used AI:
1. **Architecture Design:** I told the AI the business needs, and it explained why a 3-tier structure (Forms/Services/Repos) was best.
2. **Writing Code:** For complex things like XLSX generation (ClosedXML) or SQLite connection management, the AI provided the boilerplate and explained it to me.
3. **Debugging:** When the installer failed, the AI helped me read logs and understand *why* it was failing.
4. **Learning by Doing:** Instead of just copying, I asked the AI to explain the VB.NET syntax and the "why" behind patterns like the Repository pattern.

### My Actual Skills:
- I understand the **logic** of the system.
- I can navigate the **project structure** proficiently.
- I know how to **modify UI forms** and connect them to services.
- I am still learning the deep details of **asynchronous programming** and **complex SQL optimization**.

---

## 💡 Lessons Learned
- **Deployment is hard:** Coding is only 50% of the job; making it work on someone else's computer is the other 50%.
- **Keep it Simple:** Moving to SQLite taught me that sometimes the "advanced" solution (SQL Server) is overkill for a local office app.
- **AI is a Force Multiplier:** As a beginner, I could never have built a system this robust without AI assistance. It allowed me to focus on the problem-solving while the AI handled the boilerplate.

---

### Phase 5: "It Works on My Machine" — Again (March 23, 2026)
Just when I thought the battle was won, we deployed the installer to a colleague's PC and **everything broke**. Login worked, but every single CRUD operation failed silently.

- **The Problem:** After a full publish and install, the `.exe` couldn't find its connection string. The `.dll.config` file was there, but .NET 8 self-contained apps look for `.exe.config`. The config was invisible to the running app.
- **The Second Problem:** Even after fixing the config copy in the installer, the database schema wasn't being initialized on a brand-new machine because `Program.vb` wasn't calling `DbContext.InitializeSchema()` at startup reliably.
- **The Fix:** Added a post-publish MSBuild target in the `.vbproj` to copy `{AssemblyName}.dll.config → {AssemblyName}.exe.config` automatically. Also hardened `Program.vb` to always call `InitializeSchema()` before opening any form, and added a diagnostic startup log file to `AppData` to catch future silent failures.
- **The Lesson:** Publishing a .NET 8 self-contained WinForms app is a very different beast from running it in Visual Studio. The config system, paths, and permissions all behave differently. **Test on a clean VM, not just your dev machine.**

---

## 🏆 Final Thoughts
This project represents my transition from "learning to code" to "solving real business problems." It’s messy, it was a struggle, and it’s not perfect—but it **works**, and it's mine.

**Theekshana**  
OJT Developer, CEB Haliela
