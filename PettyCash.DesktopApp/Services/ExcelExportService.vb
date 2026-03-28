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

        ' Year + Sinhala month (centered)
        ws.Range(row, 1, row, 7).Merge()
        With ws.Cell(row, 1)
            .Value = $"{report.Year} {sinhalaMonth}"
            .Style.Font.Bold = True
            .Style.Font.FontSize = 13
            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
        End With
        row += 1

        ' Chief Engineer title (centered)
        ws.Range(row, 1, row, 7).Merge()
        With ws.Cell(row, 1)
            .Value = CHIEF_ENGINEER_TITLE
            .Style.Font.Bold = True
            .Style.Font.FontSize = 14
            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
        End With
        row += 1

        ' Report title (centered)
        ws.Range(row, 1, row, 7).Merge()
        With ws.Cell(row, 1)
            .Value = REPORT_TITLE
            .Style.Font.Bold = True
            .Style.Font.FontSize = 14
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
        Dim headers() As String = {"අනු අංකය", "දිනය", "විස්තරය", "E 5200", "E 5300", "E 7800", "E 7510"}
        For i = 0 To headers.Length - 1
            Dim cell = ws.Cell(row, i + 1)
            cell.Value = headers(i)
            With cell.Style
                .Font.Bold = True
                .Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                .Alignment.Vertical = XLAlignmentVerticalValues.Center
                .Border.OutsideBorder = XLBorderStyleValues.Thin
                .Border.OutsideBorderColor = XLColor.Black
            End With
        Next
        row += 1

        ' ── TABLE BODY ────────────────────────────────────────────
        Dim totalE5200 As Decimal = 0
        Dim totalE5300 As Decimal = 0
        Dim totalE7800 As Decimal = 0
        Dim totalE7510 As Decimal = 0
        Dim serialNo = 1

        If report.Entries IsNot Nothing Then
            For Each entry In report.Entries
                Dim dateStr = entry.EntryDate.ToString("yy.MM.dd")
                Dim e5200Val As String = If(entry.CategoryCode = "E5200", entry.Amount.ToString("N2"), "-")
                Dim e5300Val As String = If(entry.CategoryCode = "E5300", entry.Amount.ToString("N2"), "-")
                Dim e7800Val As String = If(entry.CategoryCode = "E7800", entry.Amount.ToString("N2"), "-")
                Dim e7510Val As String = If(entry.CategoryCode = "E7510", entry.Amount.ToString("N2"), "-")

                ' Update totals
                Select Case entry.CategoryCode
                    Case "E5200" : totalE5200 += entry.Amount
                    Case "E5300" : totalE5300 += entry.Amount
                    Case "E7800" : totalE7800 += entry.Amount
                    Case "E7510" : totalE7510 += entry.Amount
                End Select

                ' Serial No
                ws.Cell(row, 1).Value = serialNo
                ws.Cell(row, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                ' Date
                ws.Cell(row, 2).Value = dateStr
                ws.Cell(row, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                ' Description
                ws.Cell(row, 3).Value = entry.Description

                ' E5200
                ws.Cell(row, 4).Value = e5200Val
                ws.Cell(row, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right

                ' E5300
                ws.Cell(row, 5).Value = e5300Val
                ws.Cell(row, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right

                ' E7800
                ws.Cell(row, 6).Value = e7800Val
                ws.Cell(row, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right

                ' E7510
                ws.Cell(row, 7).Value = e7510Val
                ws.Cell(row, 7).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right

                ' Cell borders (all 7 columns)
                For col = 1 To 7
                    ws.Cell(row, col).Style.Border.OutsideBorder = XLBorderStyleValues.Thin
                    ws.Cell(row, col).Style.Border.OutsideBorderColor = XLColor.Black
                Next

                serialNo += 1
                row += 1
            Next
        End If

        ' ── TOTALS ROW ────────────────────────────────────────────
        ' Merge first 3 columns (empty)
        ws.Range(row, 1, row, 3).Merge()
        ws.Cell(row, 1).Value = ""

        ws.Cell(row, 4).Value = totalE5200.ToString("N2")
        ws.Cell(row, 4).Style.Font.Bold = True
        ws.Cell(row, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right

        ws.Cell(row, 5).Value = totalE5300.ToString("N2")
        ws.Cell(row, 5).Style.Font.Bold = True
        ws.Cell(row, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right

        ws.Cell(row, 6).Value = totalE7800.ToString("N2")
        ws.Cell(row, 6).Style.Font.Bold = True
        ws.Cell(row, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right

        ws.Cell(row, 7).Value = totalE7510.ToString("N2")
        ws.Cell(row, 7).Style.Font.Bold = True
        ws.Cell(row, 7).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right

        For col = 4 To 7
            ws.Cell(row, col).Style.Border.OutsideBorder = XLBorderStyleValues.Thin
            ws.Cell(row, col).Style.Border.OutsideBorderColor = XLColor.Black
        Next
        row += 1

        ' ── SUMMARY SECTION ───────────────────────────────────────
        Dim totalExpenses = totalE5200 + totalE5300 + totalE7800 + totalE7510
        Dim remainingBalance = MONTHLY_LIMIT - totalExpenses

        row += 1

        ' Total expenses
        ws.Range(row, 1, row, 5).Merge()
        ws.Cell(row, 1).Value = "වියදම් වූ මුළු මුදල රු."
        ws.Cell(row, 1).Style.Font.Bold = True
        ws.Range(row, 6, row, 7).Merge()
        ws.Cell(row, 6).Value = totalExpenses.ToString("N2")
        ws.Cell(row, 6).Style.Font.Bold = True
        ws.Cell(row, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
        row += 1

        ' Monthly limit
        ws.Range(row, 1, row, 5).Merge()
        ws.Cell(row, 1).Value = "සුළු අක් මුදල් සඳහා ලැබීම් රු."
        ws.Cell(row, 1).Style.Font.Bold = True
        ws.Range(row, 6, row, 7).Merge()
        ws.Cell(row, 6).Value = MONTHLY_LIMIT.ToString("N2")
        ws.Cell(row, 6).Style.Font.Bold = True
        ws.Cell(row, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
        row += 1

        ' Remaining balance
        ws.Range(row, 1, row, 5).Merge()
        ws.Cell(row, 1).Value = "ඉතිරි මුදල රු."
        ws.Cell(row, 1).Style.Font.Bold = True
        ws.Range(row, 6, row, 7).Merge()
        ws.Cell(row, 6).Value = remainingBalance.ToString("N2")
        ws.Cell(row, 6).Style.Font.Bold = True
        ws.Cell(row, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
        row += 1

        ' ── REQUEST TEXT ──────────────────────────────────────────
        row += 1
        ws.Range(row, 1, row, 7).Merge()
        ws.Cell(row, 1).Value = $"ඉහත දක්වා ඇත්තේ පා.සේ.ම. සුළු අක් මුදල් වල ගෙවීම සාරාංශයයි. රු {totalExpenses:N2} ක මුදල ප්‍රතිපූර්ණයට අවශ්‍ය කටයුතු සලසා දෙන මෙන් කාරුණිකව ඉල්ලා සිටිමි."
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
        ws.Column(4).Width = 12   ' E 5200
        ws.Column(5).Width = 12   ' E 5300
        ws.Column(6).Width = 12   ' E 7800
        ws.Column(7).Width = 12   ' E 7510

        ' ── PAGE SETUP (A4 portrait, everything on one page) ─────
        ws.PageSetup.PrintAreas.Add(1, 1, lastRow, 7)
        ws.PageSetup.PageOrientation = XLPageOrientation.Portrait
        ws.PageSetup.PaperSize = XLPaperSize.A4Paper
        ws.PageSetup.Margins.Top = 0.4
        ws.PageSetup.Margins.Bottom = 0.4
        ws.PageSetup.Margins.Left = 0.4
        ws.PageSetup.Margins.Right = 0.4
        ws.PageSetup.CenterHorizontally = True
        ws.PageSetup.FitToPages(1, 1)

    End Sub

#End Region

#Region "Helpers"

    Private Shared Function GetSinhalaMonth(month As Integer) As String
        ' Delegate to the shared settings helper so user edits are reflected
        Return SinhalaMonthSettings.GetMonthName(month)
    End Function

#End Region

End Class
