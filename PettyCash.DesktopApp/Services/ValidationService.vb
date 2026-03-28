' ============================================================================
' ValidationService.vb - Business Rules Validation Service
' Petty Cash Management System
' ============================================================================
' Purpose: Enforces all business rules (BR1-BR9) for expense entries
' Layer: Business Logic Layer
' Dependencies: ExpenseRepository, Constants
' ============================================================================



''' <summary>
''' Validates expense entries against all business rules.
''' </summary>
Public Class ValidationService

#Region "Constants - Business Rule Limits"
    ' BR1: Monthly total limit
    Public Const MONTHLY_LIMIT As Decimal = 25000D

    ' BR2: Single bill limit
    Public Const SINGLE_BILL_LIMIT As Decimal = 5000D

    ' BR4: High-value warning threshold
    Public Const HIGH_VALUE_THRESHOLD As Decimal = 3000D

    ' BR8: Minimum description length
    Public Const MIN_DESCRIPTION_LENGTH As Integer = 3

    ' BR5: Valid category codes
    Public Shared ReadOnly VALID_CATEGORIES As String() = {"E5200", "E5300", "E7800", "E7510"}
#End Region

#Region "Private Fields"
    Private ReadOnly _expenseRepository As ExpenseRepository
#End Region

#Region "Constructor"
    ''' <summary>
    ''' Initializes a new instance of the ValidationService class.
    ''' </summary>
    ''' <param name="expenseRepository">The expense repository instance.</param>
    Public Sub New(expenseRepository As ExpenseRepository)
        _expenseRepository = expenseRepository
    End Sub
#End Region

#Region "Public Methods"

    ''' <summary>
    ''' Validates an expense entry against all business rules.
    ''' </summary>
    ''' <param name="expense">The expense to validate.</param>
    ''' <param name="isUpdate">True if this is an update operation (for monthly total calculation).</param>
    ''' <returns>ValidationResult with errors and warnings.</returns>
    Public Function ValidateExpense(expense As Expense, Optional isUpdate As Boolean = False) As ValidationResult
        Dim result As New ValidationResult()
        ' result.IsValid is ReadOnly and derived from Errors.Count in Models.ValidationResult

        ' BR3: Check positive amount
        If Not CheckPositiveAmount(expense.Amount) Then
            result.AddError("BR3", "Amount must be greater than zero.")
        End If

        ' BR2: Check single bill limit
        If Not CheckBillLimit(expense.Amount) Then
            result.AddError("BR2", $"Amount exceeds single bill limit of LKR {SINGLE_BILL_LIMIT:N2}.")
        End If

        ' BR1: Check monthly limit
        Dim monthlyCheckResult = CheckMonthlyLimit(expense.Amount, expense.EntryDate.Year, expense.EntryDate.Month, expense.EntryId, isUpdate)
        If Not monthlyCheckResult.IsWithinLimit Then
            result.AddError("BR1", $"Monthly limit exceeded. Current total: LKR {monthlyCheckResult.CurrentTotal:N2}, Remaining: LKR {monthlyCheckResult.RemainingBalance:N2}")
        End If

        ' BR5: Check valid category
        If Not CheckCategory(expense.CategoryCode) Then
            result.AddError("BR5", $"Invalid category code. Must be one of: {String.Join(", ", VALID_CATEGORIES)}")
        End If

        ' BR6: Check unique bill number
        If Not CheckUniqueBillNumber(expense.BillNo, expense.EntryDate.Year, expense.EntryDate.Month, expense.EntryId, isUpdate) Then
            result.AddError("BR6", "Bill number already exists for this month.")
        End If

        ' BR7: Check date is not future
        If Not CheckDateNotFuture(expense.EntryDate) Then
            result.AddError("BR7", "Entry date cannot be in the future.")
        End If

        ' BR8: Check description length
        If Not CheckDescriptionLength(expense.Description) Then
            result.AddError("BR8", $"Description must be at least {MIN_DESCRIPTION_LENGTH} characters.")
        End If

        ' BR4: Check high-value warning (soft warning, not blocking)
        If CheckHighValueWarning(expense.Amount, expense.EntryDate.Year, expense.EntryDate.Month) Then
            result.AddWarning("BR4", $"This is a high-value bill (LKR {expense.Amount:N2}). Multiple high-value bills may require supervisor review.")
        End If

        Return result
    End Function

    ''' <summary>
    ''' Checks if adding to or editing a finalized month.
    ''' </summary>
    ''' <param name="year">The year.</param>
    ''' <param name="month">The month.</param>
    ''' <returns>True if month is NOT finalized (can edit), False if finalized.</returns>
    Public Function CheckMonthNotFinalized(year As Integer, month As Integer) As Boolean
        ' BR9: Finalized month is locked
        ' TODO: Implement finalization check from database
        Return True ' Placeholder - return True means not finalized
    End Function

#End Region

#Region "Private Validation Methods"

    ''' <summary>
    ''' BR1: Checks if the monthly limit would be exceeded.
    ''' </summary>
    Private Function CheckMonthlyLimit(amount As Decimal, year As Integer, month As Integer, entryId As Integer, isUpdate As Boolean) As MonthlyLimitResult
        Dim result As New MonthlyLimitResult()

        ' Get current monthly total
        Dim currentTotal As Decimal = _expenseRepository.GetMonthlyTotal(year, month)

        ' If updating, subtract the original amount
        If isUpdate AndAlso entryId > 0 Then
            Dim originalExpense = _expenseRepository.GetById(entryId)
            If originalExpense IsNot Nothing Then
                currentTotal -= originalExpense.Amount
            End If
        End If

        result.CurrentTotal = currentTotal
        result.NewTotal = currentTotal + amount
        result.RemainingBalance = MONTHLY_LIMIT - currentTotal
        result.IsWithinLimit = (currentTotal + amount) <= MONTHLY_LIMIT

        Return result
    End Function

    ''' <summary>
    ''' BR2: Checks if amount is within single bill limit.
    ''' </summary>
    Private Function CheckBillLimit(amount As Decimal) As Boolean
        Return amount <= SINGLE_BILL_LIMIT
    End Function

    ''' <summary>
    ''' BR3: Checks if amount is positive.
    ''' </summary>
    Private Function CheckPositiveAmount(amount As Decimal) As Boolean
        Return amount > 0
    End Function

    ''' <summary>
    ''' BR4: Checks if this is a high-value warning threshold.
    ''' Returns True if warning should be shown.
    ''' </summary>
    Private Function CheckHighValueWarning(amount As Decimal, year As Integer, month As Integer) As Boolean
        If amount < HIGH_VALUE_THRESHOLD Then Return False

        ' Count existing high-value bills this month
        Dim highValueCount As Integer = _expenseRepository.GetHighValueBillCount(year, month, HIGH_VALUE_THRESHOLD)

        ' Warning if this is 2nd or more high-value bill
        Return highValueCount >= 1
    End Function

    ''' <summary>
    ''' BR5: Checks if category code is valid.
    ''' </summary>
    Private Function CheckCategory(categoryCode As String) As Boolean
        If String.IsNullOrWhiteSpace(categoryCode) Then Return False
        Return VALID_CATEGORIES.Contains(categoryCode.Trim().ToUpper())
    End Function

    ''' <summary>
    ''' BR6: Checks if bill number is unique within the month.
    ''' </summary>
    Private Function CheckUniqueBillNumber(billNo As String, year As Integer, month As Integer, entryId As Integer, isUpdate As Boolean) As Boolean
        If String.IsNullOrWhiteSpace(billNo) Then Return False

        Dim existingEntry = _expenseRepository.GetByBillNumber(billNo.Trim(), year, month)

        If existingEntry Is Nothing Then Return True

        ' If updating, allow same bill number if it's the same entry
        If isUpdate AndAlso existingEntry.EntryId = entryId Then Return True

        Return False
    End Function

    ''' <summary>
    ''' BR7: Checks if date is not in the future.
    ''' </summary>
    Private Function CheckDateNotFuture(entryDate As Date) As Boolean
        Return entryDate.Date <= DateTime.Today
    End Function

    ''' <summary>
    ''' BR8: Checks if description meets minimum length.
    ''' </summary>
    Private Function CheckDescriptionLength(description As String) As Boolean
        If String.IsNullOrWhiteSpace(description) Then Return False
        Return description.Trim().Length >= MIN_DESCRIPTION_LENGTH
    End Function

#End Region

    ''' <summary>
    ''' Result of monthly limit check.
    ''' </summary>
    Private Class MonthlyLimitResult
        Public Property IsWithinLimit As Boolean
        Public Property CurrentTotal As Decimal
        Public Property NewTotal As Decimal
        Public Property RemainingBalance As Decimal
    End Class

End Class
