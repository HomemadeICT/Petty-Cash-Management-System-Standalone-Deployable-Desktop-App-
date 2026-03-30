' ============================================================================
' ExpenseRepository.vb - Expense Data Access (SQLite Edition)
' Petty Cash Management System
' ============================================================================

Imports System.Data
Imports System.Data.SQLite

''' <summary>
''' Repository for Expense entity operations.
''' </summary>
Public Class ExpenseRepository
    Implements IRepository(Of Expense)

#Region "IRepository Implementation"

    Public Function GetById(id As Integer) As Expense Implements IRepository(Of Expense).GetById
        Dim sql = "SELECT * FROM petty_cash_entries WHERE entry_id = @EntryId"

        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                command.Parameters.AddWithValue("@EntryId", id)
                Using reader = command.ExecuteReader()
                    If reader.Read() Then Return MapFromReader(reader)
                End Using
            End Using
        End Using
        Return Nothing
    End Function

    Public Function GetAll() As List(Of Expense) Implements IRepository(Of Expense).GetAll
        Dim expenses As New List(Of Expense)
        Dim sql = "SELECT * FROM petty_cash_entries WHERE is_deleted = 0 ORDER BY entry_date DESC"

        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                Using reader = command.ExecuteReader()
                    While reader.Read()
                        expenses.Add(MapFromReader(reader))
                    End While
                End Using
            End Using
        End Using
        Return expenses
    End Function

    Public Function Add(entity As Expense) As Integer Implements IRepository(Of Expense).Add
        Dim sql = "INSERT INTO petty_cash_entries (entry_date, bill_no, category_code, description, amount, report_month, report_year, created_by, created_at)
                   VALUES (@EntryDate, @BillNo, @CategoryCode, @Description, @Amount, @ReportMonth, @ReportYear, @CreatedBy, @CreatedAt);
                   SELECT last_insert_rowid();"

        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                command.Parameters.AddWithValue("@EntryDate", entity.EntryDate.Date)
                command.Parameters.AddWithValue("@BillNo", entity.BillNo)
                command.Parameters.AddWithValue("@CategoryCode", entity.CategoryCode)
                command.Parameters.AddWithValue("@Description", entity.Description)
                command.Parameters.AddWithValue("@Amount", entity.Amount)
                command.Parameters.AddWithValue("@ReportMonth", entity.ReportMonth)
                command.Parameters.AddWithValue("@ReportYear", entity.ReportYear)
                command.Parameters.AddWithValue("@CreatedBy", entity.CreatedBy)
                command.Parameters.AddWithValue("@CreatedAt", DateTime.Now)
                Return Convert.ToInt32(command.ExecuteScalar())
            End Using
        End Using
    End Function

    Public Sub Update(entity As Expense) Implements IRepository(Of Expense).Update
        ' NOTE: report_month and report_year are deliberately NOT updated here.
        ' The assigned report month is set once on creation and preserved on edits
        ' to prevent records from "moving" between months when the entry date changes.
        Dim sql = "UPDATE petty_cash_entries SET
                   entry_date = @EntryDate,
                   bill_no = @BillNo,
                   category_code = @CategoryCode,
                   description = @Description,
                   amount = @Amount,
                   report_month = @ReportMonth,
                   report_year = @ReportYear,
                   updated_at = @UpdatedAt
                   WHERE entry_id = @EntryId"

        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                command.Parameters.AddWithValue("@EntryId", entity.EntryId)
                command.Parameters.AddWithValue("@EntryDate", entity.EntryDate.Date)
                command.Parameters.AddWithValue("@BillNo", entity.BillNo)
                command.Parameters.AddWithValue("@CategoryCode", entity.CategoryCode)
                command.Parameters.AddWithValue("@Description", entity.Description)
                command.Parameters.AddWithValue("@Amount", entity.Amount)
                command.Parameters.AddWithValue("@ReportMonth", entity.ReportMonth)
                command.Parameters.AddWithValue("@ReportYear", entity.ReportYear)
                command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now)
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub Delete(id As Integer) Implements IRepository(Of Expense).Delete
        ' Soft delete
        Dim sql = "UPDATE petty_cash_entries SET is_deleted = 1, deleted_at = @DeletedAt WHERE entry_id = @EntryId"

        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                command.Parameters.AddWithValue("@EntryId", id)
                command.Parameters.AddWithValue("@DeletedAt", DateTime.Now)
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

#End Region

#Region "Custom Methods"

    Public Function GetByMonth(year As Integer, month As Integer) As List(Of Expense)
        Dim expenses As New List(Of Expense)
        ' Query by report_month/report_year — the assigned report period,
        ' NOT the entry_date month. This enables cross-month posting.
        Dim sql = "SELECT * FROM petty_cash_entries 
                   WHERE report_year = @Year 
                   AND report_month = @Month 
                   AND is_deleted = 0 
                   ORDER BY entry_date ASC"

        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                command.Parameters.AddWithValue("@Year", year)
                command.Parameters.AddWithValue("@Month", month)
                Using reader = command.ExecuteReader()
                    While reader.Read()
                        expenses.Add(MapFromReader(reader))
                    End While
                End Using
            End Using
        End Using
        Return expenses
    End Function

    Public Function GetMonthlyTotal(year As Integer, month As Integer) As Decimal
        Dim sql = "SELECT ifnull(SUM(amount), 0) FROM petty_cash_entries 
                   WHERE report_year = @Year 
                   AND report_month = @Month 
                   AND is_deleted = 0"

        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                command.Parameters.AddWithValue("@Year", year)
                command.Parameters.AddWithValue("@Month", month)
                Return Convert.ToDecimal(command.ExecuteScalar())
            End Using
        End Using
    End Function

    Public Function GetCategoryTotals(year As Integer, month As Integer) As Dictionary(Of String, Decimal)
        Dim totals As New Dictionary(Of String, Decimal)
        Dim sql = "SELECT category_code, SUM(amount) as total 
                   FROM petty_cash_entries 
                   WHERE report_year = @Year 
                   AND report_month = @Month 
                   AND is_deleted = 0 
                   GROUP BY category_code"

        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                command.Parameters.AddWithValue("@Year", year)
                command.Parameters.AddWithValue("@Month", month)
                Using reader = command.ExecuteReader()
                    While reader.Read()
                        totals.Add(reader("category_code").ToString(), Convert.ToDecimal(reader("total")))
                    End While
                End Using
            End Using
        End Using
        Return totals
    End Function

    Public Function GetByBillNumber(billNo As String, year As Integer, month As Integer) As Expense
        Dim sql = "SELECT * FROM petty_cash_entries 
                   WHERE bill_no = @BillNo 
                   AND report_year = @Year 
                   AND report_month = @Month 
                   AND is_deleted = 0 
                   LIMIT 1"

        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                command.Parameters.AddWithValue("@BillNo", billNo)
                command.Parameters.AddWithValue("@Year", year)
                command.Parameters.AddWithValue("@Month", month)
                Using reader = command.ExecuteReader()
                    If reader.Read() Then Return MapFromReader(reader)
                End Using
            End Using
        End Using
        Return Nothing
    End Function

    Public Sub SoftDelete(id As Integer)
        Dim sql = "UPDATE petty_cash_entries SET is_deleted = 1, deleted_at = @DeletedAt WHERE entry_id = @Id"
        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                command.Parameters.AddWithValue("@Id", id)
                command.Parameters.AddWithValue("@DeletedAt", DateTime.Now)
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Function GetMonthlyCount(year As Integer, month As Integer) As Integer
        Dim sql = "SELECT COUNT(*) FROM petty_cash_entries 
                   WHERE report_year = @Year 
                   AND report_month = @Month 
                   AND is_deleted = 0"
        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                command.Parameters.AddWithValue("@Year", year)
                command.Parameters.AddWithValue("@Month", month)
                Return Convert.ToInt32(command.ExecuteScalar())
            End Using
        End Using
    End Function

    Public Function GetHighValueBillCount(year As Integer, month As Integer, threshold As Decimal) As Integer
        Dim sql = "SELECT COUNT(*) FROM petty_cash_entries 
                   WHERE report_year = @Year 
                   AND report_month = @Month 
                   AND amount >= @Threshold 
                   AND is_deleted = 0"
        Using connection = DbContext.GetConnection()
            Using command As New SQLiteCommand(sql, connection)
                command.Parameters.AddWithValue("@Year", year)
                command.Parameters.AddWithValue("@Month", month)
                command.Parameters.AddWithValue("@Threshold", threshold)
                Return Convert.ToInt32(command.ExecuteScalar())
            End Using
        End Using
    End Function

#End Region

#Region "Private Methods"

    Private Function MapFromReader(reader As SQLiteDataReader) As Expense
        Return New Expense With {
            .EntryId = Convert.ToInt32(reader("entry_id")),
            .EntryDate = Convert.ToDateTime(reader("entry_date")),
            .BillNo = reader("bill_no").ToString(),
            .CategoryCode = reader("category_code").ToString(),
            .Description = reader("description").ToString(),
            .Amount = Convert.ToDecimal(reader("amount")),
            .ReportMonth = If(IsDBNull(reader("report_month")), 0, Convert.ToInt32(reader("report_month"))),
            .ReportYear = If(IsDBNull(reader("report_year")), 0, Convert.ToInt32(reader("report_year"))),
            .CreatedBy = Convert.ToInt32(reader("created_by")),
            .CreatedAt = Convert.ToDateTime(reader("created_at")),
            .IsDeleted = Convert.ToBoolean(reader("is_deleted"))
        }
    End Function

#End Region

End Class
