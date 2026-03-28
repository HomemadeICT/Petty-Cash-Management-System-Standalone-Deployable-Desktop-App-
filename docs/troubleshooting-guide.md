# Petty Cash Management System - Troubleshooting Guide

**Application Version:** 1.0  
**Last Updated:** February 9, 2026  
**Target Audience:** End Users, Support Staff, Administrators

---

## 📋 Table of Contents

1. [General Issues](#general-issues)
2. [Login Problems](#login-problems)
3. [Data Entry Issues](#data-entry-issues)
4. [Report Problems](#report-problems)
5. [Database Issues](#database-issues)
6. [Performance Issues](#performance-issues)
7. [Error Messages Reference](#error-messages-reference)
8. [When to Contact IT](#when-to-contact-it)

---

## General Issues

### Issue: Application Takes Long Time to Load

**Symptoms:** Application window opens but stays blank for 30+ seconds

**Quick Fix:**
1. Wait up to 1 minute for full load
2. If still not responding → Close and restart

**If Still Happening:**
1. Check system RAM usage:
   - Open Task Manager (Ctrl+Shift+Esc)
   - Click "Performance" tab
   - If Memory > 90% → Close other applications
2. Check disk space:
   - Right-click C:\ drive → Properties
   - If free space < 1 GB → Delete temporary files
3. Restart computer
4. Restart application

### Issue: "Application Crashed" / Forcing Close

**Symptoms:** Application suddenly closes without warning

**First Step:** Restart application
- Usually works after single restart

**If Happens Repeatedly:**
1. Clear temporary application data:
   - Close application
   - Delete: `C:\Users\YourName\AppData\Local\PettyCash\*`
   - Restart application

2. Repair installation:
   - Control Panel → Programs → Uninstall
   - Find "Petty Cash Manager"
   - Click "Repair"
   - Restart computer

3. Contact IT if problem persists

### Issue: Application Does Not Close Properly

**Symptoms:** Click "Exit" or "Logout" but application stays running

**Solution:**
1. Use Task Manager to force close:
   - Press Ctrl+Shift+Esc
   - Find "PettyCash.exe"
   - Right-click → "End Task"
2. Restart application
3. Try exiting again (should work now)

### Issue: Forms Look Weird / Buttons Cut Off

**Symptoms:** Dialog boxes don't fit on screen, buttons are partially hidden

**Solution:**
1. **Adjust Display Scaling:**
   - Right-click desktop → Display Settings
   - Look for "Scale and Layout"
   - If set to 125% or 150% → Change to 100%
   - Sign out and sign back in to apply

2. **Change Screen Resolution:**
   - Right-click desktop → Display Settings
   - Resolution: Set to 1920x1080 or higher
   - Apply and test

---

## Login Problems

### Problem: "Invalid Username or Password"

**Symptoms:** Cannot log in with correct credentials

**Most Common Cause:** Caps Lock is ON

**Solution:**
1. Turn OFF Caps Lock (Press Caps Lock key)
2. Try logging in again
3. If still fails → Check that username is correct:
   - Usernames are case-sensitive: `clerk1` ≠ `Clerk1`
   - Try typing username in lowercase

**If Still Failing:**
1. Try a different user account (if available)
   - This helps determine if problem is your account vs. system
2. If other user can login → Your password is wrong or account is disabled
3. Contact your supervisor to reset password

### Problem: "Account Disabled" / "User Not Active"

**Symptoms:** Message says account is inactive

**Cause:** Administrator deactivated your account

**Solution:**
- Contact supervisor (ES Sir)
- Ask them to reactivate your account
- Or reset password

### Problem: "Login Timeout" / "Took too long to log in"

**Symptoms:** Login form hangs, then shows timeout error

**Cause:** Database connection issue

**Solution:**
1. Check network connection:
   - Is computer connected to network? (Check WiFi/LAN)
   - Can you ping the database server?
   - ```
     Open Command Prompt:
     ping database_server_ip
     Should see replies
     ```

2. If network is OK → Database server might be down:
   - Try again in 5 minutes
   - If persists → Contact IT

3. Try using server IP instead of hostname:
   - Ask IT for server IP address
   - In connection string use IP instead of name

### Problem: "Database Connection Failed" at Login

**Symptoms:** Error message: "Cannot connect to database server"

**Causes & Solutions:**

**Cause 1: Server is off**
- Solution: Contact IT to start database server

**Cause 2: Firewall blocking**
- Solution: Contact IT to verify firewall rules

**Cause 3: Wrong server address**
- Solution: Check with IT for correct server address/IP

**Cause 4: Network down**
- Solution: Check network connection
  - Can you access other network resources?
  - Restart WiFi/LAN connection
  - Restart computer

---

## Data Entry Issues

### Problem: Cannot Save Entry - "Monthly Limit Exceeded"

**Symptoms:** Error: "Monthly limit exceeded. Remaining: LKR X"

**What It Means:** You've already spent LKR 25,000 this month

**Solution:**
1. Check remaining budget:
   - Look at dashboard → "Remaining Budget"
   - If 0 or negative → Cannot enter more bills this month

2. Options:
   - **Option A:** Wait until next month
   - **Option B:** Ask supervisor for exception
   - **Option C:** Report entry is for next month (if date allows)

**How to Avoid:**
- Before entering large bills, check remaining budget
- If getting close (e.g., < LKR 3,000 left), inform supervisor

### Problem: Cannot Save Entry - "Single Bill Limit (LKR 5,000)"

**Symptoms:** Error: "Single bill limit is LKR 5,000"

**What It Means:** Amount you entered exceeds LKR 5,000

**Solution:**
1. Check the amount you entered
2. Is amount > LKR 5,000? Yes → That's the problem
3. Options:
   - **Reduce amount:** Split into 2 bills if possible
   - **Get approval:** Ask supervisor if exception needed
   - **Verify:** Double-check receipt amount (maybe you misread)

**Example:**
```
Incorrect: LKR 6,500 (exceeds limit)
Correct: Split into LKR 3,000 + LKR 3,500
OR: Get supervisor approval for exception
```

### Problem: Cannot Save Entry - "Bill Number Already Exists"

**Symptoms:** Error: "Bill INV-001 already used in February 2026"

**What It Means:** You already entered this bill number in the same month

**Solution:**
1. Check if entry exists:
   - Click "View Entries"
   - Search for this bill number
   - Do you see it? Yes → It's already entered

2. Options:
   - **If duplicate:** Cancel. Entry is already in system.
   - **If typo in current entry:** Fix the bill number to be different
   - **If typo in old entry:** Edit the old entry to correct bill number

**Example:**
```
You entered: INV-001
System says: Already exists
Check: Is it INV-001 or INV-010? (Maybe you misread)
Solution: Use INV-010 instead
```

### Problem: Cannot Save Entry - "Description Too Short"

**Symptoms:** Error: "Description minimum 10 characters"

**Solution:**
1. Click in description field
2. Add more detail
3. Need to write at least 10 characters total (including spaces)

**Example:**
```
❌ Too short: "Oil" (3 chars)
✓ Correct: "Motor oil filter" (16 chars)

❌ Too short: "Coffee" (6 chars)
✓ Correct: "Coffee for staff" (16 chars)
```

### Problem: Cannot Save Entry - "Invalid Amount"

**Symptoms:** Error: "Amount must be greater than zero"

**Solution:**
1. Check the amount entered
2. Is it 0 or negative? Yes → That's the problem
3. Enter an amount > 0

**Common Mistakes:**
```
❌ 0 → Error
❌ -500 → Error  
✓ 1 → OK (even if small)
✓ 500 → OK
✓ 5000 → OK (but max 5000)
```

### Problem: Cannot Save Entry - "Select a Category"

**Symptoms:** Error: "Category is required"

**Solution:**
1. Look for Category dropdown field
2. Click the dropdown
3. Select one:
   - Vehicle Parts & Maintenance (E5200)
   - Office Use Items (E5300)
   - Staff Treatments & Welfare (E7800)
4. Try saving again

### Problem: Entry Seems to Disappear After Saving

**Symptoms:** Saved entry, but not in list when you check

**Cause:** May be soft-filtered or deleted

**Solution:**
1. Click "View Entries"
2. Check filters:
   - Is there a date filter? Try removing it.
   - Is "Show Deleted" checked? Uncheck it.
3. Check if entries you added appear with today's date
4. Use search to find bill number

### Problem: Edit/Delete Button Disabled

**Symptoms:** Cannot click Edit or Delete button

**Cause:** Permissions or entry is locked

**Solution:**
1. Check user role:
   - Admins can edit/delete any entry
   - Clerks can only edit own entries
   - If it's another user's entry → Cannot modify

2. If it's your entry:
   - Try logout/login again
   - Restart application
   - Contact IT if still doesn't work

---

## Report Problems

### Problem: Report is Empty / No Data

**Symptoms:** Generate report but it shows no entries

**Cause:** No entries match the date range

**Solution:**
1. Check date range:
   - Did you select the correct month/year?
   - Example: Looking at February 2026, but all entries are March 2026

2. Check filters:
   - Some entries might be filtered out
   - Try removing filters

3. Verify entries exist:
   - Click "View Entries"
   - Do you see any entries in that month? No → No entries to report

### Problem: Print Button Does Nothing

**Symptoms:** Click "Print" but nothing happens

**Cause:** Printer not configured or offline

**Solution 1: Check Printer**
1. Is printer connected? Yes → Check if powered on
2. Try printing test page:
   - Windows Settings → Devices → Printers & Scanners
   - Find your printer
   - Click → "Print a test page"
   - Did it print? If yes → Printer OK

**Solution 2: Use "Save as PDF" Instead**
1. Click "[Save as PDF]" instead of Print
2. Choose location to save
3. File saved as PDF
4. Can print PDF later, or email it

**Solution 3: Try Different Printer**
1. Click Print
2. In printer selector, choose "Microsoft Print to PDF"
3. Save as PDF
4. Print PDF from another device

### Problem: Report Is Very Slow to Generate

**Symptoms:** Clicking "Generate Report" takes 30+ seconds

**Cause:** Large amount of data to process

**Solution:**
1. This is normal if there are 100+ entries
2. Just wait for completion
3. Can do this during lunch break if needed

**To Speed Up:**
1. Try filtering by specific category first
2. Or use shorter date range (single month)

### Problem: Exported PDF Is Corrupted / Won't Open

**Symptoms:** Save as PDF, but file cannot be opened

**Solution:**
1. Try again:
   - Generate report again
   - Save to different location (e.g., Desktop)
   
2. Verify PDF reader:
   - Is Adobe Reader or similar installed?
   - Try using browser to open PDF
   
3. If file is truly corrupted → Try "Print" to PDF instead

---

## Database Issues

### Problem: "Database Error" or "Cannot Connect to Database"

**Symptoms:** Random error message about database

**Cause:** Connection lost or server down

**Solution:**
1. **Immediate:**
   - Close application
   - Wait 30 seconds
   - Reopen application
   - Try again

2. **Check Server:**
   - Is database server running?
   - Try to ping server:
     ```
     Command Prompt:
     ping serverIP
     ```
   - If no response → Server is down
   - Call IT to restart server

3. **Check Network:**
   - Can you access other network resources?
   - If network is down → Wait for IT to fix
   - Restart computer to reconnect

### Problem: "Access Denied" or "Permission Error"

**Symptoms:** Message: "You do not have permission..."

**Cause:** Your user role doesn't allow this action

**Solution:**
1. Check your role:
   - Login as admin
   - Settings → User Management
   - Find your user
   - Your role is: User / Clerk / Supervisor / Admin

2. What you can do by role:
   - **Clerk:** Add/edit own entries, view own entries
   - **Supervisor:** View all entries, approve reports
   - **Admin:** Everything

3. Need more access?
   - Ask admin to change your role
   - Or ask admin to do the action for you

### Problem: "Data Integrity Error"

**Symptoms:** Message: "Duplicate entry" or "Foreign key violation"

**Cause:** Corrupted database or race condition

**Solution:**
1. Save your work and close application
2. Contact IT
3. They will:
   - Check database integrity
   - Run repair if needed
   - Restore from backup if necessary

---

## Performance Issues

### Issue: Application Is Very Slow

**Symptoms:** Everything takes long time to respond

**Quick Fix:**
1. Close all other applications
2. Restart computer
3. Try again

**If Persists:**
1. Check disk space:
   - Right-click C:\ → Properties
   - If < 1 GB free → Delete temp files, recycle bin

2. Check network:
   - Network may be overloaded
   - Try during off-peak hours

3. Contact IT if still slow

### Issue: Reports Take Very Long Time

**Symptoms:** Report generation hangs for several minutes

**Cause:** Large data set or slow network

**Solution:**
1. Do this during lunch or breaks (not interrupting)
2. Or ask IT to optimize database (index rebuild, statistics update)

---

## Error Messages Reference

### Error: "The specified table '{table}' does not exist"

**Meaning:** Database tables not properly created  
**Action:** Contact IT to run database setup script

### Error: "Specified cast is not valid"

**Meaning:** Data type mismatch (internal error)  
**Action:** Close app, restart. If persists, contact IT.

### Error: "Cannot open file '{filename}'"

**Meaning:** Cannot access required file or database file  
**Action:** Check file permissions, IT to verify paths

### Error: "Timeout expired"

**Meaning:** Operation took too long, connection timed out  
**Action:** Try again. Check network. Contact IT if persistent.

### Error: "Conversion from string "{value}" to type Integer is not valid"

**Meaning:** Entered text where number expected  
**Action:** Clear field, enter numbers only (not letters)

### Error: "A transport-level error has occurred"

**Meaning:** Network/database communication problem  
**Action:** Check network. Check if database server is running. Contact IT.

---

## When to Contact IT

### Contact IT Immediately If:
- ✗ Application crashes frequently
- ✗ Cannot access application despite restart
- ✗ Database connection fails for everyone
- ✗ Receiving repeated error messages
- ✗ Data appears corrupted or missing
- ✗ Cannot login despite correct password

### IT Contact Information:

**Support Email:** support@yourdomain.com  
**Support Phone:** [Your IT support number]  
**In-Person:** IT department, [Location]  

**When Contacting IT, Provide:**
- Exact error message (take screenshot)
- When did problem start?
- What were you doing when it happened?
- Does it happen to other users too?
- Your username/role
- Computer name or IP address

---

**End of Troubleshooting Guide**
