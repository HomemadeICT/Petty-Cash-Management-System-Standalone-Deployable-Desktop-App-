' ============================================================================
' ExpenseEntryForm.vb - Expense Entry Form Code-Behind
' Petty Cash Management System
' ============================================================================
' Purpose: Add/Edit expense entries with validation
' ============================================================================

Imports System.Windows.Forms

Public Class ExpenseEntryForm

#Region "Private Fields"
    Private _expense As Expense
    Private _isEdit As Boolean
    Private _targetYear As Integer
    Private _targetMonth As Integer
    Private _expenseService As ExpenseService
    Private _validationService As ValidationService
    Private _notificationService As NotificationService
    Private _categoryRepo As CategoryRepository
#End Region

#Region "Constructor"

    Public Sub New(expense As Expense, targetYear As Integer, targetMonth As Integer)
        InitializeComponent()
        _expense = expense
        _isEdit = (expense IsNot Nothing)
        _targetYear = targetYear
        _targetMonth = targetMonth
    End Sub

#End Region

#Region "Form Events"

    Private Sub ExpenseEntryForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FormIconHelper.ApplyIcon(Me, FormIconHelper.FormType.ExpenseEntry)
        ' Initialize services
        InitializeServices()

        ' Load categories
        LoadCategories()

        ' Set form title and date range
        If _isEdit Then
            lblTitle.Text = "Edit Expense"
            Me.Text = "Edit Expense"
            LoadExpenseData()
            
            ' For edits, we allow any date up to today, but the report month is fixed
            dtpEntryDate.MaxDate = Date.Today
        Else
            lblTitle.Text = "Add Expense"
            cboCategory.SelectedIndex = 0
            
            ' USER FREEDOM: Allow picking any date in the past for any reporting month.
            ' Decouples transaction date from reporting period as per ES Sir's request.
            dtpEntryDate.MinDate = New Date(2000, 1, 1)
            dtpEntryDate.MaxDate = Date.Today
            
            Dim targetMonthEnd As New Date(_targetYear, _targetMonth, Date.DaysInMonth(_targetYear, _targetMonth))
            dtpEntryDate.Value = If(Date.Today < targetMonthEnd, Date.Today, targetMonthEnd)
        End If

        ' Update remaining balance info
        UpdateRemainingInfo()

        ' Focus on bill number
        txtBillNo.Focus()
    End Sub

#End Region

#Region "Service Initialization"

    Private Sub InitializeServices()
        Dim expenseRepo As New ExpenseRepository()
        Dim auditRepo As New AuditLogRepository()

        _categoryRepo = New CategoryRepository()
        _validationService = New ValidationService(expenseRepo)
        Dim auditService As New AuditService(auditRepo)
        _expenseService = New ExpenseService(expenseRepo, _validationService, auditService)
        _notificationService = New NotificationService()
    End Sub

#End Region

#Region "Data Loading"

    Private Sub LoadCategories()
        Dim selectedCode As String = ""
        If cboCategory.SelectedItem IsNot Nothing Then
            selectedCode = DirectCast(cboCategory.SelectedItem, CategoryItem).Code
        End If

        cboCategory.Items.Clear()

        ' Load from database dynamically
        Dim categories = _categoryRepo.GetAll()
        For Each cat In categories
            cboCategory.Items.Add(New CategoryItem(cat.CategoryCode, $"{cat.CategoryCode} - {cat.CategoryName}"))
        Next

        cboCategory.DisplayMember = "DisplayName"

        ' Restore previous selection or default to first
        If cboCategory.Items.Count > 0 Then
            cboCategory.SelectedIndex = 0
            If Not String.IsNullOrEmpty(selectedCode) Then
                For i = 0 To cboCategory.Items.Count - 1
                    If DirectCast(cboCategory.Items(i), CategoryItem).Code = selectedCode Then
                        cboCategory.SelectedIndex = i
                        Exit For
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub LoadExpenseData()
        If _expense Is Nothing Then Return

        dtpEntryDate.Value = _expense.EntryDate
        txtBillNo.Text = _expense.BillNo
        txtDescription.Text = _expense.Description
        txtAmount.Text = _expense.Amount.ToString("N2")

        ' Select category
        For i = 0 To cboCategory.Items.Count - 1
            Dim item = DirectCast(cboCategory.Items(i), CategoryItem)
            If item.Code = _expense.CategoryCode Then
                cboCategory.SelectedIndex = i
                Exit For
            End If
        Next
    End Sub

    Private Sub UpdateRemainingInfo()
        Dim currentTotal = _expenseService.GetMonthlyTotal(_targetYear, _targetMonth)

        ' If editing, subtract the original amount
        If _isEdit AndAlso _expense IsNot Nothing Then
            currentTotal -= _expense.Amount
        End If

        Dim remaining = Constants.MONTHLY_LIMIT - currentTotal
        lblRemainingInfo.Text = $"Remaining: LKR {remaining:N2}"

        ' Color based on remaining
        If remaining < 5000 Then
            lblRemainingInfo.ForeColor = Color.Red
        ElseIf remaining < 10000 Then
            lblRemainingInfo.ForeColor = Color.Orange
        Else
            lblRemainingInfo.ForeColor = Color.Gray
        End If
    End Sub

#End Region

#Region "Button Events"

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        ' Clear warnings
        HideWarnings()

        ' Build expense object
        Dim expense = BuildExpense()
        If expense Is Nothing Then Return

        ' Validate
        Dim result = _validationService.ValidateExpense(expense, _isEdit)

        If Not result.IsValid Then
            MessageBox.Show(result.GetErrorString(), "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Show warnings if any
        If result.HasWarnings Then
            ShowWarnings(result.GetWarningString())
            If MessageBox.Show(result.GetWarningString() & Environment.NewLine & Environment.NewLine & "Continue anyway?",
                              "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.No Then
                Return
            End If
        End If

        ' Save
        Dim saveResult As OperationResult
        If _isEdit Then
            saveResult = _expenseService.UpdateExpense(expense, SessionManager.CurrentUserId)
        Else
            saveResult = _expenseService.AddExpense(expense, SessionManager.CurrentUserId)
        End If

        If saveResult.IsSuccess Then
            ' Send notifications if applicable
            Try
                SendNotificationsIfNeeded(expense)
            Catch ex As Exception
                ' Don't fail the save if notification fails
                System.Diagnostics.Debug.WriteLine($"Notification error: {ex.Message}")
            End Try

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

#End Region

#Region "Validation & Helpers"

    Private Function BuildExpense() As Expense
        ' Validate amount format
        Dim amount As Decimal
        If Not Decimal.TryParse(txtAmount.Text.Replace(",", ""), amount) Then
            MessageBox.Show("Please enter a valid amount.", "Invalid Amount", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtAmount.Focus()
            Return Nothing
        End If

        ' Get selected category
        If cboCategory.SelectedItem Is Nothing Then
            MessageBox.Show("Please select a category.", "No Category", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cboCategory.Focus()
            Return Nothing
        End If

        Dim categoryItem = DirectCast(cboCategory.SelectedItem, CategoryItem)

        Dim expense As New Expense With {
            .EntryDate = dtpEntryDate.Value.Date,
            .BillNo = txtBillNo.Text.Trim(),
            .CategoryCode = categoryItem.Code,
            .Description = txtDescription.Text.Trim(),
            .Amount = amount,
            .ReportMonth = If(_isEdit AndAlso _expense IsNot Nothing, _expense.ReportMonth, _targetMonth),
            .ReportYear = If(_isEdit AndAlso _expense IsNot Nothing, _expense.ReportYear, _targetYear),
            .CreatedBy = SessionManager.CurrentUserId
        }

        If _isEdit AndAlso _expense IsNot Nothing Then
            expense.EntryId = _expense.EntryId
        End If

        Return expense
    End Function

    Private Sub ShowWarnings(warningText As String)
        lblWarnings.Text = "⚠ " & warningText
        pnlWarnings.Height = 50
        pnlWarnings.Visible = True
        Me.Height = 510
    End Sub

    Private Sub HideWarnings()
        lblWarnings.Text = ""
        pnlWarnings.Height = 0
        pnlWarnings.Visible = False
        Me.Height = 450
    End Sub

    ''' <summary>
    ''' Sends notifications if business rules are triggered.
    ''' </summary>
    Private Sub SendNotificationsIfNeeded(expense As Expense)
        Dim monthName = New Date(_targetYear, _targetMonth, 1).ToString("MMMM yyyy")

        ' Check for overuse (monthly limit exceeded)
        Dim newTotal = _expenseService.GetMonthlyTotal(_targetYear, _targetMonth)
        If newTotal > Constants.MONTHLY_LIMIT Then
            _notificationService.SendOveruseNotification(monthName, newTotal, Constants.MONTHLY_LIMIT)
        End If

        ' Check for high-value warning (2nd+ bill >= LKR 3,000)
        If expense.Amount >= Constants.HIGH_VALUE_THRESHOLD Then
            Dim highValueCount = _expenseService.GetHighValueBillCount(_targetYear, _targetMonth)
            If highValueCount >= 2 Then
                _notificationService.SendHighValueWarning(expense.BillNo, expense.Amount, monthName)
            End If
        End If
    End Sub

#End Region

#Region "Category Management Button"

    Private Sub btnManageCategory_Click(sender As Object, e As EventArgs) Handles btnManageCategory.Click
        Using frm As New CategoryManagementForm()
            frm.ShowDialog(Me)
        End Using
        ' Reload categories after management
        LoadCategories()
    End Sub

#End Region

#Region "Input Validation"

    Private Sub txtAmount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAmount.KeyPress
        ' Allow only digits, decimal point, and control characters
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "."c AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If

        ' Only allow one decimal point
        If e.KeyChar = "."c AndAlso txtAmount.Text.Contains(".") Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtAmount_Leave(sender As Object, e As EventArgs) Handles txtAmount.Leave
        ' Format amount on leave
        Dim amount As Decimal
        If Decimal.TryParse(txtAmount.Text.Replace(",", ""), amount) Then
            txtAmount.Text = amount.ToString("N2")
        End If
    End Sub

#End Region

End Class

''' <summary>
''' Helper class for category dropdown items.
''' </summary>
Public Class CategoryItem
    Public Property Code As String
    Public Property DisplayName As String

    Public Sub New(code As String, displayName As String)
        Me.Code = code
        Me.DisplayName = displayName
    End Sub

    Public Overrides Function ToString() As String
        Return DisplayName
    End Function
End Class