' ============================================================================
' Enums.vb - Application Enumerations
' Petty Cash Management System
' ============================================================================
' Purpose: Defines all application enumerations
' Layer: Utilities
' ============================================================================

''' <summary>
''' User roles in the system.
''' </summary>
Public Enum UserRoleEnum
    Admin = 1
    Clerk = 2
    Viewer = 3
End Enum

''' <summary>
''' Report status.
''' </summary>
Public Enum ReportStatus
    Draft = 1
    Submitted = 2
    Finalized = 3
End Enum

''' <summary>
''' Notification channels.
''' </summary>
Public Enum NotificationChannel
    Email = 1
    WhatsApp = 2
    Both = 3
    None = 0
End Enum

''' <summary>
''' Audit action types.
''' </summary>
Public Enum ActionType
    ' Authentication
    LoginSuccess
    LoginFailed
    Logout
    PasswordChanged

    ' Expenses
    ExpenseCreated
    ExpenseUpdated
    ExpenseDeleted

    ' Reports
    ReportGenerated
    ReportPrinted
    ReportFinalized

    ' Users
    UserCreated
    UserUpdated
    UserDeactivated
    UserReactivated
    UserPasswordReset

    ' Permissions
    UserPermissionGranted
    UserPermissionRevoked
    UserPermissionsReset

    ' Settings
    SettingsChanged
End Enum

''' <summary>
''' Validation severity levels.
''' </summary>
Public Enum ValidationSeverity
    [Error] = 1    ' Blocking error
    Warning = 2    ' Non-blocking warning
    Info = 3       ' Informational
End Enum
