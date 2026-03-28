' ============================================================================
' AuditLogRepository.vb - Audit Trail Access (SQLite Edition)
' Petty Cash Management System
' ============================================================================

Imports System.Data
Imports System.Data.SQLite

''' <summary>
''' Repository for Audit Log entity operations.
''' </summary>
Public Class AuditLogRepository
    Implements IRepository(Of AuditLog)

#Region "IRepository Implementation"

    Public Function GetById(id As Integer) As AuditLog Implements IRepository(Of AuditLog).GetById
        Dim sql = "SELECT * FROM audit_log WHERE audit_id = @AuditId"

        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                command.Parameters.AddWithValue("@AuditId", id)
                Using reader = command.ExecuteReader()
                    If reader.Read() Then Return MapFromReader(reader)
                End Using
            End Using
        End Using
        Return Nothing
    End Function

    Public Function GetAll() As List(Of AuditLog) Implements IRepository(Of AuditLog).GetAll
        Dim logs As New List(Of AuditLog)
        Dim sql = "SELECT * FROM audit_log ORDER BY action_timestamp DESC LIMIT 1000"

        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                Using reader = command.ExecuteReader()
                    While reader.Read()
                        logs.Add(MapFromReader(reader))
                    End While
                End Using
            End Using
        End Using
        Return logs
    End Function

    Public Function Add(entity As AuditLog) As Integer Implements IRepository(Of AuditLog).Add
        Dim sql = "INSERT INTO audit_log (user_id, action_type, table_name, record_id, details, action_timestamp)
                   VALUES (@UserId, @ActionType, @TableName, @RecordId, @Details, @Timestamp);
                   SELECT last_insert_rowid();"

        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                command.Parameters.AddWithValue("@UserId", entity.UserId)
                command.Parameters.AddWithValue("@ActionType", entity.ActionType)
                command.Parameters.AddWithValue("@TableName", If(entity.TableName IsNot Nothing, entity.TableName, DBNull.Value))
                command.Parameters.AddWithValue("@RecordId", If(entity.RecordId > 0, entity.RecordId, DBNull.Value))
                command.Parameters.AddWithValue("@Details", If(entity.Details IsNot Nothing, entity.Details, DBNull.Value))
                command.Parameters.AddWithValue("@Timestamp", DateTime.Now)
                Return Convert.ToInt32(command.ExecuteScalar())
            End Using
        End Using
    End Function

    Public Sub Update(entity As AuditLog) Implements IRepository(Of AuditLog).Update
        ' Audit logs are typically immutable, but interface requires it
        Throw New NotSupportedException("Audit logs cannot be modified.")
    End Sub

    Public Sub Delete(id As Integer) Implements IRepository(Of AuditLog).Delete
        ' Audit logs are typically never deleted
        Throw New NotSupportedException("Audit logs cannot be deleted.")
    End Sub

    Public Function GetRecent(count As Integer) As List(Of AuditLog)
        Return GetRecentLogs(count)
    End Function

    Public Function GetFiltered(startDate As DateTime, endDate As DateTime, userId As Integer?, actionType As String) As List(Of AuditLog)
        Dim logs As New List(Of AuditLog)
        Dim sql = "SELECT * FROM audit_log WHERE action_timestamp BETWEEN @Start AND @End"
        
        If userId.HasValue Then sql &= " AND user_id = @UserId"
        If Not String.IsNullOrEmpty(actionType) Then sql &= " AND action_type = @ActionType"
        
        sql &= " ORDER BY action_timestamp DESC"

        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                command.Parameters.AddWithValue("@Start", startDate)
                command.Parameters.AddWithValue("@End", endDate)
                If userId.HasValue Then command.Parameters.AddWithValue("@UserId", userId.Value)
                If Not String.IsNullOrEmpty(actionType) Then command.Parameters.AddWithValue("@ActionType", actionType)
                
                Using reader = command.ExecuteReader()
                    While reader.Read()
                        logs.Add(MapFromReader(reader))
                    End While
                End Using
            End Using
        End Using
        Return logs
    End Function

    Public Function GetByUser(userId As Integer) As List(Of AuditLog)
        Dim logs As New List(Of AuditLog)
        Dim sql = "SELECT * FROM audit_log WHERE user_id = @UserId ORDER BY action_timestamp DESC"
        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                command.Parameters.AddWithValue("@UserId", userId)
                Using reader = command.ExecuteReader()
                    While reader.Read()
                        logs.Add(MapFromReader(reader))
                    End While
                End Using
            End Using
        End Using
        Return logs
    End Function

    Public Function GetByRecord(tableName As String, recordId As Integer) As List(Of AuditLog)
        Dim logs As New List(Of AuditLog)
        Dim sql = "SELECT * FROM audit_log WHERE table_name = @TableName AND record_id = @RecordId ORDER BY action_timestamp DESC"
        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                command.Parameters.AddWithValue("@TableName", tableName)
                command.Parameters.AddWithValue("@RecordId", recordId)
                Using reader = command.ExecuteReader()
                    While reader.Read()
                        logs.Add(MapFromReader(reader))
                    End While
                End Using
            End Using
        End Using
        Return logs
    End Function

#End Region

#Region "Custom Methods"

    Public Function GetRecentLogs(count As Integer) As List(Of AuditLog)
        Dim logs As New List(Of AuditLog)
        Dim sql = $"SELECT * FROM audit_log ORDER BY action_timestamp DESC LIMIT @Count"

        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                command.Parameters.AddWithValue("@Count", count)
                Using reader = command.ExecuteReader()
                    While reader.Read()
                        logs.Add(MapFromReader(reader))
                    End While
                End Using
            End Using
        End Using
        Return logs
    End Function

#End Region

#Region "Private Methods"

    Private Function MapFromReader(reader As SQLiteDataReader) As AuditLog
        Return New AuditLog With {
            .AuditId = Convert.ToInt32(reader("audit_id")),
            .UserId = Convert.ToInt32(reader("user_id")),
            .ActionType = reader("action_type").ToString(),
            .TableName = If(IsDBNull(reader("table_name")), Nothing, reader("table_name").ToString()),
            .RecordId = If(IsDBNull(reader("record_id")), 0, Convert.ToInt32(reader("record_id"))),
            .Details = If(IsDBNull(reader("details")), Nothing, reader("details").ToString()),
            .ActionTimestamp = Convert.ToDateTime(reader("action_timestamp"))
        }
    End Function

#End Region

End Class
