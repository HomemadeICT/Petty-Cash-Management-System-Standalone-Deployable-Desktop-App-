' ============================================================================
' AuditService.vb - Audit Logging Service
' Petty Cash Management System
' ============================================================================
' Purpose: Logs all system actions for audit trail compliance
' Layer: Business Logic Layer
' Dependencies: AuditLogRepository
' ============================================================================

''' <summary>
''' Provides audit logging functionality for all system actions.
''' </summary>
Public Class AuditService

#Region "Private Fields"
    Private ReadOnly _auditLogRepository As AuditLogRepository
#End Region

#Region "Constructor"
    Public Sub New(auditLogRepository As AuditLogRepository)
        _auditLogRepository = auditLogRepository
    End Sub
#End Region

#Region "Public Methods"

    ''' <summary>
    ''' Logs a system action to the audit trail.
    ''' </summary>
    ''' <param name="actionType">Type of action (LOGIN, LOGOUT, EXPENSE_CREATED, etc.)</param>
    ''' <param name="userId">ID of the user performing the action.</param>
    ''' <param name="details">Description of the action.</param>
    ''' <param name="tableName">Optional: The table affected.</param>
    ''' <param name="recordId">Optional: The record ID affected.</param>
    Public Sub LogAction(actionType As String, userId As Integer, details As String, Optional tableName As String = "", Optional recordId As Integer = 0)
        Try
            Dim logEntry As New AuditLog With {
                .UserId = userId,
                .ActionType = actionType,
                .TableName = tableName,
                .RecordId = recordId,
                .Details = details,
                .ActionTimestamp = DateTime.Now
            }

            _auditLogRepository.Add(logEntry)

        Catch ex As Exception
            ' If audit logging fails, we should not crash the application
            ' Log to file or event log as fallback
            System.Diagnostics.Debug.WriteLine($"Audit log failed: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Gets audit log entries with optional filters.
    ''' </summary>
    ''' <param name="startDate">Optional start date filter.</param>
    ''' <param name="endDate">Optional end date filter.</param>
    ''' <param name="userId">Optional user ID filter.</param>
    ''' <param name="actionType">Optional action type filter.</param>
    ''' <returns>List of audit log entries.</returns>
    Public Function GetAuditLog(Optional startDate As Date? = Nothing,
                                Optional endDate As Date? = Nothing,
                                Optional userId As Integer? = Nothing,
                                Optional actionType As String = Nothing) As List(Of AuditLog)

        Return _auditLogRepository.GetFiltered(startDate, endDate, userId, actionType)
    End Function

    ''' <summary>
    ''' Gets all activity for a specific user.
    ''' </summary>
    ''' <param name="userId">The user ID to filter by.</param>
    ''' <returns>List of audit log entries for the user.</returns>
    Public Function GetUserActivity(userId As Integer) As List(Of AuditLog)
        Return _auditLogRepository.GetByUser(userId)
    End Function

    ''' <summary>
    ''' Gets all changes for a specific record.
    ''' </summary>
    ''' <param name="tableName">The table name.</param>
    ''' <param name="recordId">The record ID.</param>
    ''' <returns>List of audit log entries for the record.</returns>
    Public Function GetRecordHistory(tableName As String, recordId As Integer) As List(Of AuditLog)
        Return _auditLogRepository.GetByRecord(tableName, recordId)
    End Function

    ''' <summary>
    ''' Gets the most recent audit log entries.
    ''' </summary>
    ''' <param name="count">Number of entries to retrieve.</param>
    Public Function GetRecentActivity(count As Integer) As List(Of AuditLog)
        Return _auditLogRepository.GetRecent(count)
    End Function

#End Region

End Class

#Region "Action Type Constants"

''' <summary>
''' Standard action type constants for audit logging.
''' </summary>
Public Class AuditActionTypes
    ' Authentication
    Public Const LOGIN_SUCCESS As String = "LOGIN_SUCCESS"
    Public Const LOGIN_FAILED As String = "LOGIN_FAILED"
    Public Const LOGOUT As String = "LOGOUT"
    Public Const PASSWORD_CHANGED As String = "PASSWORD_CHANGED"

    ' Expense operations
    Public Const EXPENSE_CREATED As String = "EXPENSE_CREATED"
    Public Const EXPENSE_UPDATED As String = "EXPENSE_UPDATED"
    Public Const EXPENSE_DELETED As String = "EXPENSE_DELETED"

    ' Report operations
    Public Const REPORT_GENERATED As String = "REPORT_GENERATED"
    Public Const REPORT_PRINTED As String = "REPORT_PRINTED"
    Public Const REPORT_FINALIZED As String = "REPORT_FINALIZED"
    Public Const BULK_EXPORT As String = "BULK_EXPORT"

    ' User management
    Public Const USER_CREATED As String = "USER_CREATED"
    Public Const USER_UPDATED As String = "USER_UPDATED"
    Public Const USER_DEACTIVATED As String = "USER_DEACTIVATED"

    ' Settings
    Public Const SETTINGS_CHANGED As String = "SETTINGS_CHANGED"
End Class

#End Region
