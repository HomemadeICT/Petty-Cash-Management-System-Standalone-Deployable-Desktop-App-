' ============================================================================
' ExcelExportService.vb - Excel (XLSX) Export Service
' Petty Cash Management System
' ============================================================================
' Purpose: Exports MonthlyReportDTO to an .xlsx file that matches the
'          official CEB report format exactly (same layout, B&W, Sinhala)
' Layer:   Business Logic / Presentation Support
' Dependencies: ClosedXML
' ============================================================================

Imports System.IO
Imports ClosedXML.Excel

''' <summary>
''' Exports monthly expense reports to Excel (.xlsx) matching the CEB report layout.
''' </summary>
Public Class ExcelExportService

#Region "Constants — must match ReportHtmlGenerator"
    Private Const CIRCULAR_NO As String = "2024/gm/15/fm-29.02.2024"
    Private Const ACCOUNTS_CIRCULAR_NO As String = "634"
    Private Const OFFICE_LOCATION As String = "ප්‍රා.සේ.ම. - හලිඇල"
    Private Const CHIEF_ENGINEER_TITLE As String = "ප්‍රධාන ඉංජිනේරු බදුල්ල"
    Private Const REPORT_TITLE As String = "සුළු අක් මුදල් ප්‍රතිපූර්ණය කිරිමේ ප්‍රකාශනය"
    Private Const MONTHLY_LIMIT As Decimal = 25000D

    ' Sinhala month names are managed by SinhalaMonthSettings (editable via Admin Settings)
#End Region

#Region "Public Methods"

    ''' <summary>
    ''' Exports the given monthly report to an Excel file matching the CEB report format.
    ''' </summary>
    Public Function ExportToExcel(report As MonthlyReportDTO, filePath As String) As OperationResult
        Try
            Dim dir = Path.GetDirectoryName(filePath)
            If Not String.IsNullOrEmpty(dir) AndAlso Not Directory.Exists(dir) Then
                Directory.CreateDirectory(dir)
            End If

            Using wb As New XLWorkbook()
                BuildReportSheet(wb, report)
                wb.SaveAs(filePath)
            End Using

            Return OperationResult.Success($"Report exported to:{Environment.NewLine}{filePath}")

        Catch ex As Exception
            Return OperationResult.Failure($"Error exporting to Excel: {ex.Message}")
        End Try
    End Function

    ''' <summary>
    ''' Generates the default suggested file name.
    ''' </summary>
    Public Function GetDefaultFileName(report As MonthlyReportDTO) As String
        Return $"PettyCash_Report_{report.Year}_{report.Month:D2}.xlsx"
    End Function

#End Region

#Region "Sheet Builder"

    Private Sub BuildReportSheet(wb As XLWorkbook, report As MonthlyReportDTO)
        Dim ws = wb.Worksheets.Add("Report")

        ' Use a Sinhala-compatible font for the entire sheet
        ws.Style.Font.FontName = "Iskoola Pota"
        ws.Style.Font.FontSize = 11
        ws.Style.Font.FontColor = XLColor.Black

        Dim sinhalaMonth = GetSinhalaMonth(report.Month)
        Dim row = 1

        ' ── HEADER SECTION ────────────────────────────────────────

        Dim totalCols = 3 + report.Categories.Count

        ' Year + Sinhala month (centered)
        ws.Range(row, 1, row, totalCols).Merge()
        With ws.Cell(row, 1)
            .Value = $"{report.Year} {sinhalaMonth}"
            .Style.Font.Bold = True
            .Style.Font.FontSize = 13
            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
        End With
        row += 1

        ' Chief Engineer title (centered)
        ws.Range(row, 1, row, totalCols).Merge()
        With ws.Cell(row, 1)
            .Value = CHIEF_ENGINEER_TITLE
            .Style.Font.Bold = True
            .Style.Font.FontSize = 14
            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
        End With
        row += 1

        ' Report title (centered)
        ws.Range(row, 1, row, totalCols).Merge()
        With ws.Cell(row, 1)
            .Value = REPORT_TITLE
            .Style.Font.Bold = True
            .Style.Font.FontSize = 14
            .Style.Font.Underline = XLFontUnderlineValues.Single
            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
        End With
        row += 1

        ' Blank row
        row += 1

        ' Office location
        ws.Range(row, 1, row, 7).Merge()
        With ws.Cell(row, 1)
            .Value = OFFICE_LOCATION
            .Style.Font.Bold = True
        End With
        row += 1

        ' Blank row
        row += 1

        ' Circular numbers
        ws.Range(row, 1, row, 7).Merge()
        ws.Cell(row, 1).Value = $"Circular No - {CIRCULAR_NO}"
        ws.Cell(row, 1).Style.Font.Bold = True
        row += 1
        ws.Range(row, 1, row, 7).Merge()
        ws.Cell(row, 1).Value = $"Accounts Circular No - {ACCOUNTS_CIRCULAR_NO}"
        ws.Cell(row, 1).Style.Font.Bold = True
        row += 1

        ' Blank row before table
        row += 1

        ' ── TABLE HEADER ──────────────────────────────────────────
        Dim tableStartRow = row
        Dim standardHeaders() As String = {"අනු අංකය", "දිනය", "විස්තරය"}
        
        ' 1. Standard Headers
        For i = 0 To standardHeaders.Length - 1
            Dim cell = ws.Cell(row, i + 1)
            cell.Value = standardHeaders(i)
            ApplyHeaderStyle(cell)
        Next

        ' 2. Dynamic Category Headers
        For i = 0 To report.Categories.Count - 1
            Dim cell = ws.Cell(row, i + 4)
            cell.Value = report.Categories(i).CategoryCode
            ApplyHeaderStyle(cell)
        Next
        row += 1

        ' ── TABLE BODY ────────────────────────────────────────────
        Dim serialNo = 1

        If report.Entries IsNot Nothing Then
            For Each entry In report.Entries
                ' Row data
                ws.Cell(row, 1).Value = serialNo
                ws.Cell(row, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                
                ws.Cell(row, 2).Value = entry.EntryDate.ToString("yy.MM.dd")
                ws.Cell(row, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                
                ws.Cell(row, 3).Value = entry.Description

                ' Dynamic category columns
                For i = 0 To report.Categories.Count - 1
                    Dim catCode = report.Categories(i).CategoryCode
                    Dim cell = ws.Cell(row, i + 4)
                    
                    If entry.CategoryCode = catCode Then
                        cell.Value = entry.Amount
                    Else
                        cell.Value = 0 ' Use numeric 0 instead of "-" for formulas
                    End If
                    
                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                    cell.Style.NumberFormat.Format = "_(* #,##0.00_);_(* (#,##0.00);_(* ""-""??_);_(@_)" ' Professional dash for zero
                Next

                ' Apply borders
                For col = 1 To totalCols
                    ws.Cell(row, col).Style.Border.OutsideBorder = XLBorderStyleValues.Thin
                    ws.Cell(row, col).Style.Border.OutsideBorderColor = XLColor.Black
                Next

                serialNo += 1
                row += 1
            Next
        End If

        ' ── TOTALS ROW ────────────────────────────────────────────
        Dim firstDataRow = tableStartRow + 1
        Dim lastDataRow = row - 1

        ' Merge first 3 columns (empty label region)
        ws.Range(row, 1, row, 3).Merge()
        ws.Cell(row, 1).Value = "එකතුව" ' Totals Label in Sinhala
        ws.Cell(row, 1).Style.Font.Bold = True
        ws.Cell(row, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right

        ' Dynamic category column SUM formulas
        For i = 0 To report.Categories.Count - 1
            Dim colNum = i + 4
            Dim colLetter = GetColumnLetter(colNum)
            Dim cell = ws.Cell(row, colNum)
            
            cell.FormulaA1 = $"=SUM({colLetter}{firstDataRow}:{colLetter}{lastDataRow})"
            cell.Style.Font.Bold = True
            cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
            cell.Style.NumberFormat.Format = "_(* #,##0.00_);_(* (#,##0.00);_(* ""-""??_);_(@_)"
            cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin
            cell.Style.Border.OutsideBorderColor = XLColor.Black
        Next

        Dim totalsRow = row
        row += 1

        ' ── SUMMARY SECTION ───────────────────────────────────────
        row += 1

        ' Total expenses (Sum of category totals)
        ws.Range(row, 1, row, totalCols - 2).Merge()
        ws.Cell(row, 1).Value = "වියදම් වූ මුළු මුදල රු."
        ws.Cell(row, 1).Style.Font.Bold = True
        ws.Range(row, totalCols - 1, row, totalCols).Merge()
        
        Dim totalExpensesRow = row
        Dim firstCatCol = "D"
        Dim lastCatCol = GetColumnLetter(totalCols)
        ws.Cell(row, totalCols - 1).FormulaA1 = $"=SUM({firstCatCol}{totalsRow}:{lastCatCol}{totalsRow})"
        ws.Cell(row, totalCols - 1).Style.Font.Bold = True
        ws.Cell(row, totalCols - 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
        ws.Cell(row, totalCols - 1).Style.NumberFormat.Format = "#,##0.00"
        row += 1

        ' Monthly limit (Static value from constants)
        ws.Range(row, 1, row, totalCols - 2).Merge()
        ws.Cell(row, 1).Value = "සුළු අක් මුදල් සඳහා ලැබීම් රු."
        ws.Cell(row, 1).Style.Font.Bold = True
        ws.Range(row, totalCols - 1, row, totalCols).Merge()
        
        Dim limitRow = row
        ws.Cell(row, totalCols - 1).Value = Constants.MONTHLY_LIMIT
        ws.Cell(row, totalCols - 1).Style.Font.Bold = True
        ws.Cell(row, totalCols - 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
        ws.Cell(row, totalCols - 1).Style.NumberFormat.Format = "#,##0.00"
        row += 1

        ' Remaining balance (Limit - Total)
        ws.Range(row, 1, row, totalCols - 2).Merge()
        ws.Cell(row, 1).Value = "ඉතිරි මුදල රු."
        ws.Cell(row, 1).Style.Font.Bold = True
        ws.Range(row, totalCols - 1, row, totalCols).Merge()
        
        ws.Cell(row, totalCols - 1).FormulaA1 = $"={GetColumnLetter(totalCols - 1)}{limitRow}-{GetColumnLetter(totalCols - 1)}{totalExpensesRow}"
        ws.Cell(row, totalCols - 1).Style.Font.Bold = True
        ws.Cell(row, totalCols - 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
        ws.Cell(row, totalCols - 1).Style.NumberFormat.Format = "#,##0.00"
        row += 1

        ' ── REQUEST TEXT ──────────────────────────────────────────
        row += 1
        ws.Range(row, 1, row, totalCols).Merge()
        
        ' Dynamic formula to display total amount within Sinhala request text
        ws.Cell(row, 1).FormulaA1 = $"=""ඉහත දක්වා ඇත්තේ පා.සේ.ම. සුළු අක් මුදල් වල ගෙවීම සාරාංශයයි. රු "" & TEXT({GetColumnLetter(totalCols - 1)}{totalExpensesRow}, ""#,##0.00"") & "" ක මුදල ප්‍රතිපූර්ණයට අවශ්‍ය කටයුතු සලසා දෙන මෙන් කාරුණිකව ඉල්ලා සිටිමි."""
        
        ws.Cell(row, 1).Style.Alignment.WrapText = True
        ws.Cell(row, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
        ws.Row(row).Height = 45  ' Enough for 2-3 wrapped lines of Sinhala text
        Dim lastRow = row
        row += 1

        ' ── SIGNATURES ────────────────────────────────────────────
        row += 3

        ws.Range(row, 1, row, 3).Merge()
        ws.Cell(row, 1).Value = ".................................................."
        row += 1
        ws.Range(row, 1, row, 3).Merge()
        ws.Cell(row, 1).Value = "විදුලි අධිකාරී"
        ws.Cell(row, 1).Style.Font.Bold = True
        row += 1

        row += 2
        ws.Range(row, 1, row, 3).Merge()
        ws.Cell(row, 1).Value = "ගෙවීම අනුමත කරමි."
        ws.Cell(row, 1).Style.Font.Bold = True
        row += 1

        row += 2
        ws.Range(row, 1, row, 3).Merge()
        ws.Cell(row, 1).Value = ".................................................."
        row += 1
        ws.Range(row, 1, row, 4).Merge()
        ws.Cell(row, 1).Value = "ප්‍රධාන ඉංජිනේරු / විදුලි ඉංජිනේරු (බදුල්ල)"
        ws.Cell(row, 1).Style.Font.Bold = True
        lastRow = row
        row += 1

        ' ── COLUMN WIDTHS ─────────────────────────────────────────
        ws.Column(1).Width = 10   ' Serial No
        ws.Column(2).Width = 12   ' Date
        ws.Column(3).Width = 35   ' Description
        
        ' Dynamic category widths
        For i = 0 To report.Categories.Count - 1
            ws.Column(i + 4).Width = 12
        Next

        ' ── PAGE SETUP (A4 portrait, everything on one page) ─────
        ws.PageSetup.PrintAreas.Add(1, 1, lastRow, totalCols)
        ws.PageSetup.PageOrientation = If(totalCols > 7, XLPageOrientation.Landscape, XLPageOrientation.Portrait) ' Switch to Landscape if too many categories
        ws.PageSetup.PaperSize = XLPaperSize.A4Paper
        ws.PageSetup.Margins.Top = 0.4
        ws.PageSetup.Margins.Bottom = 0.4
        ws.PageSetup.Margins.Left = 0.4
        ws.PageSetup.Margins.Right = 0.4
        ws.PageSetup.CenterHorizontally = True
        ws.PageSetup.FitToPages(1, 1)
    End Sub

    Private Sub ApplyHeaderStyle(cell As IXLCell)
        With cell.Style
            .Font.Bold = True
            .Alignment.Horizontal = XLAlignmentHorizontalValues.Center
            .Alignment.Vertical = XLAlignmentVerticalValues.Center
            .Border.OutsideBorder = XLBorderStyleValues.Thin
            .Border.OutsideBorderColor = XLColor.Black
        End With
    End Sub

    Private Function GetColumnLetter(columnNumber As Integer) As String
        Dim dividend As Integer = columnNumber
        Dim columnName As String = String.Empty
        Dim modulo As Integer

        While dividend > 0
            modulo = (dividend - 1) Mod 26
            columnName = Convert.ToChar(65 + modulo).ToString() & columnName
            dividend = CInt((dividend - modulo) / 26)
        End While

        Return columnName
    End Function

#End Region

#Region "Helpers"

    Private Shared Function GetSinhalaMonth(month As Integer) As String
        ' Delegate to the shared settings helper so user edits are reflected
        Return SinhalaMonthSettings.GetMonthName(month)
    End Function

#End Region

End Class
