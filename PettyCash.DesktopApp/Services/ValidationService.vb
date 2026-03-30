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

#Region "Business Rules (from Constants)"
    ' Note: Limits are centrally defined in Constants.vb
#End Region

#Region "Private Fields"
    Private ReadOnly _expenseRepository As ExpenseRepository
    Private ReadOnly _categoryRepo As CategoryRepository
#End Region

#Region "Constructor"
    ''' <summary>
    ''' Initializes a new instance of the ValidationService class.
    ''' </summary>
    ''' <param name="expenseRepository">The expense repository instance.</param>
    Public Sub New(expenseRepository As ExpenseRepository, Optional categoryRepository As CategoryRepository = Nothing)
        _expenseRepository = expenseRepository
        _categoryRepo = If(categoryRepository, New CategoryRepository())
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
            result.AddError("BR2", $"Amount exceeds single bill limit of LKR {Constants.SINGLE_BILL_LIMIT:N2}.")
        End If

        ' BR1: Check monthly limit (uses report month, not entry date month)
        Dim monthlyCheckResult = CheckMonthlyLimit(expense.Amount, expense.ReportYear, expense.ReportMonth, expense.EntryId, isUpdate)
        If Not monthlyCheckResult.IsWithinLimit Then
            result.AddError("BR1", $"Monthly limit exceeded. Current total: LKR {monthlyCheckResult.CurrentTotal:N2}, Remaining: LKR {monthlyCheckResult.RemainingBalance:N2}")
        End If

        ' BR5: Check valid category
        If Not CheckCategory(expense.CategoryCode) Then
            result.AddError("BR5", "Invalid category code. The selected category does not exist in the system.")
        End If

        ' BR6: Check unique bill number (within the report month, not entry date month)
        If Not CheckUniqueBillNumber(expense.BillNo, expense.ReportYear, expense.ReportMonth, expense.EntryId, isUpdate) Then
            result.AddError("BR6", "Bill number already exists for this month.")
        End If

        ' BR7: Check date is not future
        If Not CheckDateNotFuture(expense.EntryDate) Then
            result.AddError("BR7", "Entry date cannot be in the future.")
        End If

        ' BR8: Check description length
        If Not CheckDescriptionLength(expense.Description) Then
            result.AddError("BR8", $"Description must be at least {Constants.MIN_DESCRIPTION_LENGTH} characters.")
        End If

        ' BR4: Check high-value warning (soft warning, not blocking — uses report month)
        If CheckHighValueWarning(expense.Amount, expense.ReportYear, expense.ReportMonth) Then
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
        ' NOTE: As per special requirement from ES Sir, "User Freedom" is enabled.
        ' Users/Admins can add/edit expenses in any month without hassle.
        ' Full database-driven finalization can be implemented here if locked down in future.
        Return True 
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
        result.RemainingBalance = Constants.MONTHLY_LIMIT - currentTotal
        result.IsWithinLimit = (currentTotal + amount) <= Constants.MONTHLY_LIMIT

        Return result
    End Function

    ''' <summary>
    ''' BR2: Checks if amount is within single bill limit.
    ''' </summary>
    Private Function CheckBillLimit(amount As Decimal) As Boolean
        Return amount <= Constants.SINGLE_BILL_LIMIT
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
        If amount < Constants.HIGH_VALUE_THRESHOLD Then Return False

        ' Count existing high-value bills this month
        Dim highValueCount As Integer = _expenseRepository.GetHighValueBillCount(year, month, Constants.HIGH_VALUE_THRESHOLD)

        ' Warning if this is 2nd or more high-value bill
        Return highValueCount >= 1
    End Function

    ''' <summary>
    ''' BR5: Checks if category code is valid.
    ''' </summary>
    Private Function CheckCategory(categoryCode As String) As Boolean
        If String.IsNullOrWhiteSpace(categoryCode) Then Return False
        ' Validate against database categories
        Dim cat = _categoryRepo.GetByCode(categoryCode.Trim().ToUpper())
        Return cat IsNot Nothing
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
        Return description.Trim().Length >= Constants.MIN_DESCRIPTION_LENGTH
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
