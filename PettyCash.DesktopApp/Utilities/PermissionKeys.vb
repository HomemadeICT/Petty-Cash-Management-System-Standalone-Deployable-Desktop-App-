' ============================================================================
' PermissionKeys.vb - Permission Key Constants
' Petty Cash Management System
' ============================================================================
' Purpose: Centralized constants for all permission keys
' Layer: Utilities
' ============================================================================

''' <summary>
''' Constants for all system permission keys.
''' Single source of truth for permission identifiers.
''' </summary>
Public Class PermissionKeys

#Region "Expense Management"
    Public Const EXPENSE_ADD As String = "EXPENSE_ADD"
    Public Const EXPENSE_EDIT As String = "EXPENSE_EDIT"
    Public Const EXPENSE_DELETE As String = "EXPENSE_DELETE"
    Public Const EXPENSE_VIEW As String = "EXPENSE_VIEW"
#End Region

#Region "Reports"
    Public Const REPORT_GENERATE As String = "REPORT_GENERATE"
    Public Const REPORT_PRINT As String = "REPORT_PRINT"
    Public Const REPORT_FINALIZE As String = "REPORT_FINALIZE"
#End Region

#Region "Dashboard"
    Public Const DASHBOARD_VIEW As String = "DASHBOARD_VIEW"
    Public Const DASHBOARD_NAVIGATE As String = "DASHBOARD_NAVIGATE"
#End Region

#Region "User Management"
    Public Const USER_CREATE As String = "USER_CREATE"
    Public Const USER_EDIT As String = "USER_EDIT"
    Public Const USER_DEACTIVATE As String = "USER_DEACTIVATE"
    Public Const USER_RESET_PASSWORD As String = "USER_RESET_PASSWORD"
    Public Const USER_MANAGE_PERMISSIONS As String = "USER_MANAGE_PERMISSIONS"
#End Region

#Region "Settings"
    Public Const SETTINGS_VIEW As String = "SETTINGS_VIEW"
    Public Const SETTINGS_EDIT As String = "SETTINGS_EDIT"
#End Region

#Region "Audit"
    Public Const AUDIT_VIEW As String = "AUDIT_VIEW"
#End Region

#Region "Category & Item Management"
    Public Const CATEGORY_MANAGE As String = "CATEGORY_MANAGE"
    Public Const ITEM_MANAGE As String = "ITEM_MANAGE"
#End Region

#Region "Self-Service"
    Public Const SELF_CHANGE_PASSWORD As String = "SELF_CHANGE_PASSWORD"
#End Region

#Region "Backup & Export"
    Public Const BACKUP_DATABASE As String = "BACKUP_DATABASE"
    Public Const REPORT_EXPORT_EXCEL As String = "REPORT_EXPORT_EXCEL"
#End Region

End Class
