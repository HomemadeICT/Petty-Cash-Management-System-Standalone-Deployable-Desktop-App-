' ============================================================================
' User.vb - User Model
' Petty Cash Management System
' ============================================================================
' Purpose: Represents a user account in the system
' Layer: Models
' ============================================================================

''' <summary>
''' Represents a user in the Petty Cash Management System.
''' </summary>
Public Class User

#Region "Properties"

    ''' <summary>
    ''' Unique identifier for the user.
    ''' </summary>
    Public Property UserId As Integer

    ''' <summary>
    ''' Username for login (unique).
    ''' </summary>
    Public Property Username As String

    ''' <summary>
    ''' Hashed password (BCrypt).
    ''' </summary>
    Public Property PasswordHash As String

    ''' <summary>
    ''' User's full name for display.
    ''' </summary>
    Public Property FullName As String

    ''' <summary>
    ''' User role: "Admin", "Clerk", or "Viewer"
    ''' </summary>
    Public Property Role As String

    ''' <summary>
    ''' Email address for notifications.
    ''' </summary>
    Public Property Email As String

    ''' <summary>
    ''' WhatsApp number for future notifications.
    ''' </summary>
    Public Property WhatsAppNo As String

    ''' <summary>
    ''' Date and time the user was created.
    ''' </summary>
    Public Property CreatedAt As DateTime

    ''' <summary>
    ''' Whether the user account is active.
    ''' </summary>
    Public Property IsActive As Boolean

#End Region

#Region "Computed Properties"

    ''' <summary>
    ''' Returns True if the user is an Admin.
    ''' </summary>
    Public ReadOnly Property IsAdmin As Boolean
        Get
            Return String.Equals(Role, "Admin", StringComparison.OrdinalIgnoreCase)
        End Get
    End Property

    ''' <summary>
    ''' Returns True if the user is a Clerk.
    ''' </summary>
    Public ReadOnly Property IsClerk As Boolean
        Get
            Return String.Equals(Role, "Clerk", StringComparison.OrdinalIgnoreCase)
        End Get
    End Property

    ''' <summary>
    ''' Returns True if the user is a Viewer.
    ''' </summary>
    Public ReadOnly Property IsViewer As Boolean
        Get
            Return String.Equals(Role, "Viewer", StringComparison.OrdinalIgnoreCase)
        End Get
    End Property

    ''' <summary>
    ''' Effective permissions for this user (loaded from PermissionService).
    ''' </summary>
    Public Property Permissions As List(Of String)

#End Region

End Class

''' <summary>
''' User roles enumeration.
''' </summary>
Public Enum UserRole
    Admin
    Clerk
    Viewer
End Enum
