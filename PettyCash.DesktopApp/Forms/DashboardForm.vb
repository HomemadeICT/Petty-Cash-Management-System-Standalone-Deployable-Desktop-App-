' ============================================================================
' DashboardForm.vb - Dashboard Form Code-Behind
' Petty Cash Management System
' ============================================================================
' Purpose: Main dashboard with expense overview and management
' ============================================================================

Imports System.Windows.Forms
Imports System.IO

Public Class DashboardForm

#Region "Private Fields"
    Private _expenseService As ExpenseService
    Private _reportService As ReportService
    Private _auditService As AuditService
    Private _notificationService As NotificationService
    Private _currentYear As Integer
    Private _currentMonth As Integer
#End Region

#Region "Form Events"

    Private Sub DashboardForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize services
        InitializeServices()

        ' Set current date
        _currentYear = Date.Now.Year
        _currentMonth = Date.Now.Month

        ' Set welcome message
        If SessionManager.CurrentUser IsNot Nothing Then
            lblWelcome.Text = $"Welcome, {SessionManager.CurrentUser.FullName} ({SessionManager.CurrentUser.Role})"
            
            ' Apply permission-based UI controls
            ApplyPermissions()
        End If

        ' Configure grid
        ConfigureGrid()

        ' Load data
        RefreshData()
    End Sub

    Private Sub DashboardForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Log logout
        If SessionManager.IsLoggedIn Then
            _auditService.LogAction(AuditActionTypes.LOGOUT, SessionManager.CurrentUserId, "User logged out")
            SessionManager.EndSession()
        End If
    End Sub

#End Region

#Region "Service Initialization"

    Private Sub InitializeServices()
        Dim expenseRepo As New ExpenseRepository()
        Dim categoryRepo As New CategoryRepository()
        Dim auditRepo As New AuditLogRepository()
        Dim validationService As New ValidationService(expenseRepo)

        _auditService = New AuditService(auditRepo)
        _expenseService = New ExpenseService(expenseRepo, validationService, _auditService)
        _reportService = New ReportService(expenseRepo, categoryRepo)
        _notificationService = New NotificationService()
    End Sub

#End Region

#Region "Data Loading"

    Private Sub RefreshData()
        ' Update month label
        lblCurrentMonth.Text = New Date(_currentYear, _currentMonth, 1).ToString("MMMM yyyy")

        ' Disable next month if future
        btnNextMonth.Enabled = Not (New Date(_currentYear, _currentMonth, 1) >= New Date(Date.Now.Year, Date.Now.Month, 1))

        ' Load summary
        LoadSummary()

        ' Load category totals
        LoadCategoryTotals()

        ' Load entries grid
        LoadEntries()

        ' Refresh session
        SessionManager.RefreshSession()
    End Sub

    Private Sub LoadSummary()
        Dim total = _expenseService.GetMonthlyTotal(_currentYear, _currentMonth)
        Dim remaining = Constants.MONTHLY_LIMIT - total

        lblTotalValue.Text = $"LKR {total:N2}"
        lblRemainingValue.Text = $"LKR {remaining:N2}"

        ' Update progress bar
        prgMonthlyLimit.Value = Math.Min(CInt(total), CInt(Constants.MONTHLY_LIMIT))

        ' Color based on usage
        If total > 20000 Then
            lblRemainingValue.ForeColor = Color.Red
        ElseIf total > 15000 Then
            lblRemainingValue.ForeColor = Color.Orange
        Else
            lblRemainingValue.ForeColor = Color.Green
        End If
    End Sub

    Private Sub LoadCategoryTotals()
        Dim totals = _expenseService.GetCategoryTotals(_currentYear, _currentMonth)

        lblCatE5200.Text = $"E5200 Vehicle Parts: LKR {totals.GetValueOrDefault("E5200", 0):N2}"
        lblCatE5300.Text = $"E5300 Office Items: LKR {totals.GetValueOrDefault("E5300", 0):N2}"
        lblCatE7800.Text = $"E7800 Physical Hardware: LKR {totals.GetValueOrDefault("E7800", 0):N2}"
        lblCatE7510.Text = $"E7510 Treatments: LKR {totals.GetValueOrDefault("E7510", 0):N2}"
    End Sub

    Private Sub LoadEntries()
        Dim entries = _expenseService.GetByMonth(_currentYear, _currentMonth)

        dgvEntries.DataSource = Nothing
        dgvEntries.DataSource = entries.Select(Function(exp) New With {
            exp.EntryId,
            .Date = exp.EntryDate.ToString("dd/MM/yyyy"),
            .BillNo = exp.BillNo,
            .Category = exp.CategoryCode,
            .Description = If(exp.Description.Length > 50, exp.Description.Substring(0, 47) & "...", exp.Description),
            .Amount = exp.Amount.ToString("N2")
        }).ToList()

        ' Hide ID column
        If dgvEntries.Columns.Contains("EntryId") Then
            dgvEntries.Columns("EntryId").Visible = False
        End If

        ' Format columns
        If dgvEntries.Columns.Contains("Amount") Then
            dgvEntries.Columns("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub ConfigureGrid()
        dgvEntries.Font = New Font("Segoe UI", 10)
        dgvEntries.RowTemplate.Height = 35
        dgvEntries.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        dgvEntries.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 51, 102)
        dgvEntries.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgvEntries.EnableHeadersVisualStyles = False
        dgvEntries.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 250)
    End Sub

#End Region

#Region "Month Navigation"

    Private Sub btnPrevMonth_Click(sender As Object, e As EventArgs) Handles btnPrevMonth.Click
        _currentMonth -= 1
        If _currentMonth < 1 Then
            _currentMonth = 12
            _currentYear -= 1
        End If
        RefreshData()
    End Sub

    Private Sub btnNextMonth_Click(sender As Object, e As EventArgs) Handles btnNextMonth.Click
        Dim nextMonth = New Date(_currentYear, _currentMonth, 1).AddMonths(1)
        If nextMonth <= New Date(Date.Now.Year, Date.Now.Month, 1) Then
            _currentMonth = nextMonth.Month
            _currentYear = nextMonth.Year
            RefreshData()
        End If
    End Sub

#End Region

#Region "Entry Actions"

    Private Sub btnAddEntry_Click(sender As Object, e As EventArgs) Handles btnAddEntry.Click
        Using frm As New ExpenseEntryForm(Nothing, _currentYear, _currentMonth)
            If frm.ShowDialog() = DialogResult.OK Then
                RefreshData()
            End If
        End Using
    End Sub

    Private Sub btnEditEntry_Click(sender As Object, e As EventArgs) Handles btnEditEntry.Click
        If dgvEntries.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select an entry to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim entryId = CInt(dgvEntries.SelectedRows(0).Cells("EntryId").Value)
        Dim expense = _expenseService.GetExpense(entryId)

        If expense Is Nothing Then
            MessageBox.Show("Entry not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Using frm As New ExpenseEntryForm(expense, _currentYear, _currentMonth)
            If frm.ShowDialog() = DialogResult.OK Then
                RefreshData()
            End If
        End Using
    End Sub

    Private Sub btnDeleteEntry_Click(sender As Object, e As EventArgs) Handles btnDeleteEntry.Click
        If dgvEntries.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select an entry to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim entryId = CInt(dgvEntries.SelectedRows(0).Cells("EntryId").Value)
        Dim billNo = dgvEntries.SelectedRows(0).Cells("BillNo").Value.ToString()

        If MessageBox.Show($"Are you sure you want to delete bill '{billNo}'?", "Confirm Delete",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then

            Dim reason = InputBox("Enter reason for deletion:", "Delete Reason")

            If Not String.IsNullOrWhiteSpace(reason) Then
                Dim result = _expenseService.DeleteExpense(entryId, SessionManager.CurrentUserId, reason)
                If result.IsSuccess Then
                    RefreshData()
                    MessageBox.Show("Entry deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show(result.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else
                MessageBox.Show("Deletion cancelled - reason is required.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub dgvEntries_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvEntries.CellDoubleClick
        If e.RowIndex >= 0 Then
            btnEditEntry_Click(sender, e)
        End If
    End Sub

#End Region

#Region "Report & Audit"

    Private Sub btnGenerateReport_Click(sender As Object, e As EventArgs) Handles btnGenerateReport.Click
        Using frm As New ReportViewerForm(_currentYear, _currentMonth)
            frm.ShowDialog()
        End Using
    End Sub

    Private Sub btnBulkExport_Click(sender As Object, e As EventArgs) Handles btnBulkExport.Click
        ' Show bulk export dialog
        Using dlg As New BulkExportForm(_currentYear)
            If dlg.ShowDialog() = DialogResult.OK Then
                ' Show save file dialog
                Dim saveDialog As New SaveFileDialog()
                saveDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*"
                saveDialog.DefaultExt = "xlsx"
                saveDialog.FileName = $"PettyCash_{_currentYear}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                saveDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)

                If saveDialog.ShowDialog() = DialogResult.OK Then
                    Try
                        ' Create bulk export service
                        Dim exportRepo As New ExpenseRepository()
                        Dim exportCatRepo As New CategoryRepository()
                        Dim bulkExportService As New BulkExportService(exportRepo, exportCatRepo)

                        ' Perform export
                        Dim result = bulkExportService.ExportMultipleMonthsToExcel(_currentYear, dlg.SelectedMonths, saveDialog.FileName)

                        If result.IsSuccess Then
                            _auditService.LogAction(AuditActionTypes.BULK_EXPORT, SessionManager.CurrentUserId, 
                                                    $"Exported {dlg.SelectedMonths.Count} months to {Path.GetFileName(saveDialog.FileName)}")
                            
                            MessageBox.Show(result.Message, "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            
                            ' Ask user if they want to open the file
                            If MessageBox.Show("Do you want to open the exported file?", "Open File",
                                              MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                Process.Start(New ProcessStartInfo(saveDialog.FileName) With {.UseShellExecute = True})
                            End If
                        Else
                            MessageBox.Show(result.Message, "Export Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If
                    Catch ex As Exception
                        MessageBox.Show($"Error during export: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
            End If
        End Using
    End Sub

    Private Sub btnAuditLog_Click(sender As Object, e As EventArgs) Handles btnAuditLog.Click
        ' Show audit log (simplified - just show recent entries)
        Dim logs = _auditService.GetRecentActivity(50)
        Dim logText = String.Join(Environment.NewLine, logs.Select(Function(l) $"{l.ActionTimestamp:dd/MM/yyyy HH:mm} | {l.ActionType} | {l.Details}"))

        MessageBox.Show(logText, "Recent Audit Log (Last 50)", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

#End Region

#Region "Settings & Logout"

    Private Sub btnSettings_Click(sender As Object, e As EventArgs) Handles btnSettings.Click
        Using frm As New AdminSettingsForm()
            frm.ShowDialog()
        End Using
    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        If MessageBox.Show("Are you sure you want to logout?", "Confirm Logout",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "Permission Controls"

    ''' <summary>
    ''' Applies permission-based visibility/enabled state to UI controls.
    ''' </summary>
    Private Sub ApplyPermissions()
        ' Expense buttons
        btnAddEntry.Visible = SessionManager.HasPermission(PermissionKeys.EXPENSE_ADD)
        btnEditEntry.Visible = SessionManager.HasPermission(PermissionKeys.EXPENSE_EDIT)
        btnDeleteEntry.Visible = SessionManager.HasPermission(PermissionKeys.EXPENSE_DELETE)

        ' Report button
        btnGenerateReport.Visible = SessionManager.HasPermission(PermissionKeys.REPORT_GENERATE)

        ' Navigation
        btnPrevMonth.Enabled = SessionManager.HasPermission(PermissionKeys.DASHBOARD_NAVIGATE)
        btnNextMonth.Enabled = SessionManager.HasPermission(PermissionKeys.DASHBOARD_NAVIGATE)

        ' Settings
        btnSettings.Visible = SessionManager.HasPermission(PermissionKeys.SETTINGS_VIEW)

        ' Audit
        btnAuditLog.Visible = SessionManager.HasPermission(PermissionKeys.AUDIT_VIEW)

        ' User management - show only if user has any user management permission
        If btnManageUsers IsNot Nothing Then
            btnManageUsers.Visible = SessionManager.HasPermission(PermissionKeys.USER_CREATE) OrElse
                                     SessionManager.HasPermission(PermissionKeys.USER_EDIT)
        End If

        ' Backup - admin only
        If btnBackupDB IsNot Nothing Then
            btnBackupDB.Visible = SessionManager.HasPermission(PermissionKeys.BACKUP_DATABASE)
        End If
    End Sub

    Private Sub btnManageUsers_Click(sender As Object, e As EventArgs) Handles btnManageUsers.Click
        Using frm As New UserManagementForm()
            frm.ShowDialog()
        End Using
    End Sub

    Private Sub btnBackupDB_Click(sender As Object, e As EventArgs) Handles btnBackupDB.Click
        Using frm As New BackupForm()
            frm.ShowDialog()
        End Using
    End Sub

#End Region

End Class