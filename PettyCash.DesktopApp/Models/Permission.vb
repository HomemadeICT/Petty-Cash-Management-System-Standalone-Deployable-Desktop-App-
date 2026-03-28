' ============================================================================
' Permission.vb - Permission Models
' Petty Cash Management System
' ============================================================================
' Purpose: Represents permissions and user permission assignments
' Layer: Models
' ============================================================================

''' <summary>
''' Represents a system permission.
''' </summary>
Public Class Permission

#Region "Properties"

    ''' <summary>
    ''' Unique identifier for the permission.
    ''' </summary>
    Public Property PermissionId As Integer

    ''' <summary>
    ''' Unique key for the permission (e.g., "EXPENSE_ADD").
    ''' </summary>
    Public Property PermissionKey As String

    ''' <summary>
    ''' Display name for the permission.
    ''' </summary>
    Public Property DisplayName As String

    ''' <summary>
    ''' Description of what the permission allows.
    ''' </summary>
    Public Property Description As String

    ''' <summary>
    ''' Category grouping (e.g., "Expense Management", "Reports").
    ''' </summary>
    Public Property Category As String

#End Region

End Class

''' <summary>
''' Represents a user-specific permission override.
''' </summary>
Public Class UserPermission

#Region "Properties"

    ''' <summary>
    ''' User ID this permission applies to.
    ''' </summary>
    Public Property UserId As Integer

    ''' <summary>
    ''' Permission key being granted or revoked.
    ''' </summary>
    Public Property PermissionKey As String

    ''' <summary>
    ''' Whether the permission is granted (True) or revoked (False).
    ''' </summary>
    Public Property IsGranted As Boolean

    ''' <summary>
    ''' User ID who granted/revoked this permission.
    ''' </summary>
    Public Property GrantedBy As Integer

    ''' <summary>
    ''' When the permission was granted/revoked.
    ''' </summary>
    Public Property GrantedAt As DateTime

#End Region

End Class

''' <summary>
''' DTO for displaying effective permissions with source information.
''' </summary>
Public Class EffectivePermission

#Region "Properties"

    ''' <summary>
    ''' Permission key.
    ''' </summary>
    Public Property PermissionKey As String

    ''' <summary>
    ''' Display name.
    ''' </summary>
    Public Property DisplayName As String

    ''' <summary>
    ''' Category.
    ''' </summary>
    Public Property Category As String

    ''' <summary>
    ''' Whether the user has this permission.
    ''' </summary>
    Public Property HasPermission As Boolean

    ''' <summary>
    ''' Source of the permission: "Role Default", "User Override", or "Not Granted".
    ''' </summary>
    Public Property PermissionSource As String

#End Region

End Class
