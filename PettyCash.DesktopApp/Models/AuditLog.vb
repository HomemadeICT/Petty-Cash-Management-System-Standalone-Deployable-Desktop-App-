' ============================================================================
' AuditLog.vb - Audit Log Model
' Petty Cash Management System
' ============================================================================
' Purpose: Represents an audit log entry
' Layer: Models
' ============================================================================

''' <summary>
''' Represents an entry in the audit log.
''' </summary>
Public Class AuditLog

#Region "Properties"

    ''' <summary>
    ''' Unique identifier for the log entry.
    ''' </summary>
    Public Property AuditId As Integer

    ''' <summary>
    ''' ID of the user who performed the action.
    ''' </summary>
    Public Property UserId As Integer

    ''' <summary>
    ''' Type of action performed (LOGIN, EXPENSE_CREATED, etc.)
    ''' </summary>
    Public Property ActionType As String

    ''' <summary>
    ''' Name of the table affected (if applicable).
    ''' </summary>
    Public Property TableName As String

    ''' <summary>
    ''' ID of the record affected (if applicable).
    ''' </summary>
    Public Property RecordId As Integer

    ''' <summary>
    ''' Detailed description of the action.
    ''' </summary>
    Public Property Details As String

    ''' <summary>
    ''' Timestamp when the action occurred.
    ''' </summary>
    Public Property ActionTimestamp As DateTime

#End Region

#Region "Navigation Properties"

    ''' <summary>
    ''' The user who performed the action (loaded on demand).
    ''' </summary>
    Public Property User As User

#End Region

End Class
