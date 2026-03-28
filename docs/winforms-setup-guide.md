# WinForms Setup Guide

**Purpose:** Step-by-step guide to create the UI Forms and integrate with existing code  
**Last Updated:** February 9, 2026

---

## Prerequisites

- ✅ Visual Studio 2022 (Community or higher)
- ✅ .NET 8.0 SDK
- ✅ SQLite 3 (Managed via NuGet)
- ✅ Generated code files (Services, Models, Repositories, Utilities)

---

## Part 1: Project Setup

### Step 1.1: Create New WinForms Project

1. Open **Visual Studio 2022**
2. Click **Create a new project**
3. Search for **"Windows Forms App (.NET Framework)"**
4. Select **Visual Basic** version
5. Click **Next**
6. Configure:
   - Project name: `PettyCash.DesktopApp`
   - Location: *Choose your preferred project directory*
   - Framework: **.NET Framework 4.8** (or **.NET 8.0** for modern setup)
7. Click **Create**

### Step 1.2: Add NuGet Package (BCrypt.Net)

1. Right-click project → **Manage NuGet Packages**
2. Search for: `BCrypt.Net-Next`
3. Click **Install**
4. Accept the license

### Step 1.3: Add Existing Code Files

1. In Solution Explorer, right-click project → **Add** → **Existing Item...**
2. Navigate to `PettyCash.DesktopApp\Services\`
3. Select ALL `.vb` files → Click **Add**
4. Repeat for:
   - `Models\` folder (5 files)
   - `Repositories\` folder (6 files)
   - `Utilities\` folder (3 files)
5. Add `App.config` and `Program.vb`

### Step 1.4: Create Folder Structure

In Solution Explorer, create folders:
```
PettyCash.DesktopApp/
├── Forms/          ← Create this
├── Reports/        ← Create this
├── Services/       ← Already exists
├── Repositories/   ← Already exists
├── Models/         ← Already exists
└── Utilities/      ← Already exists
```

Right-click project → **Add** → **New Folder**

---

## Part 2: Create Forms

### Step 2.1: LoginForm

1. Right-click `Forms` folder → **Add** → **Windows Form**
2. Name: `LoginForm.vb`
3. Click **Add**

#### Design the Form

| Control | Name | Properties |
|---------|------|------------|
| Label | lblTitle | Text = "Petty Cash Management System", Font = Bold 16pt |
| Label | lblUsername | Text = "Username:" |
| TextBox | txtUsername | Width = 200 |
| Label | lblPassword | Text = "Password:" |
| TextBox | txtPassword | PasswordChar = "*", Width = 200 |
| Button | btnLogin | Text = "Login", Width = 100 |
| Label | lblStatus | Text = "", ForeColor = Red |

#### Form Properties

| Property | Value |
|----------|-------|
| Text | "Login - Petty Cash System" |
| StartPosition | CenterScreen |
| FormBorderStyle | FixedDialog |
| MaximizeBox | False |
| MinimizeBox | False |
| Size | 350, 250 |

#### Code Behind (LoginForm.vb)

```vb
Public Class LoginForm

    Private _authService As AuthService
    Private _userRepository As UserRepository
    Private _auditLogRepository As AuditLogRepository

    Private Sub LoginForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize dependencies
        _userRepository = New UserRepository()
        _auditLogRepository = New AuditLogRepository()
        _authService = New AuthService(_userRepository, _auditLogRepository)
        
        txtUsername.Focus()
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        lblStatus.Text = ""
        
        If String.IsNullOrWhiteSpace(txtUsername.Text) Then
            lblStatus.Text = "Please enter username"
            Return
        End If
        
        If String.IsNullOrWhiteSpace(txtPassword.Text) Then
            lblStatus.Text = "Please enter password"
            Return
        End If
        
        Dim result = _authService.AuthenticateUser(txtUsername.Text, txtPassword.Text)
        
        If result.IsSuccess Then
            ' Open Dashboard
            Dim dashboard As New DashboardForm(result.User)
            Me.Hide()
            dashboard.ShowDialog()
            Me.Close()
        Else
            lblStatus.Text = result.ErrorMessage
            txtPassword.Clear()
            txtPassword.Focus()
        End If
    End Sub

    Private Sub txtPassword_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPassword.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            btnLogin_Click(sender, e)
        End If
    End Sub

End Class
```

---

### Step 2.2: DashboardForm

1. Right-click `Forms` folder → **Add** → **Windows Form**
2. Name: `DashboardForm.vb`
3. Click **Add**

#### Design the Form

| Control | Name | Properties |
|---------|------|------------|
| Panel | pnlHeader | Dock = Top, Height = 60, BackColor = DarkBlue |
| Label | lblTitle | Text = "Dashboard", ForeColor = White, Font = Bold 18pt |
| Label | lblUser | Text = "Welcome, {user}", ForeColor = White |
| Button | btnLogout | Text = "Logout", Anchor = Right |
| GroupBox | grpSummary | Text = "Monthly Summary" |
| Label | lblMonthYear | Text = "February 2026" |
| Label | lblTotal | Text = "Total: LKR 0.00" |
| Label | lblRemaining | Text = "Remaining: LKR 25,000.00" |
| ProgressBar | prgLimit | Maximum = 25000, Value = 0 |
| DataGridView | dgvEntries | Dock = Fill (in separate panel) |
| Button | btnAddEntry | Text = "Add Entry" |
| Button | btnEditEntry | Text = "Edit" |
| Button | btnDeleteEntry | Text = "Delete" |
| Button | btnReport | Text = "Generate Report" |

#### Form Properties

| Property | Value |
|----------|-------|
| Text | "Dashboard - Petty Cash System" |
| StartPosition | CenterScreen |
| WindowState | Maximized |
| MinimumSize | 1024, 600 |

#### Code Behind (DashboardForm.vb)

```vb
Public Class DashboardForm

    Private _currentUser As User
    Private _expenseService As ExpenseService
    Private _reportService As ReportService
    Private _currentYear As Integer
    Private _currentMonth As Integer

    Public Sub New(user As User)
        InitializeComponent()
        _currentUser = user
        _currentYear = Date.Now.Year
        _currentMonth = Date.Now.Month
    End Sub

    Private Sub DashboardForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize services
        Dim expenseRepo As New ExpenseRepository()
        Dim categoryRepo As New CategoryRepository()
        Dim auditRepo As New AuditLogRepository()
        Dim validationService As New ValidationService(expenseRepo)
        Dim auditService As New AuditService(auditRepo)
        
        _expenseService = New ExpenseService(expenseRepo, validationService, auditService)
        _reportService = New ReportService(expenseRepo, categoryRepo)
        
        ' Set user info
        lblUser.Text = $"Welcome, {_currentUser.FullName}"
        
        ' Load data
        RefreshData()
    End Sub

    Private Sub RefreshData()
        ' Update month label
        lblMonthYear.Text = New Date(_currentYear, _currentMonth, 1).ToString("MMMM yyyy")
        
        ' Get monthly data
        Dim total = _expenseService.GetMonthlyTotal(_currentYear, _currentMonth)
        Dim remaining = 25000D - total
        
        lblTotal.Text = $"Total: LKR {total:N2}"
        lblRemaining.Text = $"Remaining: LKR {remaining:N2}"
        
        prgLimit.Value = Math.Min(CInt(total), 25000)
        prgLimit.ForeColor = If(total > 20000, Color.Red, If(total > 15000, Color.Orange, Color.Green))
        
        ' Load entries grid
        LoadEntries()
    End Sub

    Private Sub LoadEntries()
        Dim entries = _expenseService.GetByMonth(_currentYear, _currentMonth)
        
        dgvEntries.DataSource = Nothing
        dgvEntries.DataSource = entries.Select(Function(e) New With {
            e.EntryId,
            .Date = e.EntryDate.ToString("dd/MM/yyyy"),
            .Bill = e.BillNo,
            .Category = e.CategoryCode,
            e.Description,
            .Amount = e.Amount.ToString("N2")
        }).ToList()
        
        ' Hide ID column
        If dgvEntries.Columns.Contains("EntryId") Then
            dgvEntries.Columns("EntryId").Visible = False
        End If
    End Sub

    Private Sub btnAddEntry_Click(sender As Object, e As EventArgs) Handles btnAddEntry.Click
        Using frm As New ExpenseEntryForm(_currentUser, Nothing)
            If frm.ShowDialog() = DialogResult.OK Then
                RefreshData()
            End If
        End Using
    End Sub

    Private Sub btnEditEntry_Click(sender As Object, e As EventArgs) Handles btnEditEntry.Click
        If dgvEntries.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select an entry to edit.")
            Return
        End If
        
        Dim entryId = CInt(dgvEntries.SelectedRows(0).Cells("EntryId").Value)
        Dim expense = _expenseService.GetExpense(entryId)
        
        Using frm As New ExpenseEntryForm(_currentUser, expense)
            If frm.ShowDialog() = DialogResult.OK Then
                RefreshData()
            End If
        End Using
    End Sub

    Private Sub btnDeleteEntry_Click(sender As Object, e As EventArgs) Handles btnDeleteEntry.Click
        If dgvEntries.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select an entry to delete.")
            Return
        End If
        
        If MessageBox.Show("Are you sure you want to delete this entry?", "Confirm Delete", 
                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            
            Dim entryId = CInt(dgvEntries.SelectedRows(0).Cells("EntryId").Value)
            Dim reason = InputBox("Enter reason for deletion:", "Delete Reason")
            
            If Not String.IsNullOrEmpty(reason) Then
                _expenseService.DeleteExpense(entryId, _currentUser.UserId, reason)
                RefreshData()
            End If
        End If
    End Sub

    Private Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        Dim report = _reportService.GenerateMonthlyReport(_currentYear, _currentMonth)
        
        Using frm As New ReportViewerForm(report)
            frm.ShowDialog()
        End Using
    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        Me.Close()
    End Sub

End Class
```

---

### Step 2.3: ExpenseEntryForm

1. Right-click `Forms` folder → **Add** → **Windows Form**
2. Name: `ExpenseEntryForm.vb`
3. Click **Add**

#### Design the Form

| Control | Name | Properties |
|---------|------|------------|
| Label | lblTitle | Text = "Add Expense" or "Edit Expense" |
| DateTimePicker | dtpDate | Format = Short |
| TextBox | txtBillNo | |
| ComboBox | cboCategory | DropDownStyle = DropDownList |
| TextBox | txtDescription | Multiline = True, Height = 60 |
| TextBox | txtAmount | |
| Label | lblWarnings | ForeColor = Orange |
| Button | btnSave | Text = "Save" |
| Button | btnCancel | Text = "Cancel" |

#### Form Properties

| Property | Value |
|----------|-------|
| Text | "Add Expense" |
| StartPosition | CenterParent |
| FormBorderStyle | FixedDialog |
| Size | 400, 350 |

#### Code Behind (ExpenseEntryForm.vb)

```vb
Public Class ExpenseEntryForm

    Private _currentUser As User
    Private _expense As Expense
    Private _isEdit As Boolean
    Private _expenseService As ExpenseService
    Private _validationService As ValidationService

    Public Sub New(user As User, expense As Expense)
        InitializeComponent()
        _currentUser = user
        _expense = expense
        _isEdit = (expense IsNot Nothing)
    End Sub

    Private Sub ExpenseEntryForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize services
        Dim expenseRepo As New ExpenseRepository()
        Dim auditRepo As New AuditLogRepository()
        _validationService = New ValidationService(expenseRepo)
        Dim auditService As New AuditService(auditRepo)
        _expenseService = New ExpenseService(expenseRepo, _validationService, auditService)
        
        ' Load categories
        cboCategory.Items.Clear()
        cboCategory.Items.Add(New With {.Code = "E5200", .Name = "E5200 - Vehicle Parts"})
        cboCategory.Items.Add(New With {.Code = "E5300", .Name = "E5300 - Office Items"})
        cboCategory.Items.Add(New With {.Code = "E7800", .Name = "E7800 - Physical Hardware"})
        cboCategory.Items.Add(New With {.Code = "E7510", .Name = "E7510 - Treatments & Staff"})
        cboCategory.DisplayMember = "Name"
        cboCategory.ValueMember = "Code"
        
        If _isEdit Then
            lblTitle.Text = "Edit Expense"
            Me.Text = "Edit Expense"
            LoadExpenseData()
        Else
            lblTitle.Text = "Add Expense"
            dtpDate.Value = Date.Today
            cboCategory.SelectedIndex = 0
        End If
    End Sub

    Private Sub LoadExpenseData()
        dtpDate.Value = _expense.EntryDate
        txtBillNo.Text = _expense.BillNo
        txtDescription.Text = _expense.Description
        txtAmount.Text = _expense.Amount.ToString("N2")
        
        For i = 0 To cboCategory.Items.Count - 1
            If cboCategory.Items(i).Code = _expense.CategoryCode Then
                cboCategory.SelectedIndex = i
                Exit For
            End If
        Next
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        ' Build expense object
        Dim expense As New Expense With {
            .EntryDate = dtpDate.Value.Date,
            .BillNo = txtBillNo.Text.Trim(),
            .CategoryCode = cboCategory.SelectedItem.Code,
            .Description = txtDescription.Text.Trim(),
            .Amount = CDec(Val(txtAmount.Text)),
            .CreatedBy = _currentUser.UserId
        }
        
        If _isEdit Then
            expense.EntryId = _expense.EntryId
        End If
        
        ' Validate
        Dim result = _validationService.ValidateExpense(expense, _isEdit)
        
        If Not result.IsValid Then
            MessageBox.Show(String.Join(Environment.NewLine, result.Errors.Select(Function(err) err.Message)),
                           "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        
        ' Show warnings if any
        If result.Warnings.Any() Then
            Dim warningMsg = String.Join(Environment.NewLine, result.Warnings.Select(Function(w) w.Message))
            If MessageBox.Show(warningMsg & Environment.NewLine & Environment.NewLine & "Continue anyway?",
                              "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.No Then
                Return
            End If
        End If
        
        ' Save
        Dim saveResult As ExpenseOperationResult
        If _isEdit Then
            saveResult = _expenseService.UpdateExpense(expense, _currentUser.UserId)
        Else
            saveResult = _expenseService.AddExpense(expense, _currentUser.UserId)
        End If
        
        If saveResult.IsSuccess Then
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Else
            MessageBox.Show(saveResult.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

End Class
```

---

### Step 2.4: ReportViewerForm

1. Right-click `Forms` folder → **Add** → **Windows Form**
2. Name: `ReportViewerForm.vb`
3. Click **Add**

#### Design the Form

| Control | Name | Properties |
|---------|------|------------|
| RichTextBox | rtbReport | Dock = Fill, ReadOnly = True, Font = Consolas 10pt |
| Panel | pnlButtons | Dock = Bottom, Height = 50 |
| Button | btnPrint | Text = "Print" |
| Button | btnClose | Text = "Close" |

#### Code Behind (ReportViewerForm.vb)

```vb
Public Class ReportViewerForm

    Private _report As MonthlyReportDTO
    Private _reportService As ReportService

    Public Sub New(report As MonthlyReportDTO)
        InitializeComponent()
        _report = report
    End Sub

    Private Sub ReportViewerForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim expenseRepo As New ExpenseRepository()
        Dim categoryRepo As New CategoryRepository()
        _reportService = New ReportService(expenseRepo, categoryRepo)
        
        ' Display formatted report
        rtbReport.Text = _reportService.FormatReportForPrint(_report)
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim printDoc As New Printing.PrintDocument()
        AddHandler printDoc.PrintPage, AddressOf PrintPage
        
        Dim printDialog As New PrintDialog()
        printDialog.Document = printDoc
        
        If printDialog.ShowDialog() = DialogResult.OK Then
            printDoc.Print()
        End If
    End Sub

    Private Sub PrintPage(sender As Object, e As Printing.PrintPageEventArgs)
        Dim font As New Font("Consolas", 10)
        e.Graphics.DrawString(rtbReport.Text, font, Brushes.Black, 50, 50)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class
```

---

## Part 3: Configure Program.vb

Update `Program.vb` to start with LoginForm:

```vb
Module Program

    <STAThread()>
    Sub Main()
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)

        ' Test database connection
        If Not DbContext.TestConnection() Then
            MessageBox.Show("Cannot connect to database. Check connection string.",
                           "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Start with Login Form
        Application.Run(New LoginForm())
    End Sub

End Module
```

---

## Part 4: Database Setup

### Step 4.1: SQLite Database Setup

In a SQLite-based project, you don't need to manually install a database server. The application handles it automatically.

1. **NuGet Packages:** Ensure you have installed the `System.Data.SQLite` package.
2. **Schema Script:** Place your `schema_sqlite.sql` file in the `SQL/` directory.
3. **App.config:** Ensure your connection string points to a local file:
   ```xml
   <add name="PettyCashDB" connectionString="Data Source=PettyCash.db;Version=3;" />
   ```
4. **Initialization:** The application will execute the schema script on first launch if the database file is missing.

---

## Part 5: Test the Application

1. **Build the project**: Ctrl+Shift+B
2. **Run**: F5
3. **Login**: admin / admin123
4. **Test adding an expense**
5. **Test generating a report**

---

## Troubleshooting

| Issue | Solution |
|-------|----------|
| Database connection error | Check App.config connection string |
| BCrypt error | Ensure BCrypt.Net-Next is installed |
| Form designer error | Close and reopen the form |
| Namespace errors | Check all files are in same namespace |

---

## File Checklist

After completion, you should have:

```
Forms/
├── LoginForm.vb
├── LoginForm.Designer.vb
├── DashboardForm.vb
├── DashboardForm.Designer.vb
├── ExpenseEntryForm.vb
├── ExpenseEntryForm.Designer.vb
├── ReportViewerForm.vb
└── ReportViewerForm.Designer.vb
```

---

**Ready to use!** 🎉
