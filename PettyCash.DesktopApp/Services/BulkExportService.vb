' ============================================================================
' BulkExportService.vb - Bulk Export Service
' Petty Cash Management System
' ============================================================================
' Purpose: Handles bulk export of expenses to XLSX files for multiple months
' Layer: Business Logic Layer
' Dependencies: ExpenseRepository, CategoryRepository, ClosedXML
' ============================================================================

Imports ClosedXML.Excel
Imports System.IO

''' <summary>
''' Service for exporting petty cash data to XLSX files.
''' </summary>
Public Class BulkExportService

#Region "Private Fields"
    Private ReadOnly _expenseRepository As ExpenseRepository
    Private ReadOnly _categoryRepository As CategoryRepository
#End Region

#Region "Constructor"
    Public Sub New(expenseRepository As ExpenseRepository, categoryRepository As CategoryRepository)
        _expenseRepository = expenseRepository
        _categoryRepository = categoryRepository
    End Sub
#End Region

#Region "Public Methods"

    ''' <summary>
    ''' Exports expenses for selected months to a single XLSX file with multiple sheets.
    ''' </summary>
    ''' <param name="year">The year.</param>
    ''' <param name="months">List of months to export (1-12).</param>
    ''' <param name="outputPath">Full path where the XLSX file will be saved.</param>
    ''' <returns>OperationResult with success/failure information.</returns>
    Public Function ExportMultipleMonthsToExcel(year As Integer, months As List(Of Integer), outputPath As String) As OperationResult
        Try
            If months Is Nothing OrElse months.Count = 0 Then
                Return OperationResult.Failure("Please select at least one month to export.")
            End If

            ' Create workbook
            Dim workbook = New XLWorkbook()

            ' Add a summary sheet first
            AddSummarySheet(workbook, year, months)

            ' Add sheet for each month
            For Each mon In months.OrderBy(Function(m) m)
                Dim expenses = _expenseRepository.GetByMonth(year, mon)
                If expenses.Count > 0 Then
                    AddMonthSheet(workbook, year, mon, expenses)
                End If
            Next

            ' Save workbook
            workbook.SaveAs(outputPath)
            workbook.Dispose()

            Return OperationResult.Success($"Exported {months.Count} months to {outputPath}")
        Catch ex As Exception
            Return OperationResult.Failure($"Export failed: {ex.Message}")
        End Try
    End Function

    ''' <summary>
    ''' Exports expenses for a single month to XLSX.
    ''' </summary>
    ''' <param name="year">The year.</param>
    ''' <param name="month">The month (1-12).</param>
    ''' <param name="outputPath">Full path where the XLSX file will be saved.</param>
    ''' <returns>OperationResult with success/failure information.</returns>
    Public Function ExportSingleMonthToExcel(year As Integer, month As Integer, outputPath As String) As OperationResult
        Try
            Dim expenses = _expenseRepository.GetByMonth(year, month)
            
            If expenses.Count = 0 Then
                Return OperationResult.Failure($"No expenses found for {New DateTime(year, month, 1):MMMM yyyy}.")
            End If

            Dim workbook = New XLWorkbook()
            AddMonthSheet(workbook, year, month, expenses)

            workbook.SaveAs(outputPath)
            workbook.Dispose()

            Return OperationResult.Success($"Exported {month}/{year} to {outputPath}")
        Catch ex As Exception
            Return OperationResult.Failure($"Export failed: {ex.Message}")
        End Try
    End Function

#End Region

#Region "Private Methods"

    ''' <summary>
    ''' Adds a summary sheet to the workbook showing totals for all selected months.
    ''' </summary>
    Private Sub AddSummarySheet(workbook As XLWorkbook, year As Integer, months As List(Of Integer))
        Dim sheet = workbook.Worksheets.Add("Summary")
        
        ' Set column widths
        sheet.Column("A").Width = 15
        sheet.Column("B").Width = 15
        sheet.Column("C").Width = 15
        sheet.Column("D").Width = 15

        ' Header
        Dim headerRow = sheet.Row(1)
        headerRow.Cell("A1").Value = "Petty Cash Export Summary"
        headerRow.Cell("A1").Style.Font.Bold = True
        headerRow.Cell("A1").Style.Font.FontSize = 14
        headerRow.Cell("A1").Style.Font.FontColor = XLColor.DarkBlue
        sheet.Range("A1:D1").Merge()

        ' Organization info
        sheet.Cell("A3").Value = "Organization:"
        sheet.Cell("B3").Value = "Ceylon Electricity Board - Haliela"
        sheet.Cell("A4").Value = "Export Date:"
        sheet.Cell("B4").Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm")
        sheet.Cell("A5").Value = "Year:"
        sheet.Cell("B5").Value = year

        ' Column headers for summary table
        Dim summaryRow = 7
        sheet.Cell(summaryRow, 1).Value = "Month"
        sheet.Cell(summaryRow, 2).Value = "Total Expenses"
        sheet.Cell(summaryRow, 3).Value = "Bill Count"
        sheet.Cell(summaryRow, 4).Value = "Monthly Limit"

        ' Style header row
        For col = 1 To 4
            Dim cell = sheet.Cell(summaryRow, col)
            cell.Style.Font.Bold = True
            cell.Style.Fill.BackgroundColor = XLColor.LightBlue
            cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
        Next

        ' Add summary data
        Dim dataRow = summaryRow + 1
        Dim grandTotal As Decimal = 0

        For Each mon In months.OrderBy(Function(m) m)
            Dim expenses = _expenseRepository.GetByMonth(year, mon)
            Dim monthTotal = expenses.Sum(Function(e) e.Amount)
            grandTotal += monthTotal

            sheet.Cell(dataRow, 1).Value = New DateTime(year, mon, 1).ToString("MMMM yyyy")
            sheet.Cell(dataRow, 2).Value = monthTotal
            sheet.Cell(dataRow, 2).Style.NumberFormat.Format = "#,##0.00"
            sheet.Cell(dataRow, 3).Value = expenses.Count
            sheet.Cell(dataRow, 4).Value = Constants.MONTHLY_LIMIT
            sheet.Cell(dataRow, 4).Style.NumberFormat.Format = "#,##0.00"

            dataRow += 1
        Next

        ' Add grand total
        dataRow += 1
        sheet.Cell(dataRow, 1).Value = "GRAND TOTAL"
        sheet.Cell(dataRow, 1).Style.Font.Bold = True
        sheet.Cell(dataRow, 2).Value = grandTotal
        sheet.Cell(dataRow, 2).Style.Font.Bold = True
        sheet.Cell(dataRow, 2).Style.NumberFormat.Format = "#,##0.00"
        sheet.Cell(dataRow, 2).Style.Fill.BackgroundColor = XLColor.Yellow
    End Sub

    ''' <summary>
    ''' Adds a month sheet to the workbook with all expenses for that month.
    ''' </summary>
    Private Sub AddMonthSheet(workbook As XLWorkbook, year As Integer, month As Integer, expenses As List(Of Expense))
        Dim monthName = New DateTime(year, month, 1).ToString("MMMM yyyy")
        Dim sheetName = New DateTime(year, month, 1).ToString("MMM-yy")
        
        Dim sheet = workbook.Worksheets.Add(sheetName)

        ' Set column widths
        sheet.Column("A").Width = 12
        sheet.Column("B").Width = 12
        sheet.Column("C").Width = 12
        sheet.Column("D").Width = 30
        sheet.Column("E").Width = 15

        ' Header
        Dim headerRow = sheet.Row(1)
        headerRow.Cell("A1").Value = $"Petty Cash Statement - {monthName}"
        headerRow.Cell("A1").Style.Font.Bold = True
        headerRow.Cell("A1").Style.Font.FontSize = 14
        headerRow.Cell("A1").Style.Font.FontColor = XLColor.DarkBlue
        sheet.Range("A1:E1").Merge()

        ' Organization and metadata
        sheet.Cell("A3").Value = "Organization:"
        sheet.Cell("B3").Value = "Ceylon Electricity Board - Haliela"
        sheet.Cell("A4").Value = "Period:"
        sheet.Cell("B4").Value = monthName

        ' Column headers
        Dim headerRowNum = 6
        sheet.Cell(headerRowNum, 1).Value = "Date"
        sheet.Cell(headerRowNum, 2).Value = "Bill No"
        sheet.Cell(headerRowNum, 3).Value = "Category"
        sheet.Cell(headerRowNum, 4).Value = "Description"
        sheet.Cell(headerRowNum, 5).Value = "Amount (LKR)"

        ' Style header row
        For col = 1 To 5
            Dim cell = sheet.Cell(headerRowNum, col)
            cell.Style.Font.Bold = True
            cell.Style.Fill.BackgroundColor = XLColor.LightBlue
            cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
            cell.Style.Border.BottomBorder = XLBorderStyleValues.Medium
        Next

        ' Add expense rows
        Dim dataRowNum = headerRowNum + 1
        Dim categoryTotals = New Dictionary(Of String, Decimal)

        For Each expense In expenses.OrderBy(Function(e) e.EntryDate).ThenBy(Function(e) e.BillNo)
            sheet.Cell(dataRowNum, 1).Value = expense.EntryDate.ToString("dd/MM/yyyy")
            sheet.Cell(dataRowNum, 2).Value = expense.BillNo
            sheet.Cell(dataRowNum, 3).Value = expense.CategoryCode
            sheet.Cell(dataRowNum, 4).Value = expense.Description
            sheet.Cell(dataRowNum, 5).Value = expense.Amount
            sheet.Cell(dataRowNum, 5).Style.NumberFormat.Format = "#,##0.00"
            sheet.Cell(dataRowNum, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right

            ' Track category totals
            If Not categoryTotals.ContainsKey(expense.CategoryCode) Then
                categoryTotals.Add(expense.CategoryCode, 0)
            End If
            categoryTotals(expense.CategoryCode) += expense.Amount

            dataRowNum += 1
        Next

        ' Add category summary section
        dataRowNum += 1
        sheet.Cell(dataRowNum, 3).Value = "Category Summary:"
        sheet.Cell(dataRowNum, 3).Style.Font.Bold = True
        dataRowNum += 1

        For Each kvp In categoryTotals.OrderBy(Function(x) x.Key)
            sheet.Cell(dataRowNum, 3).Value = kvp.Key
            sheet.Cell(dataRowNum, 4).Value = GetCategoryName(kvp.Key)
            sheet.Cell(dataRowNum, 5).Value = kvp.Value
            sheet.Cell(dataRowNum, 5).Style.NumberFormat.Format = "#,##0.00"
            sheet.Cell(dataRowNum, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
            dataRowNum += 1
        Next

        ' Add totals
        dataRowNum += 1
        sheet.Cell(dataRowNum, 3).Value = "GRAND TOTAL"
        sheet.Cell(dataRowNum, 3).Style.Font.Bold = True
        sheet.Cell(dataRowNum, 5).Value = expenses.Sum(Function(e) e.Amount)
        sheet.Cell(dataRowNum, 5).Style.Font.Bold = True
        sheet.Cell(dataRowNum, 5).Style.NumberFormat.Format = "#,##0.00"
        sheet.Cell(dataRowNum, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
        sheet.Cell(dataRowNum, 5).Style.Fill.BackgroundColor = XLColor.Yellow

        ' Monthly limit info
        dataRowNum += 1
        Dim remaining = Constants.MONTHLY_LIMIT - expenses.Sum(Function(e) e.Amount)
        sheet.Cell(dataRowNum, 3).Value = "Monthly Limit"
        sheet.Cell(dataRowNum, 5).Value = Constants.MONTHLY_LIMIT
        sheet.Cell(dataRowNum, 5).Style.NumberFormat.Format = "#,##0.00"
        dataRowNum += 1
        sheet.Cell(dataRowNum, 3).Value = "Remaining Balance"
        sheet.Cell(dataRowNum, 5).Value = remaining
        sheet.Cell(dataRowNum, 5).Style.NumberFormat.Format = "#,##0.00"
        sheet.Cell(dataRowNum, 5).Style.Fill.BackgroundColor = If(remaining < 0, XLColor.Red, XLColor.LightGreen)
    End Sub

    ''' <summary>
    ''' Gets the category name from category code.
    ''' </summary>
    Private Function GetCategoryName(code As String) As String
        Select Case code
            Case "E5200" : Return "Vehicle Parts"
            Case "E5300" : Return "Office Items"
            Case "E7800" : Return "Physical Hardware"
            Case "E7510" : Return "Treatments & Staff"
            Case Else : Return "Unknown"
        End Select
    End Function

#End Region

End Class
