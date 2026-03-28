' ============================================================================
' ExpenseService.vb - Expense Management Service
' Petty Cash Management System
' ============================================================================
' Purpose: Handles CRUD operations for petty cash expenses with validation
' Layer: Business Logic Layer
' Dependencies: ExpenseRepository, ValidationService, AuditService
' ============================================================================

''' <summary>
''' Manages petty cash expense entries.
''' </summary>
Public Class ExpenseService

#Region "Private Fields"
    Private ReadOnly _expenseRepository As ExpenseRepository
    Private ReadOnly _validationService As ValidationService
    Private ReadOnly _auditService As AuditService
#End Region

#Region "Constructor"
    ''' <summary>
    ''' Initializes a new instance of the ExpenseService class.
    ''' </summary>
    Public Sub New(expenseRepository As ExpenseRepository, validationService As ValidationService, auditService As AuditService)
        _expenseRepository = expenseRepository
        _validationService = validationService
        _auditService = auditService
    End Sub
#End Region

#Region "Public Methods - CRUD Operations"

    ''' <summary>
    ''' Adds a new expense entry after validation.
    ''' </summary>
    ''' <param name="expense">The expense to add.</param>
    ''' <param name="userId">The ID of the user adding the expense.</param>
    ''' <returns>OperationResult with success/failure and any warnings.</returns>
    Public Function AddExpense(expense As Expense, userId As Integer) As ExpenseOperationResult
        Dim result As New ExpenseOperationResult()

        Try
            ' Validate business rules
            Dim validationResult = _validationService.ValidateExpense(expense, isUpdate:=False)

            If Not validationResult.IsValid Then
                result.IsSuccess = False
                result.ErrorMessage = validationResult.GetErrorString()
                Return result
            End If

            ' Set metadata
            expense.CreatedBy = userId
            expense.CreatedAt = DateTime.Now
            expense.UpdatedAt = DateTime.Now
            expense.IsDeleted = False

            ' Save to database
            Dim newId = _expenseRepository.Add(expense)
            expense.EntryId = newId

            ' Log action
            _auditService.LogAction("EXPENSE_CREATED", userId,
                $"Created expense: {expense.BillNo}, Amount: {expense.Amount:N2}, Category: {expense.CategoryCode}",
                "petty_cash_entries", newId)

            result.IsSuccess = True
            result.Expense = expense
            result.Warnings = validationResult.Warnings

        Catch ex As Exception
            result.IsSuccess = False
            result.ErrorMessage = "An error occurred while saving the expense."
        End Try

        Return result
    End Function

    ''' <summary>
    ''' Updates an existing expense entry.
    ''' </summary>
    ''' <param name="expense">The expense with updated values.</param>
    ''' <param name="userId">The ID of the user updating the expense.</param>
    ''' <returns>OperationResult with success/failure.</returns>
    Public Function UpdateExpense(expense As Expense, userId As Integer) As ExpenseOperationResult
        Dim result As New ExpenseOperationResult()

        Try
            ' Get original expense
            Dim originalExpense = _expenseRepository.GetById(expense.EntryId)
            If originalExpense Is Nothing Then
                result.IsSuccess = False
                result.ErrorMessage = "Expense not found."
                Return result
            End If

            ' Check if month is finalized (BR9)
            If Not _validationService.CheckMonthNotFinalized(expense.EntryDate.Year, expense.EntryDate.Month) Then
                result.IsSuccess = False
                result.ErrorMessage = "Cannot edit expenses in a finalized month."
                Return result
            End If

            ' Validate business rules
            Dim validationResult = _validationService.ValidateExpense(expense, isUpdate:=True)

            If Not validationResult.IsValid Then
                result.IsSuccess = False
                result.ErrorMessage = validationResult.GetErrorString()
                Return result
            End If

            ' Update metadata
            expense.UpdatedAt = DateTime.Now

            ' Save changes
            _expenseRepository.Update(expense)

            ' Log action with change details
            Dim changes = GetChangeDetails(originalExpense, expense)
            _auditService.LogAction("EXPENSE_UPDATED", userId,
                $"Updated expense ID {expense.EntryId}: {changes}",
                "petty_cash_entries", expense.EntryId)

            result.IsSuccess = True
            result.Expense = expense
            result.Warnings = validationResult.Warnings

        Catch ex As Exception
            result.IsSuccess = False
            result.ErrorMessage = "An error occurred while updating the expense."
        End Try

        Return result
    End Function

    ''' <summary>
    ''' Soft deletes an expense entry (Admin only).
    ''' </summary>
    ''' <param name="entryId">The expense ID to delete.</param>
    ''' <param name="userId">The ID of the user deleting the expense.</param>
    ''' <param name="reason">The reason for deletion.</param>
    ''' <returns>OperationResult with success/failure.</returns>
    Public Function DeleteExpense(entryId As Integer, userId As Integer, reason As String) As OperationResult
        Dim result As New OperationResult()

        Try
            Dim expense = _expenseRepository.GetById(entryId)
            If expense Is Nothing Then
                result.IsSuccess = False
                result.ErrorMessage = "Expense not found."
                Return result
            End If

            ' Check if month is finalized
            If Not _validationService.CheckMonthNotFinalized(expense.EntryDate.Year, expense.EntryDate.Month) Then
                result.IsSuccess = False
                result.ErrorMessage = "Cannot delete expenses in a finalized month."
                Return result
            End If

            ' Soft delete
            _expenseRepository.SoftDelete(entryId)

            ' Log action
            _auditService.LogAction("EXPENSE_DELETED", userId,
                $"Deleted expense ID {entryId}, Bill: {expense.BillNo}, Amount: {expense.Amount:N2}, Reason: {reason}",
                "petty_cash_entries", entryId)

            result.IsSuccess = True
            result.Message = "Expense deleted successfully."

        Catch ex As Exception
            result.IsSuccess = False
            result.ErrorMessage = "An error occurred while deleting the expense."
        End Try

        Return result
    End Function

    ''' <summary>
    ''' Gets a single expense by ID.
    ''' </summary>
    Public Function GetExpense(entryId As Integer) As Expense
        Return _expenseRepository.GetById(entryId)
    End Function

    ''' <summary>
    ''' Gets all expenses for a given month.
    ''' </summary>
    Public Function GetMonthlyExpenses(year As Integer, month As Integer) As List(Of Expense)
        Return _expenseRepository.GetByMonth(year, month)
    End Function

    ''' <summary>
    ''' Gets all expenses for a given month (alias).
    ''' </summary>
    Public Function GetByMonth(year As Integer, month As Integer) As List(Of Expense)
        Return GetMonthlyExpenses(year, month)
    End Function

#End Region

#Region "Public Methods - Calculations"

    ''' <summary>
    ''' Gets the total amount for a given month.
    ''' </summary>
    Public Function GetMonthlyTotal(year As Integer, month As Integer) As Decimal
        Return _expenseRepository.GetMonthlyTotal(year, month)
    End Function

    ''' <summary>
    ''' Gets the remaining balance for a given month.
    ''' </summary>
    Public Function GetRemainingBalance(year As Integer, month As Integer) As Decimal
        Dim total = GetMonthlyTotal(year, month)
        Return ValidationService.MONTHLY_LIMIT - total
    End Function

    ''' <summary>
    ''' Gets category-wise totals for a given month.
    ''' </summary>
    Public Function GetCategoryTotals(year As Integer, month As Integer) As Dictionary(Of String, Decimal)
        Return _expenseRepository.GetCategoryTotals(year, month)
    End Function

    ''' <summary>
    ''' Gets the count of expenses for a given month.
    ''' </summary>
    Public Function GetMonthlyCount(year As Integer, month As Integer) As Integer
        Return _expenseRepository.GetMonthlyCount(year, month)
    End Function

    ''' <summary>
    ''' Gets the count of high-value bills (>= LKR 3,000) for a given month.
    ''' </summary>
    Public Function GetHighValueBillCount(year As Integer, month As Integer) As Integer
        Dim expenses = _expenseRepository.GetByMonth(year, month)
        Return expenses.Where(Function(e) e.Amount >= Constants.HIGH_VALUE_THRESHOLD).Count()
    End Function

#End Region

#Region "Private Methods"

    ''' <summary>
    ''' Compares original and updated expense to generate change description.
    ''' </summary>
    Private Function GetChangeDetails(original As Expense, updated As Expense) As String
        Dim changes As New List(Of String)

        If original.Amount <> updated.Amount Then
            changes.Add($"Amount: {original.Amount:N2} → {updated.Amount:N2}")
        End If
        If original.BillNo <> updated.BillNo Then
            changes.Add($"Bill: {original.BillNo} → {updated.BillNo}")
        End If
        If original.CategoryCode <> updated.CategoryCode Then
            changes.Add($"Category: {original.CategoryCode} → {updated.CategoryCode}")
        End If
        If original.Description <> updated.Description Then
            changes.Add("Description changed")
        End If
        If original.EntryDate <> updated.EntryDate Then
            changes.Add($"Date: {original.EntryDate:d} → {updated.EntryDate:d}")
        End If

        Return If(changes.Count > 0, String.Join(", ", changes), "No changes")
    End Function

#End Region

End Class


