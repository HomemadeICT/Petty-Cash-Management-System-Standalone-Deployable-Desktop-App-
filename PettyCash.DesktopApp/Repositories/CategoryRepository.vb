' ============================================================================
' CategoryRepository.vb - Category Data Access (SQLite Edition)
' Petty Cash Management System
' ============================================================================

Imports System.Data
Imports System.Data.SQLite

''' <summary>
''' Repository for Category entity operations.
''' </summary>
Public Class CategoryRepository
    Implements IRepository(Of Category)

#Region "IRepository Implementation"

    Public Function GetById(id As Integer) As Category Implements IRepository(Of Category).GetById
        Dim sql = "SELECT * FROM petty_cash_categories WHERE category_id = @Id"

        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                command.Parameters.AddWithValue("@Id", id)
                Using reader = command.ExecuteReader()
                    If reader.Read() Then Return MapFromReader(reader)
                End Using
            End Using
        End Using
        Return Nothing
    End Function

    Public Function GetAll() As List(Of Category) Implements IRepository(Of Category).GetAll
        Dim categories As New List(Of Category)
        Dim sql = "SELECT * FROM petty_cash_categories ORDER BY category_code"

        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                Using reader = command.ExecuteReader()
                    While reader.Read()
                        categories.Add(MapFromReader(reader))
                    End While
                End Using
            End Using
        End Using
        Return categories
    End Function

    Public Function Add(entity As Category) As Integer Implements IRepository(Of Category).Add
        Dim sql = "INSERT INTO petty_cash_categories (category_code, category_name, created_at)
                   VALUES (@Code, @Name, @CreatedAt);
                   SELECT last_insert_rowid();"

        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                command.Parameters.AddWithValue("@Code", entity.CategoryCode)
                command.Parameters.AddWithValue("@Name", entity.CategoryName)
                command.Parameters.AddWithValue("@CreatedAt", DateTime.Now)
                Return Convert.ToInt32(command.ExecuteScalar())
            End Using
        End Using
    End Function

    Public Sub Update(entity As Category) Implements IRepository(Of Category).Update
        Dim sql = "UPDATE petty_cash_categories SET
                   category_code = @Code,
                   category_name = @Name
                   WHERE category_id = @Id"

        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                command.Parameters.AddWithValue("@Id", entity.CategoryId)
                command.Parameters.AddWithValue("@Code", entity.CategoryCode)
                command.Parameters.AddWithValue("@Name", entity.CategoryName)
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub Delete(id As Integer) Implements IRepository(Of Category).Delete
        Dim sql = "DELETE FROM petty_cash_categories WHERE category_id = @Id"

        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                command.Parameters.AddWithValue("@Id", id)
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

#End Region

#Region "Custom Methods"

    Public Function GetByCode(code As String) As Category
        Dim sql = "SELECT * FROM petty_cash_categories WHERE category_code = @Code"

        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                command.Parameters.AddWithValue("@Code", code)
                Using reader = command.ExecuteReader()
                    If reader.Read() Then Return MapFromReader(reader)
                End Using
            End Using
        End Using
        Return Nothing
    End Function

    ''' <summary>
    ''' Returns True if any non-deleted expense entry references this category code.
    ''' Used to prevent deletion of in-use categories.
    ''' </summary>
    Public Function IsCategoryInUse(categoryCode As String) As Boolean
        Dim sql = "SELECT COUNT(*) FROM petty_cash_entries WHERE category_code = @Code AND is_deleted = 0"
        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                command.Parameters.AddWithValue("@Code", categoryCode)
                Return Convert.ToInt32(command.ExecuteScalar()) > 0
            End Using
        End Using
    End Function

#End Region

#Region "Private Methods"

    Private Function MapFromReader(reader As SQLiteDataReader) As Category
        Return New Category With {
            .CategoryId = Convert.ToInt32(reader("category_id")),
            .CategoryCode = reader("category_code").ToString(),
            .CategoryName = reader("category_name").ToString()
        }
    End Function

#End Region

End Class
