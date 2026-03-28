' ============================================================================
' PermissionService.vb - Permission Management Service
' Petty Cash Management System
' ============================================================================
' Purpose: Handles permission checking and management
' Layer: Business Logic Layer
' Dependencies: PermissionRepository, AuditService
' ============================================================================

''' <summary>
''' Provides permission checking and management services.
''' </summary>
Public Class PermissionService

#Region "Private Fields"
    Private ReadOnly _permissionRepository As PermissionRepository
    Private ReadOnly _auditService As AuditService
    Private _cachedPermissions As Dictionary(Of Integer, List(Of String))
#End Region

#Region "Constructor"
    Public Sub New(permissionRepository As PermissionRepository, auditService As AuditService)
        _permissionRepository = permissionRepository
        _auditService = auditService
        _cachedPermissions = New Dictionary(Of Integer, List(Of String))
    End Sub
#End Region

#Region "Permission Checking"

    ''' <summary>
    ''' Checks if a user has a specific permission.
    ''' Uses cache for performance.
    ''' </summary>
    Public Function HasPermission(userId As Integer, permissionKey As String) As Boolean
        ' Check cache first
        If Not _cachedPermissions.ContainsKey(userId) Then
            LoadUserPermissions(userId)
        End If

        Return _cachedPermissions(userId).Contains(permissionKey)
    End Function

    ''' <summary>
    ''' Gets all effective permissions for a user.
    ''' </summary>
    Public Function GetUserPermissions(userId As Integer) As List(Of String)
        If Not _cachedPermissions.ContainsKey(userId) Then
            LoadUserPermissions(userId)
        End If

        Return New List(Of String)(_cachedPermissions(userId))
    End Function

    ''' <summary>
    ''' Gets detailed permission information for a user.
    ''' </summary>
    Public Function GetUserPermissionsDetailed(userId As Integer) As List(Of EffectivePermission)
        Return _permissionRepository.GetEffectivePermissionsDetailed(userId)
    End Function

    ''' <summary>
    ''' Clears the permission cache for a user (call after permission changes).
    ''' </summary>
    Public Sub ClearCache(userId As Integer)
        If _cachedPermissions.ContainsKey(userId) Then
            _cachedPermissions.Remove(userId)
        End If
    End Sub

    ''' <summary>
    ''' Clears all cached permissions.
    ''' </summary>
    Public Sub ClearAllCache()
        _cachedPermissions.Clear()
    End Sub

#End Region

#Region "Permission Management"

    ''' <summary>
    ''' Grants a permission to a user.
    ''' </summary>
    Public Function GrantPermission(userId As Integer, permissionKey As String, grantedBy As Integer) As OperationResult
        Dim result As New OperationResult()

        Try
            ' Verify permission exists
            Dim permission = _permissionRepository.GetByKey(permissionKey)
            If permission Is Nothing Then
                result.IsSuccess = False
                result.ErrorMessage = "Invalid permission key."
                Return result
            End If

            ' Set permission
            _permissionRepository.SetUserPermission(userId, permissionKey, True, grantedBy)

            ' Clear cache
            ClearCache(userId)

            ' Audit log
            _auditService.LogAction("USER_PERMISSION_GRANTED", grantedBy,
                $"Granted permission '{permission.DisplayName}' to user ID {userId}",
                "user_permissions", userId)

            result.IsSuccess = True
            result.Message = $"Permission '{permission.DisplayName}' granted successfully."

        Catch ex As Exception
            result.IsSuccess = False
            result.ErrorMessage = "An error occurred while granting permission."
        End Try

        Return result
    End Function

    ''' <summary>
    ''' Revokes a permission from a user.
    ''' </summary>
    Public Function RevokePermission(userId As Integer, permissionKey As String, revokedBy As Integer) As OperationResult
        Dim result As New OperationResult()

        Try
            ' Verify permission exists
            Dim permission = _permissionRepository.GetByKey(permissionKey)
            If permission Is Nothing Then
                result.IsSuccess = False
                result.ErrorMessage = "Invalid permission key."
                Return result
            End If

            ' Set permission to revoked
            _permissionRepository.SetUserPermission(userId, permissionKey, False, revokedBy)

            ' Clear cache
            ClearCache(userId)

            ' Audit log
            _auditService.LogAction("USER_PERMISSION_REVOKED", revokedBy,
                $"Revoked permission '{permission.DisplayName}' from user ID {userId}",
                "user_permissions", userId)

            result.IsSuccess = True
            result.Message = $"Permission '{permission.DisplayName}' revoked successfully."

        Catch ex As Exception
            result.IsSuccess = False
            result.ErrorMessage = "An error occurred while revoking permission."
        End Try

        Return result
    End Function

    ''' <summary>
    ''' Resets user permissions to role defaults (removes all overrides).
    ''' </summary>
    Public Function ResetToDefaults(userId As Integer, resetBy As Integer) As OperationResult
        Dim result As New OperationResult()

        Try
            ' Clear all user permission overrides
            _permissionRepository.ClearUserPermissions(userId)

            ' Clear cache
            ClearCache(userId)

            ' Audit log
            _auditService.LogAction("USER_PERMISSIONS_RESET", resetBy,
                $"Reset permissions to role defaults for user ID {userId}",
                "user_permissions", userId)

            result.IsSuccess = True
            result.Message = "Permissions reset to role defaults successfully."

        Catch ex As Exception
            result.IsSuccess = False
            result.ErrorMessage = "An error occurred while resetting permissions."
        End Try

        Return result
    End Function

#End Region

#Region "Permission Lookup"

    ''' <summary>
    ''' Gets all available permissions.
    ''' </summary>
    Public Function GetAllPermissions() As List(Of Permission)
        Return _permissionRepository.GetAllPermissions()
    End Function

    ''' <summary>
    ''' Gets permissions grouped by category.
    ''' </summary>
    Public Function GetPermissionsByCategory() As Dictionary(Of String, List(Of Permission))
        Dim allPermissions = _permissionRepository.GetAllPermissions()
        Dim grouped As New Dictionary(Of String, List(Of Permission))

        For Each permission In allPermissions
            If Not grouped.ContainsKey(permission.Category) Then
                grouped(permission.Category) = New List(Of Permission)
            End If
            grouped(permission.Category).Add(permission)
        Next

        Return grouped
    End Function

    ''' <summary>
    ''' Gets role default permissions.
    ''' </summary>
    Public Function GetRoleDefaults(role As String) As List(Of String)
        Return _permissionRepository.GetRoleDefaults(role)
    End Function

#End Region

#Region "Private Methods"

    Private Sub LoadUserPermissions(userId As Integer)
        Dim permissions = _permissionRepository.GetEffectivePermissions(userId)
        _cachedPermissions(userId) = permissions
    End Sub

#End Region

End Class
