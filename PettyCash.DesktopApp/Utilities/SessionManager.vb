' ============================================================================
' SessionManager.vb - Session State Management
' Petty Cash Management System
' ============================================================================
' Purpose: Manages the current user session across the application
' Layer: Utilities
' ============================================================================

''' <summary>
''' Manages the current user session.
''' </summary>
Public Class SessionManager

#Region "Private Fields"
    Private Shared _currentUser As User
    Private Shared _sessionToken As String
    Private Shared _loginTime As DateTime
    Private Shared ReadOnly _sessionTimeoutMinutes As Integer = 30
    Private Shared _permissionService As PermissionService
#End Region

#Region "Properties"

    ''' <summary>
    ''' Gets the currently logged-in user.
    ''' </summary>
    Public Shared ReadOnly Property CurrentUser As User
        Get
            Return _currentUser
        End Get
    End Property

    ''' <summary>
    ''' Gets whether a user is currently logged in.
    ''' </summary>
    Public Shared ReadOnly Property IsLoggedIn As Boolean
        Get
            Return _currentUser IsNot Nothing AndAlso IsSessionValid()
        End Get
    End Property

    ''' <summary>
    ''' Gets whether the current user is an Admin.
    ''' </summary>
    Public Shared ReadOnly Property IsAdmin As Boolean
        Get
            Return _currentUser IsNot Nothing AndAlso _currentUser.IsAdmin
        End Get
    End Property

    ''' <summary>
    ''' Gets the current user's ID.
    ''' </summary>
    Public Shared ReadOnly Property CurrentUserId As Integer
        Get
            Return If(_currentUser IsNot Nothing, _currentUser.UserId, 0)
        End Get
    End Property

#End Region

#Region "Public Methods"

    ''' <summary>
    ''' Starts a new session for the given user.
    ''' </summary>
    Public Shared Sub StartSession(user As User, token As String, permissionService As PermissionService)
        _currentUser = user
        _sessionToken = token
        _loginTime = DateTime.Now
        _permissionService = permissionService
        
        ' Load user permissions
        If _permissionService IsNot Nothing Then
            user.Permissions = _permissionService.GetUserPermissions(user.UserId)
        End If
    End Sub

    ''' <summary>
    ''' Ends the current session.
    ''' </summary>
    Public Shared Sub EndSession()
        _currentUser = Nothing
        _sessionToken = Nothing
        _loginTime = DateTime.MinValue
    End Sub

    ''' <summary>
    ''' Checks if the session is still valid.
    ''' </summary>
    Public Shared Function IsSessionValid() As Boolean
        If _currentUser Is Nothing Then Return False
        If String.IsNullOrEmpty(_sessionToken) Then Return False

        Dim elapsed = DateTime.Now.Subtract(_loginTime).TotalMinutes
        Return elapsed < _sessionTimeoutMinutes
    End Function

    ''' <summary>
    ''' Refreshes the session timeout.
    ''' </summary>
    Public Shared Sub RefreshSession()
        If _currentUser IsNot Nothing Then
            _loginTime = DateTime.Now
        End If
    End Sub

    ''' <summary>
    ''' Checks if the current user has a specific permission.
    ''' </summary>
    Public Shared Function HasPermission(permissionKey As String) As Boolean
        If _permissionService Is Nothing Then
            ' Fallback for legacy mode or when DB migration is missing
            ' If we don't have the service, rely on IsAdmin for backwards compatibility
            Return _currentUser IsNot Nothing AndAlso _currentUser.IsAdmin
        End If

        If _currentUser Is Nothing Then Return False
        
        Return _permissionService.HasPermission(_currentUser.UserId, permissionKey)
    End Function

    ''' <summary>
    ''' Refreshes the current user's permissions (call after permission changes).
    ''' </summary>
    Public Shared Sub RefreshPermissions()
        If _currentUser IsNot Nothing AndAlso _permissionService IsNot Nothing Then
            _permissionService.ClearCache(_currentUser.UserId)
            _currentUser.Permissions = _permissionService.GetUserPermissions(_currentUser.UserId)
        End If
    End Sub

#End Region

End Class
