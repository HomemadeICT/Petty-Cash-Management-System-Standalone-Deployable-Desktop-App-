' ============================================================================
' PermissionRepository.vb - Access Control Data (SQLite Edition)
' Petty Cash Management System
' ============================================================================

Imports System.Data
Imports System.Data.SQLite

''' <summary>
''' Repository for managing user permissions and role defaults.
''' </summary>
Public Class PermissionRepository

#Region "Effective Permissions"

    ''' <summary>
    ''' Gets effective permissions for a user (Role defaults + User overrides).
    ''' </summary>
    Public Function GetEffectivePermissions(userId As Integer) As List(Of String)
        Dim sql = "
            SELECT DISTINCT p.permission_key
            FROM permissions p
            LEFT JOIN role_default_permissions rdp ON p.permission_id = rdp.permission_id
            LEFT JOIN users u ON u.role = rdp.role_name
            LEFT JOIN user_permissions up ON p.permission_id = up.permission_id AND up.user_id = @UserId
            WHERE u.user_id = @UserId
              AND (up.is_granted = 1 OR (up.is_granted IS NULL AND rdp.is_granted = 1))"

        Dim permissions As New List(Of String)
        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                command.Parameters.AddWithValue("@UserId", userId)
                Using reader = command.ExecuteReader()
                    While reader.Read()
                        permissions.Add(reader("permission_key").ToString())
                    End While
                End Using
            End Using
        End Using
        Return permissions
    End Function

    ''' <summary>
    ''' Gets detailed effective permission information for UI management.
    ''' </summary>
    Public Function GetEffectivePermissionsDetailed(userId As Integer) As List(Of EffectivePermission)
        Dim sql = "
            SELECT p.permission_key, p.description as display_name, 
                   rdp.is_granted as role_default, 
                   up.is_granted as user_override
            FROM permissions p
            JOIN users u ON u.user_id = @UserId
            JOIN role_default_permissions rdp ON p.permission_id = rdp.permission_id AND rdp.role_name = u.role
            LEFT JOIN user_permissions up ON p.permission_id = up.permission_id AND up.user_id = @UserId"

        Dim results As New List(Of EffectivePermission)
        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                command.Parameters.AddWithValue("@UserId", userId)
                Using reader = command.ExecuteReader()
                    While reader.Read()
                        Dim isOverridden = reader("user_override") IsNot DBNull.Value
                        Dim isGranted = If(isOverridden, Convert.ToBoolean(reader("user_override")), Convert.ToBoolean(reader("role_default")))
                        
                        Dim ep As New EffectivePermission With {
                            .PermissionKey = reader("permission_key").ToString(),
                            .DisplayName = reader("display_name").ToString(),
                            .HasPermission = isGranted,
                            .PermissionSource = If(isOverridden, "User Override", "Role Default")
                        }
                        results.Add(ep)
                    End While
                End Using
            End Using
        End Using
        Return results
    End Function

#End Region

#Region "Permission Management"

    Public Function GetByKey(permissionKey As String) As Permission
        Dim sql = "SELECT * FROM permissions WHERE permission_key = @Key"
        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                command.Parameters.AddWithValue("@Key", permissionKey)
                Using reader = command.ExecuteReader()
                    If reader.Read() Then
                        Return New Permission With {
                            .PermissionId = Convert.ToInt32(reader("permission_id")),
                            .PermissionKey = reader("permission_key").ToString(),
                            .DisplayName = reader("description").ToString(),
                            .Description = reader("description").ToString()
                        }
                    End If
                End Using
            End Using
        End Using
        Return Nothing
    End Function

    Public Sub SetUserPermission(userId As Integer, permissionKey As String, isGranted As Boolean, grantedBy As Integer)
        Dim sql = "
            INSERT INTO user_permissions (user_id, permission_id, is_granted)
            SELECT @UserId, permission_id, @IsGranted
            FROM permissions
            WHERE permission_key = @PermissionKey
            ON CONFLICT(user_id, permission_id) DO UPDATE SET is_granted = excluded.is_granted"

        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                command.Parameters.AddWithValue("@UserId", userId)
                command.Parameters.AddWithValue("@PermissionKey", permissionKey)
                command.Parameters.AddWithValue("@IsGranted", If(isGranted, 1, 0))
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub ClearUserPermissions(userId As Integer)
        Dim sql = "DELETE FROM user_permissions WHERE user_id = @UserId"
        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                command.Parameters.AddWithValue("@UserId", userId)
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

#End Region

#Region "Lookups"

    Public Function GetAllPermissions() As List(Of Permission)
        Dim sql = "SELECT * FROM permissions ORDER BY permission_key"
        Dim results As New List(Of Permission)
        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                Using reader = command.ExecuteReader()
                    While reader.Read()
                        results.Add(New Permission With {
                            .PermissionId = Convert.ToInt32(reader("permission_id")),
                            .PermissionKey = reader("permission_key").ToString(),
                            .DisplayName = reader("description").ToString(),
                            .Description = reader("description").ToString()
                        })
                    End While
                End Using
            End Using
        End Using
        Return results
    End Function

    Public Function GetRoleDefaults(roleName As String) As List(Of String)
        Dim sql = "
            SELECT p.permission_key
            FROM role_default_permissions rdp
            JOIN permissions p ON rdp.permission_id = p.permission_id
            WHERE rdp.role_name = @Role AND rdp.is_granted = 1"

        Dim results As New List(Of String)
        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                command.Parameters.AddWithValue("@Role", roleName)
                Using reader = command.ExecuteReader()
                    While reader.Read()
                        results.Add(reader("permission_key").ToString())
                    End While
                End Using
            End Using
        End Using
        Return results
    End Function

#End Region

End Class
