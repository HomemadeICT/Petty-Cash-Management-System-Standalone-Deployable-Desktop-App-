' ============================================================================
' UserManagementService.vb - User Management Service
' Petty Cash Management System
' ============================================================================
' Purpose: Handles user registration, updates, and management operations
' Layer: Business Logic Layer
' Dependencies: UserRepository, PermissionService, AuditService
' ============================================================================

Imports BCrypt.Net

''' <summary>
''' Provides user management services for administrators.
''' </summary>
Public Class UserManagementService

#Region "Private Fields"
    Private ReadOnly _userRepository As UserRepository
    Private ReadOnly _permissionService As PermissionService
    Private ReadOnly _auditService As AuditService
#End Region

#Region "Constructor"
    Public Sub New(userRepository As UserRepository, permissionService As PermissionService, auditService As AuditService)
        _userRepository = userRepository
        _permissionService = permissionService
        _auditService = auditService
    End Sub
#End Region

#Region "User CRUD Operations"

    ''' <summary>
    ''' Registers a new user.
    ''' </summary>
    Public Function RegisterUser(user As User, password As String, registeredBy As Integer) As OperationResult
        Dim result As New OperationResult()

        Try
            ' Validate username uniqueness
            If _userRepository.UsernameExists(user.Username) Then
                result.IsSuccess = False
                result.ErrorMessage = "Username already exists. Please choose a different username."
                Return result
            End If

            ' Validate password
            If String.IsNullOrWhiteSpace(password) OrElse password.Length < 4 Then
                result.IsSuccess = False
                result.ErrorMessage = "Password must be at least 4 characters."
                Return result
            End If

            ' Validate role
            If Not IsValidRole(user.Role) Then
                result.IsSuccess = False
                result.ErrorMessage = "Invalid role. Must be Admin, Clerk, or Viewer."
                Return result
            End If

            ' Hash password
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
            user.CreatedAt = DateTime.Now
            user.IsActive = True

            ' Add user
            Dim userId = _userRepository.Add(user)

            ' Audit log
            _auditService.LogAction("USER_CREATED", registeredBy,
                $"Created user: {user.Username} ({user.FullName}), Role: {user.Role}",
                "users", userId)

            result.IsSuccess = True
            result.Message = $"User '{user.Username}' registered successfully."

        Catch ex As Exception
            result.IsSuccess = False
            result.ErrorMessage = "An error occurred while registering the user."
        End Try

        Return result
    End Function

    ''' <summary>
    ''' Updates an existing user's information.
    ''' </summary>
    Public Function UpdateUser(user As User, updatedBy As Integer) As OperationResult
        Dim result As New OperationResult()

        Try
            ' Get original user
            Dim originalUser = _userRepository.GetById(user.UserId)
            If originalUser Is Nothing Then
                result.IsSuccess = False
                result.ErrorMessage = "User not found."
                Return result
            End If

            ' Validate role
            If Not IsValidRole(user.Role) Then
                result.IsSuccess = False
                result.ErrorMessage = "Invalid role. Must be Admin, Clerk, or Viewer."
                Return result
            End If

            ' Update user
            _userRepository.Update(user)

            ' If role changed, clear permission cache
            If originalUser.Role <> user.Role Then
                _permissionService.ClearCache(user.UserId)
            End If

            ' Audit log
            Dim changes = GetUserChangeDetails(originalUser, user)
            _auditService.LogAction("USER_UPDATED", updatedBy,
                $"Updated user ID {user.UserId}: {changes}",
                "users", user.UserId)

            result.IsSuccess = True
            result.Message = $"User '{user.Username}' updated successfully."

        Catch ex As Exception
            result.IsSuccess = False
            result.ErrorMessage = "An error occurred while updating the user."
        End Try

        Return result
    End Function

    ''' <summary>
    ''' Deactivates a user account.
    ''' </summary>
    Public Function DeactivateUser(userId As Integer, deactivatedBy As Integer) As OperationResult
        Dim result As New OperationResult()

        Try
            Dim user = _userRepository.GetById(userId)
            If user Is Nothing Then
                result.IsSuccess = False
                result.ErrorMessage = "User not found."
                Return result
            End If

            If Not user.IsActive Then
                result.IsSuccess = False
                result.ErrorMessage = "User is already deactivated."
                Return result
            End If

            ' Prevent deactivating the last admin
            If user.IsAdmin Then
                Dim activeAdmins = _userRepository.GetActiveUsers().Where(Function(u) u.IsAdmin).Count()
                If activeAdmins <= 1 Then
                    result.IsSuccess = False
                    result.ErrorMessage = "Cannot deactivate the last active admin user."
                    Return result
                End If
            End If

            ' Deactivate
            _userRepository.Delete(userId) ' This is a soft delete (sets is_active = 0)

            ' Clear permission cache
            _permissionService.ClearCache(userId)

            ' Audit log
            _auditService.LogAction("USER_DEACTIVATED", deactivatedBy,
                $"Deactivated user: {user.Username} ({user.FullName})",
                "users", userId)

            result.IsSuccess = True
            result.Message = $"User '{user.Username}' deactivated successfully."

        Catch ex As Exception
            result.IsSuccess = False
            result.ErrorMessage = "An error occurred while deactivating the user."
        End Try

        Return result
    End Function

    ''' <summary>
    ''' Reactivates a deactivated user account.
    ''' </summary>
    Public Function ReactivateUser(userId As Integer, reactivatedBy As Integer) As OperationResult
        Dim result As New OperationResult()

        Try
            Dim user = _userRepository.GetById(userId)
            If user Is Nothing Then
                result.IsSuccess = False
                result.ErrorMessage = "User not found."
                Return result
            End If

            If user.IsActive Then
                result.IsSuccess = False
                result.ErrorMessage = "User is already active."
                Return result
            End If

            ' Reactivate
            user.IsActive = True
            _userRepository.Update(user)

            ' Audit log
            _auditService.LogAction("USER_REACTIVATED", reactivatedBy,
                $"Reactivated user: {user.Username} ({user.FullName})",
                "users", userId)

            result.IsSuccess = True
            result.Message = $"User '{user.Username}' reactivated successfully."

        Catch ex As Exception
            result.IsSuccess = False
            result.ErrorMessage = "An error occurred while reactivating the user."
        End Try

        Return result
    End Function

    ''' <summary>
    ''' Resets a user's password (Admin function).
    ''' </summary>
    Public Function ResetPassword(userId As Integer, newPassword As String, resetBy As Integer) As OperationResult
        Dim result As New OperationResult()

        Try
            Dim user = _userRepository.GetById(userId)
            If user Is Nothing Then
                result.IsSuccess = False
                result.ErrorMessage = "User not found."
                Return result
            End If

            ' Validate password
            If String.IsNullOrWhiteSpace(newPassword) OrElse newPassword.Length < 4 Then
                result.IsSuccess = False
                result.ErrorMessage = "Password must be at least 4 characters."
                Return result
            End If

            ' Hash and update password
            Dim newHash = BCrypt.Net.BCrypt.HashPassword(newPassword)
            _userRepository.UpdatePassword(userId, newHash)

            ' Audit log
            _auditService.LogAction("USER_PASSWORD_RESET", resetBy,
                $"Reset password for user: {user.Username} ({user.FullName})",
                "users", userId)

            result.IsSuccess = True
            result.Message = $"Password reset successfully for user '{user.Username}'."

        Catch ex As Exception
            result.IsSuccess = False
            result.ErrorMessage = "An error occurred while resetting the password."
        End Try

        Return result
    End Function

#End Region

#Region "User Retrieval"

    ''' <summary>
    ''' Gets all users (active and inactive).
    ''' </summary>
    Public Function GetAllUsers() As List(Of User)
        Return _userRepository.GetAll()
    End Function

    ''' <summary>
    ''' Gets only active users.
    ''' </summary>
    Public Function GetActiveUsers() As List(Of User)
        Return _userRepository.GetActiveUsers()
    End Function

    ''' <summary>
    ''' Gets a user by ID.
    ''' </summary>
    Public Function GetUserById(userId As Integer) As User
        Return _userRepository.GetById(userId)
    End Function

#End Region

#Region "Private Methods"

    Private Function IsValidRole(role As String) As Boolean
        Return role = "Admin" OrElse role = "Clerk" OrElse role = "Viewer"
    End Function

    Private Function GetUserChangeDetails(original As User, updated As User) As String
        Dim changes As New List(Of String)

        If original.FullName <> updated.FullName Then
            changes.Add($"Name: {original.FullName} → {updated.FullName}")
        End If
        If original.Role <> updated.Role Then
            changes.Add($"Role: {original.Role} → {updated.Role}")
        End If
        If original.Email <> updated.Email Then
            changes.Add($"Email: {original.Email} → {updated.Email}")
        End If
        If original.WhatsAppNo <> updated.WhatsAppNo Then
            changes.Add($"WhatsApp: {original.WhatsAppNo} → {updated.WhatsAppNo}")
        End If
        If original.IsActive <> updated.IsActive Then
            changes.Add($"Active: {original.IsActive} → {updated.IsActive}")
        End If

        Return If(changes.Count > 0, String.Join(", ", changes), "No changes")
    End Function

#End Region

End Class
