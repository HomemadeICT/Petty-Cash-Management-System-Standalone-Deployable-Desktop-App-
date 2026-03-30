' ============================================================================
' ReportService.vb - Report Generation Service
' Petty Cash Management System
' ============================================================================
' Purpose: Generates monthly reports, category summaries, and print output
' Layer: Business Logic Layer
' Dependencies: ExpenseRepository, CategoryRepository
' ============================================================================

''' <summary>
''' Generates and manages petty cash reports.
''' </summary>
Public Class ReportService

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
    ''' Generates a complete monthly report with all expenses and summaries.
    ''' </summary>
    ''' <param name="year">The year.</param>
    ''' <param name="month">The month.</param>
    ''' <returns>MonthlyReportDTO with all report data.</returns>
    Public Function GenerateMonthlyReport(year As Integer, month As Integer) As MonthlyReportDTO
        Dim report As New MonthlyReportDTO()

        ' Set report header
        report.Year = year
        report.Month = month
        report.MonthName = New DateTime(year, month, 1).ToString("MMMM yyyy")
        report.GeneratedAt = DateTime.Now
        report.OrganizationName = "Ceylon Electricity Board - Haliela"
        report.ReportTitle = $"Petty Cash Statement - {report.MonthName}"

        ' Get all expenses for the month
        Dim expenses = _expenseRepository.GetByMonth(year, month)
        
        ' Compile unique categories used in this month's expenses
        Dim uniqueCodes = expenses.Select(Function(e) e.CategoryCode).Distinct().OrderBy(Function(c) c).ToList()
        report.Categories = uniqueCodes.Select(Function(code) New CategorySummaryDTO With {
            .CategoryCode = code,
            .CategoryName = GetCategoryName(code)
        }).ToList()

        report.Entries = expenses.Select(Function(e) New ReportEntryDTO With {
            .EntryDate = e.EntryDate,
            .BillNo = e.BillNo,
            .CategoryCode = e.CategoryCode,
            .CategoryName = report.Categories.FirstOrDefault(Function(c) c.CategoryCode = e.CategoryCode)?.CategoryName,
            .Description = e.Description,
            .Amount = e.Amount
        }).OrderBy(Function(e) e.EntryDate).ThenBy(Function(e) e.BillNo).ToList()

        ' Calculate totals
        report.GrandTotal = expenses.Sum(Function(e) e.Amount)
        report.MonthlyLimit = Constants.MONTHLY_LIMIT
        report.RemainingBalance = report.MonthlyLimit - report.GrandTotal
        report.TotalBillCount = expenses.Count

        ' Calculate category summaries
        report.CategorySummaries = CalculateCategorySummaries(expenses)

        ' Get high-value bills
        report.HighValueBills = expenses.Where(Function(e) e.Amount >= Constants.HIGH_VALUE_THRESHOLD) _
            .OrderByDescending(Function(e) e.Amount).ToList()

        Return report
    End Function

    ''' <summary>
    ''' Generates a category-wise breakdown report.
    ''' </summary>
    Public Function GenerateCategoryReport(year As Integer, month As Integer) As List(Of CategorySummaryDTO)
        Dim expenses = _expenseRepository.GetByMonth(year, month)
        Return CalculateCategorySummaries(expenses)
    End Function

    ''' <summary>
    ''' Gets a summary of high-value bills for the month.
    ''' </summary>
    Public Function GetHighValueBillsReport(year As Integer, month As Integer) As List(Of Expense)
        Dim expenses = _expenseRepository.GetByMonth(year, month)
        Return expenses.Where(Function(e) e.Amount >= Constants.HIGH_VALUE_THRESHOLD) _
            .OrderByDescending(Function(e) e.Amount).ToList()
    End Function

    ''' <summary>
    ''' Formats the report for printing (returns print-ready string).
    ''' </summary>
    Public Function FormatReportForPrint(report As MonthlyReportDTO) As String
        Dim sb As New System.Text.StringBuilder()

        ' Header
        sb.AppendLine("═══════════════════════════════════════════════════════════════")
        sb.AppendLine($"                    {report.OrganizationName}")
        sb.AppendLine($"                    {report.ReportTitle}")
        sb.AppendLine("═══════════════════════════════════════════════════════════════")
        sb.AppendLine()

        ' Entries table header
        sb.AppendLine($"{"Date",-12} {"Bill No",-15} {"Category",-10} {"Description",-30} {"Amount",12}")
        sb.AppendLine(New String("-"c, 80))

        ' Entries
        For Each entry In report.Entries
            sb.AppendLine($"{entry.EntryDate:dd/MM/yyyy,-12} {entry.BillNo,-15} {entry.CategoryCode,-10} {Truncate(entry.Description, 30),-30} {entry.Amount,12:N2}")
        Next

        sb.AppendLine(New String("-"c, 80))

        ' Category totals
        sb.AppendLine()
        sb.AppendLine("Category-wise Summary:")
        For Each category In report.CategorySummaries
            sb.AppendLine($"  {category.CategoryCode} ({category.CategoryName}): LKR {category.Total:N2} ({category.BillCount} bills)")
        Next

        ' Grand total
        sb.AppendLine()
        sb.AppendLine(New String("="c, 80))
        sb.AppendLine($"{"GRAND TOTAL:",70} LKR {report.GrandTotal,12:N2}")
        sb.AppendLine($"{"REMAINING BALANCE:",70} LKR {report.RemainingBalance,12:N2}")
        sb.AppendLine($"{"TOTAL BILLS:",70} {report.TotalBillCount,12}")
        sb.AppendLine(New String("="c, 80))

        ' Footer with signature lines
        sb.AppendLine()
        sb.AppendLine()
        sb.AppendLine($"Prepared by: ___________________     Date: _______________")
        sb.AppendLine()
        sb.AppendLine($"Checked by:  ___________________     Date: _______________")
        sb.AppendLine()
        sb.AppendLine($"                                     Official Seal: _______________")
        sb.AppendLine()
        sb.AppendLine($"Report generated on: {report.GeneratedAt:dd/MM/yyyy HH:mm}")

        Return sb.ToString()
    End Function

#End Region

#Region "Private Methods"

    Private Function CalculateCategorySummaries(expenses As List(Of Expense)) As List(Of CategorySummaryDTO)
        Return expenses.GroupBy(Function(e) e.CategoryCode) _
            .Select(Function(g) New CategorySummaryDTO With {
                .CategoryCode = g.Key,
                .CategoryName = GetCategoryName(g.Key),
                .Total = g.Sum(Function(e) e.Amount),
                .BillCount = g.Count()
            }).OrderBy(Function(c) c.CategoryCode).ToList()
    End Function

    Private Function GetCategoryName(code As String) As String
        Dim category = _categoryRepository.GetByCode(code)
        If category IsNot Nothing Then Return category.CategoryName
        Return code ' Fallback to code if name not found
    End Function

    Private Function Truncate(text As String, maxLength As Integer) As String
        If String.IsNullOrEmpty(text) Then Return ""
        If text.Length <= maxLength Then Return text
        Return text.Substring(0, maxLength - 3) & "..."
    End Function

#End Region

End Class

#Region "Report DTOs"

''' <summary>
''' Data Transfer Object for monthly report.
''' </summary>
Public Class MonthlyReportDTO
    Public Property Year As Integer
    Public Property Month As Integer
    Public Property MonthName As String
    Public Property OrganizationName As String
    Public Property ReportTitle As String
    Public Property GeneratedAt As DateTime

    Public Property Entries As List(Of ReportEntryDTO)
    Public Property Categories As List(Of CategorySummaryDTO) ' All categories in use for this report
    Public Property CategorySummaries As List(Of CategorySummaryDTO)
    Public Property HighValueBills As List(Of Expense)

    Public Property GrandTotal As Decimal
    Public Property MonthlyLimit As Decimal
    Public Property RemainingBalance As Decimal
    Public Property TotalBillCount As Integer

    ' Aliases for form compatibility
    Public ReadOnly Property TotalAmount As Decimal
        Get
            Return GrandTotal
        End Get
    End Property

    Public ReadOnly Property EntryCount As Integer
        Get
            Return TotalBillCount
        End Get
    End Property

    Public ReadOnly Property CategoryTotals As Dictionary(Of String, Decimal)
        Get
            Dim result As New Dictionary(Of String, Decimal)
            If CategorySummaries IsNot Nothing Then
                For Each cat In CategorySummaries
                    result(cat.CategoryCode) = cat.Total
                Next
            End If
            Return result
        End Get
    End Property

End Class

''' <summary>
''' Single entry in a report.
''' </summary>
Public Class ReportEntryDTO
    Public Property EntryDate As Date
    Public Property BillNo As String
    Public Property CategoryCode As String
    Public Property CategoryName As String
    Public Property Description As String
    Public Property Amount As Decimal
End Class

''' <summary>
''' Category summary in a report.
''' </summary>
Public Class CategorySummaryDTO
    Public Property CategoryCode As String
    Public Property CategoryName As String
    Public Property Total As Decimal
    Public Property BillCount As Integer
End Class

#End Region
