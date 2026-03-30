' ============================================================================
' Expense.vb - Expense Model
' Petty Cash Management System
' ============================================================================
' Purpose: Represents a petty cash expense entry
' Layer: Models
' ============================================================================

''' <summary>
''' Represents a petty cash expense entry.
''' </summary>
Public Class Expense

#Region "Properties"

    ''' <summary>
    ''' Unique identifier for the expense entry.
    ''' </summary>
    Public Property EntryId As Integer

    ''' <summary>
    ''' Date of the expense (transaction date).
    ''' </summary>
    Public Property EntryDate As Date

    ''' <summary>
    ''' Bill/receipt number (unique per month).
    ''' </summary>
    Public Property BillNo As String

    ''' <summary>
    ''' Category code (E5200, E5300, E7800, E7510).
    ''' </summary>
    Public Property CategoryCode As String

    ''' <summary>
    ''' Description of the expense (min 10 chars).
    ''' </summary>
    Public Property Description As String

    ''' <summary>
    ''' Amount in LKR (max 5000 per bill).
    ''' </summary>
    Public Property Amount As Decimal

    ''' <summary>
    ''' ID of the user who created this entry.
    ''' </summary>
    Public Property CreatedBy As Integer

    ''' <summary>
    ''' Timestamp when the entry was created.
    ''' </summary>
    Public Property CreatedAt As DateTime

    ''' <summary>
    ''' Timestamp of last update.
    ''' </summary>
    Public Property UpdatedAt As DateTime

    ''' <summary>
    ''' Soft delete flag.
    ''' </summary>
    Public Property IsDeleted As Boolean

    ''' <summary>
    ''' Timestamp of deletion (if soft deleted).
    ''' </summary>
    Public Property DeletedAt As DateTime?

    ''' <summary>
    ''' The month this expense is assigned to for reporting (1-12).
    ''' May differ from EntryDate.Month for cross-month posting (e.g., Dec 15-31 → January).
    ''' </summary>
    Public Property ReportMonth As Integer

    ''' <summary>
    ''' The year this expense is assigned to for reporting.
    ''' May differ from EntryDate.Year for cross-month posting (e.g., Dec 2025 → Jan 2026).
    ''' </summary>
    Public Property ReportYear As Integer

#End Region

#Region "Computed Properties"

    ''' <summary>
    ''' Returns the report year (the year this expense is assigned to for reporting).
    ''' </summary>
    Public ReadOnly Property EntryYear As Integer
        Get
            Return If(ReportYear > 0, ReportYear, EntryDate.Year)
        End Get
    End Property

    ''' <summary>
    ''' Returns the report month (the month this expense is assigned to for reporting).
    ''' </summary>
    Public ReadOnly Property EntryMonth As Integer
        Get
            Return If(ReportMonth > 0, ReportMonth, EntryDate.Month)
        End Get
    End Property

    ''' <summary>
    ''' Returns the report month name (e.g., "January 2026").
    ''' </summary>
    Public ReadOnly Property MonthName As String
        Get
            If ReportYear > 0 AndAlso ReportMonth > 0 Then
                Return New Date(ReportYear, ReportMonth, 1).ToString("MMMM yyyy")
            End If
            Return EntryDate.ToString("MMMM yyyy")
        End Get
    End Property

    ''' <summary>
    ''' Returns True if this is a high-value bill (>= 3000).
    ''' </summary>
    Public ReadOnly Property IsHighValue As Boolean
        Get
            Return Amount >= Constants.HIGH_VALUE_THRESHOLD
        End Get
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Creates a copy of this expense for editing.
    ''' </summary>
    Public Function Clone() As Expense
        Return New Expense With {
            .EntryId = Me.EntryId,
            .EntryDate = Me.EntryDate,
            .BillNo = Me.BillNo,
            .CategoryCode = Me.CategoryCode,
            .Description = Me.Description,
            .Amount = Me.Amount,
            .ReportMonth = Me.ReportMonth,
            .ReportYear = Me.ReportYear,
            .CreatedBy = Me.CreatedBy,
            .CreatedAt = Me.CreatedAt,
            .UpdatedAt = Me.UpdatedAt,
            .IsDeleted = Me.IsDeleted,
            .DeletedAt = Me.DeletedAt
        }
    End Function

#End Region

End Class
