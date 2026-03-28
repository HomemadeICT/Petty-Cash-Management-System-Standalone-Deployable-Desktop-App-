' ============================================================================
' ValidationResult.vb - Validation Result Classes
' Petty Cash Management System
' ============================================================================
' Purpose: Holds validation errors and warnings
' Layer: Models
' ============================================================================

''' <summary>
''' Represents the result of a validation operation.
''' </summary>
Public Class ValidationResult

#Region "Properties"

    ''' <summary>
    ''' List of validation errors (blocking).
    ''' </summary>
    Public Property Errors As New List(Of ValidationMessage)

    ''' <summary>
    ''' List of validation warnings (non-blocking).
    ''' </summary>
    Public Property Warnings As New List(Of ValidationMessage)

    ''' <summary>
    ''' Returns True if there are no errors.
    ''' </summary>
    Public ReadOnly Property IsValid As Boolean
        Get
            Return Errors.Count = 0
        End Get
    End Property

    ''' <summary>
    ''' Returns True if there are any warnings.
    ''' </summary>
    Public ReadOnly Property HasWarnings As Boolean
        Get
            Return Warnings.Count > 0
        End Get
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Adds an error message.
    ''' </summary>
    Public Sub AddError(ruleCode As String, message As String)
        Errors.Add(New ValidationMessage With {
            .RuleCode = ruleCode,
            .Message = message,
            .Severity = ValidationSeverity.Error
        })
    End Sub

    ''' <summary>
    ''' Adds a warning message.
    ''' </summary>
    Public Sub AddWarning(ruleCode As String, message As String)
        Warnings.Add(New ValidationMessage With {
            .RuleCode = ruleCode,
            .Message = message,
            .Severity = ValidationSeverity.Warning
        })
    End Sub

    ''' <summary>
    ''' Gets all error messages as a single string.
    ''' </summary>
    Public Function GetErrorString() As String
        Return String.Join(Environment.NewLine, Errors.Select(Function(e) e.Message))
    End Function

    ''' <summary>
    ''' Gets all warning messages as a single string.
    ''' </summary>
    Public Function GetWarningString() As String
        Return String.Join(Environment.NewLine, Warnings.Select(Function(w) w.Message))
    End Function

#End Region

End Class

''' <summary>
''' A single validation message.
''' </summary>
Public Class ValidationMessage
    Public Property RuleCode As String
    Public Property Message As String
    Public Property Severity As ValidationSeverity
End Class

''' <summary>
''' Result of expense operation with additional data.
''' </summary>
Public Class ExpenseOperationResult
    Inherits OperationResult

    ''' <summary>
    ''' The saved expense (if successful).
    ''' </summary>
    Public Property Expense As Expense

    ''' <summary>
    ''' Validation warnings (non-blocking).
    ''' </summary>
    Public Property Warnings As New List(Of ValidationMessage)

    ''' <summary>
    ''' Remaining monthly balance after this operation.
    ''' </summary>
    Public Property RemainingBalance As Decimal

    ''' <summary>
    ''' Creates a successful result.
    ''' </summary>
    Public Shared Overloads Function Success(expense As Expense, remaining As Decimal) As ExpenseOperationResult
        Return New ExpenseOperationResult With {
            .IsSuccess = True,
            .Expense = expense,
            .RemainingBalance = remaining,
            .Message = "Expense saved successfully."
        }
    End Function

    ''' <summary>
    ''' Creates a failed result.
    ''' </summary>
    Public Shadows Shared Function Failure(errorMessage As String) As ExpenseOperationResult
        Return New ExpenseOperationResult With {
            .IsSuccess = False,
            .ErrorMessage = errorMessage
        }
    End Function

End Class
