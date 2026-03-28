' ============================================================================
' UserRepository.vb - User Data Access (SQLite Edition)
' Petty Cash Management System
' ============================================================================

Imports System.Data
Imports System.Data.SQLite

''' <summary>
''' Repository for User entity operations.
''' </summary>
Public Class UserRepository
    Implements IRepository(Of User)

#Region "IRepository Implementation"

    Public Function GetById(id As Integer) As User Implements IRepository(Of User).GetById
        Dim sql = "SELECT * FROM users WHERE user_id = @UserId"

        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                command.Parameters.AddWithValue("@UserId", id)
                Using reader = command.ExecuteReader()
                    If reader.Read() Then Return MapFromReader(reader)
                End Using
            End Using
        End Using
        Return Nothing
    End Function

    Public Function GetAll() As List(Of User) Implements IRepository(Of User).GetAll
        Dim users As New List(Of User)
        Dim sql = "SELECT * FROM users ORDER BY username"

        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                Using reader = command.ExecuteReader()
                    While reader.Read()
                        users.Add(MapFromReader(reader))
                    End While
                End Using
            End Using
        End Using
        Return users
    End Function

    Public Function Add(entity As User) As Integer Implements IRepository(Of User).Add
        Dim sql = "INSERT INTO users (username, password_hash, full_name, role, email, whatsapp_no, created_at, is_active)
                   VALUES (@Username, @PasswordHash, @FullName, @Role, @Email, @WhatsAppNo, @CreatedAt, @IsActive);
                   SELECT last_insert_rowid();"

        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                command.Parameters.AddWithValue("@Username", entity.Username)
                command.Parameters.AddWithValue("@PasswordHash", entity.PasswordHash)
                command.Parameters.AddWithValue("@FullName", entity.FullName)
                command.Parameters.AddWithValue("@Role", entity.Role)
                command.Parameters.AddWithValue("@Email", If(entity.Email, DBNull.Value))
                command.Parameters.AddWithValue("@WhatsAppNo", If(entity.WhatsAppNo, DBNull.Value))
                command.Parameters.AddWithValue("@CreatedAt", DateTime.Now)
                command.Parameters.AddWithValue("@IsActive", True)
                Return Convert.ToInt32(command.ExecuteScalar())
            End Using
        End Using
    End Function

    Public Sub Update(entity As User) Implements IRepository(Of User).Update
        Dim sql = "UPDATE users SET
                   full_name = @FullName,
                   role = @Role,
                   email = @Email,
                   whatsapp_no = @WhatsAppNo,
                   is_active = @IsActive
                   WHERE user_id = @UserId"

        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                command.Parameters.AddWithValue("@UserId", entity.UserId)
                command.Parameters.AddWithValue("@FullName", entity.FullName)
                command.Parameters.AddWithValue("@Role", entity.Role)
                command.Parameters.AddWithValue("@Email", If(entity.Email, DBNull.Value))
                command.Parameters.AddWithValue("@WhatsAppNo", If(entity.WhatsAppNo, DBNull.Value))
                command.Parameters.AddWithValue("@IsActive", entity.IsActive)
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub Delete(id As Integer) Implements IRepository(Of User).Delete
        Dim sql = "UPDATE users SET is_active = 0 WHERE user_id = @UserId"

        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                command.Parameters.AddWithValue("@UserId", id)
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

#End Region

#Region "Custom Methods"

    Public Function GetByUsername(username As String) As User
        Dim sql = "SELECT * FROM users WHERE username = @Username"

        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                command.Parameters.AddWithValue("@Username", username)
                Using reader = command.ExecuteReader()
                    If reader.Read() Then Return MapFromReader(reader)
                End Using
            End Using
        End Using
        Return Nothing
    End Function

    Public Sub UpdatePassword(userId As Integer, passwordHash As String)
        Dim sql = "UPDATE users SET password_hash = @PasswordHash WHERE user_id = @UserId"

        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                command.Parameters.AddWithValue("@UserId", userId)
                command.Parameters.AddWithValue("@PasswordHash", passwordHash)
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Function GetActiveUsers() As List(Of User)
        Dim users As New List(Of User)
        Dim sql = "SELECT * FROM users WHERE is_active = 1 ORDER BY username"

        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                Using reader = command.ExecuteReader()
                    While reader.Read()
                        users.Add(MapFromReader(reader))
                    End While
                End Using
            End Using
        End Using
        Return users
    End Function

    Public Function UsernameExists(username As String) As Boolean
        Dim sql = "SELECT COUNT(*) FROM users WHERE username = @Username"

        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                command.Parameters.AddWithValue("@Username", username)
                Return Convert.ToInt32(command.ExecuteScalar()) > 0
            End Using
        End Using
    End Function

#End Region

#Region "Private Methods"

    Private Function MapFromReader(reader As SQLiteDataReader) As User
        Return New User With {
            .UserId = Convert.ToInt32(reader("user_id")),
            .Username = reader("username").ToString(),
            .PasswordHash = reader("password_hash").ToString(),
            .FullName = reader("full_name").ToString(),
            .Role = reader("role").ToString(),
            .Email = If(reader("email") Is DBNull.Value, Nothing, reader("email").ToString()),
            .WhatsAppNo = If(reader("whatsapp_no") Is DBNull.Value, Nothing, reader("whatsapp_no").ToString()),
            .CreatedAt = Convert.ToDateTime(reader("created_at")),
            .IsActive = Convert.ToBoolean(reader("is_active"))
        }
    End Function

#End Region

End Class
