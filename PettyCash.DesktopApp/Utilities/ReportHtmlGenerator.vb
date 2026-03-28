' ============================================================================
' ReportHtmlGenerator.vb - HTML Report Generator
' Petty Cash Management System
' ============================================================================
' Purpose: Generates HTML reports for browser-based printing
' ============================================================================

Imports System.Text

''' <summary>
''' Generates HTML reports matching the PHP implementation quality.
''' </summary>
Public Class ReportHtmlGenerator

#Region "Constants"
    Private Const CIRCULAR_NO As String = "2024/gm/15/fm-29.02.2024"
    Private Const ACCOUNTS_CIRCULAR_NO As String = "634"
    Private Const OFFICE_LOCATION As String = "ප්‍රා.සේ.ම. - හලිඇල"
    Private Const CHIEF_ENGINEER_TITLE As String = "ප්‍රධාන ඉංජිනේරු බදුල්ල"
    Private Const REPORT_TITLE As String = "සුළු අක් මුදල් ප්‍රතිපූර්ණය කිරිමේ ප්‍රකාශනය"
    Private Const MONTHLY_LIMIT As Decimal = 25000D
#End Region

#Region "Sinhala Month Names"
    ' Month names are managed by SinhalaMonthSettings (editable via Admin Settings >
    ' "සිංහල මස් නම් සංස්කරණය"). No hardcoded array needed here.
#End Region

#Region "Public Methods"

    ''' <summary>
    ''' Generates a complete HTML report for the given monthly report data.
    ''' </summary>
    Public Shared Function GenerateHtmlReport(report As MonthlyReportDTO) As String
        Dim sb As New StringBuilder()
        Dim sinhalaMonth = GetSinhalaMonth(report.Month)

        ' HTML Document Start
        sb.AppendLine("<!DOCTYPE html>")
        sb.AppendLine("<html lang=""si"">")
        sb.AppendLine("<head>")
        sb.AppendLine("    <meta charset=""UTF-8"">")
        sb.AppendLine($"    <title>Petty Cash Statement - {report.Year}/{report.Month}</title>")
        sb.AppendLine(GetCssStyles())
        sb.AppendLine("</head>")
        sb.AppendLine("<body>")

        ' No-print controls
        sb.AppendLine("    <div class=""no-print"" style=""padding: 10px; background: #f0f0f0; margin-bottom: 20px;"">")
        sb.AppendLine("        <button onclick=""window.print()"" style=""padding: 10px 20px; font-size: 16px; cursor: pointer;"">")
        sb.AppendLine("            🖨️ Print Report")
        sb.AppendLine("        </button>")
        sb.AppendLine("    </div>")

        ' Print Container
        sb.AppendLine("    <div class=""print-container"">")

        ' Header Section
        sb.AppendLine("        <!-- Header -->")
        sb.AppendLine("        <div class=""report-header"" style=""text-align: center; margin-bottom: 10px;"">")
        sb.AppendLine($"            <div style=""font-size: 14px; font-weight: bold;"">{report.Year} {sinhalaMonth}</div>")
        sb.AppendLine($"            <div style=""font-size: 16px; font-weight: bold;"">{CHIEF_ENGINEER_TITLE}</div>")
        sb.AppendLine($"            <div style=""font-size: 16px; font-weight: bold;"">{REPORT_TITLE}</div>")
        sb.AppendLine("        </div>")
        sb.AppendLine()
        sb.AppendLine($"        <div style=""margin-bottom: 5px;""><strong>{OFFICE_LOCATION}</strong></div>")
        sb.AppendLine()

        ' Circular Numbers
        sb.AppendLine("        <!-- Circular Numbers -->")
        sb.AppendLine("        <div class=""circular-info"" style=""margin-bottom: 10px;"">")
        sb.AppendLine($"            <div style=""font-weight: bold;"">Circular No - {CIRCULAR_NO}</div>")
        sb.AppendLine($"            <div style=""font-weight: bold;"">Accounts Circular No - {ACCOUNTS_CIRCULAR_NO}</div>")
        sb.AppendLine("        </div>")
        sb.AppendLine()

        ' Calculate totals
        Dim totalE5200 As Decimal = 0
        Dim totalE5300 As Decimal = 0
        Dim totalE7800 As Decimal = 0

        ' Entries Table
        sb.AppendLine("        <!-- Entries Table -->")
        sb.AppendLine("        <table class=""report-table"" style=""width: 100%; border-collapse: collapse;"">")
        sb.AppendLine("            <thead>")
        sb.AppendLine("                <tr>")
        sb.AppendLine("                    <th style=""width: 40px;"">අනු අංකය</th>")
        sb.AppendLine("                    <th style=""width: 80px;"">දිනය</th>")
        sb.AppendLine("                    <th>විස්තරය</th>")
        sb.AppendLine("                    <th style=""width: 70px;"">E 5200</th>")
        sb.AppendLine("                    <th style=""width: 70px;"">E 5300</th>")
        sb.AppendLine("                    <th style=""width: 70px;"">E 7800</th>")
        sb.AppendLine("                </tr>")
        sb.AppendLine("            </thead>")
        sb.AppendLine("            <tbody>")

        ' Table Body
        If report.Entries IsNot Nothing Then
            Dim serialNo = 1
            For Each entry In report.Entries
                ' Skip E7510 category
                If entry.CategoryCode = "E7510" Then Continue For

                Dim dateStr = entry.EntryDate.ToString("yy.MM.dd")
                Dim e5200Val = If(entry.CategoryCode = "E5200", entry.Amount.ToString("N2"), "-")
                Dim e5300Val = If(entry.CategoryCode = "E5300", entry.Amount.ToString("N2"), "-")
                Dim e7800Val = If(entry.CategoryCode = "E7800", entry.Amount.ToString("N2"), "-")

                ' Update totals
                Select Case entry.CategoryCode
                    Case "E5200" : totalE5200 += entry.Amount
                    Case "E5300" : totalE5300 += entry.Amount
                    Case "E7800" : totalE7800 += entry.Amount
                End Select

                sb.AppendLine("                <tr>")
                sb.AppendLine($"                    <td style=""text-align: center;"">{serialNo}</td>")
                sb.AppendLine($"                    <td style=""text-align: center;"">{dateStr}</td>")
                sb.AppendLine($"                    <td class=""description-cell"">{HtmlEncode(entry.Description)}</td>")
                sb.AppendLine($"                    <td class=""amount-cell"">{e5200Val}</td>")
                sb.AppendLine($"                    <td class=""amount-cell"">{e5300Val}</td>")
                sb.AppendLine($"                    <td class=""amount-cell"">{e7800Val}</td>")
                sb.AppendLine("                </tr>")
                serialNo += 1
            Next
        End If

        sb.AppendLine("            </tbody>")
        sb.AppendLine("            <tfoot>")
        sb.AppendLine("                <tr style=""font-weight: bold;"">")
        sb.AppendLine("                    <td colspan=""3"" style=""text-align: right; border: none;"">&nbsp;</td>")
        sb.AppendLine($"                    <td class=""amount-cell"">{totalE5200:N2}</td>")
        sb.AppendLine($"                    <td class=""amount-cell"">{totalE5300:N2}</td>")
        sb.AppendLine($"                    <td class=""amount-cell"">{totalE7800:N2}</td>")
        sb.AppendLine("                </tr>")
        sb.AppendLine("            </tfoot>")
        sb.AppendLine("        </table>")
        sb.AppendLine()

        ' Summary Section
        Dim totalExpenses = totalE5200 + totalE5300 + totalE7800
        Dim remainingBalance = MONTHLY_LIMIT - totalExpenses

        sb.AppendLine("        <!-- Summary Section -->")
        sb.AppendLine("        <div style=""margin-top: 10px; width: 100%;"">")
        sb.AppendLine("            <table style=""width: 100%; border: none;"">")
        sb.AppendLine("                <tr>")
        sb.AppendLine("                    <td style=""border: none; width: 70%;""><strong>වියදම් වූ මුළු මුදල රු.</strong></td>")
        sb.AppendLine($"                    <td style=""border: none; text-align: right;""><strong>{totalExpenses:N2}</strong></td>")
        sb.AppendLine("                </tr>")
        sb.AppendLine("                <tr>")
        sb.AppendLine("                    <td style=""border: none;""><strong>සුළු අක් මුදල් සඳහා ලැබීම් රු.</strong></td>")
        sb.AppendLine($"                    <td style=""border: none; text-align: right;""><strong>{MONTHLY_LIMIT:N2}</strong></td>")
        sb.AppendLine("                </tr>")
        sb.AppendLine("                <tr>")
        sb.AppendLine("                    <td style=""border: none;""><strong>ඉතිරි මුදල රු.</strong></td>")
        sb.AppendLine($"                    <td style=""border: none; text-align: right;""><strong>{remainingBalance:N2}</strong></td>")
        sb.AppendLine("                </tr>")
        sb.AppendLine("            </table>")
        sb.AppendLine("        </div>")
        sb.AppendLine()

        ' Request Text
        sb.AppendLine("        <!-- Request Text -->")
        sb.AppendLine("        <div style=""margin-top: 20px;"">")
        sb.AppendLine("            <p>")
        sb.AppendLine($"                ඉහත දක්වා ඇත්තේ පා.සේ.ම. සුළු අක් මුදල් වල ගෙවීම සාරාංශයයි. රු <strong>{totalExpenses:N2}</strong> ක මුදල ප්‍රතිපූර්ණයට අවශ්‍ය කටයුතු")
        sb.AppendLine("                සලසා දෙන මෙන් කාරුණිකව ඉල්ලමි.")
        sb.AppendLine("            </p>")
        sb.AppendLine("        </div>")
        sb.AppendLine()

        ' Signatures
        sb.AppendLine("        <!-- Signatures -->")
        sb.AppendLine("        <div class=""signatures"" style=""margin-top: 40px;"">")
        sb.AppendLine("            <div style=""margin-bottom: 40px;"">")
        sb.AppendLine("                <div style=""margin-bottom: 5px;"">..................................................</div>")
        sb.AppendLine("                <strong>විදුලි අධිකාරී</strong>")
        sb.AppendLine("            </div>")
        sb.AppendLine()
        sb.AppendLine("            <div style=""margin-bottom: 50px; font-weight: bold;"">")
        sb.AppendLine("                ගෙවීම අනුමත කරමි.")
        sb.AppendLine("            </div>")
        sb.AppendLine()
        sb.AppendLine("            <div>")
        sb.AppendLine("                <div style=""margin-bottom: 5px;"">..................................................</div>")
        sb.AppendLine("                <strong>ප්‍රධාන ඉංජිනේරු / විදුලි ඉංජිනේරු (බදුල්ල)</strong>")
        sb.AppendLine("            </div>")
        sb.AppendLine("        </div>")
        sb.AppendLine()

        sb.AppendLine("    </div>")
        sb.AppendLine("</body>")
        sb.AppendLine("</html>")

        Return sb.ToString()
    End Function

#End Region

#Region "Private Methods"

    Private Shared Function GetSinhalaMonth(month As Integer) As String
        ' Delegate to the shared settings helper so user edits are reflected
        Return SinhalaMonthSettings.GetMonthName(month)
    End Function

    Private Shared Function HtmlEncode(text As String) As String
        If String.IsNullOrEmpty(text) Then Return String.Empty
        Return text.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("""", "&quot;")
    End Function

    Private Shared Function GetCssStyles() As String
        Return "    <style>
        @media print {
            @page {
                size: A4 portrait;
                margin: 0;
            }

            body {
                margin: 0;
                padding: 15mm;
                -webkit-print-color-adjust: exact;
            }

            .no-print {
                display: none !important;
            }

            .print-container {
                width: 100%;
            }
        }

        body {
            font-family: ""Iskoola Pota"", ""Bindumathi"", ""Nirmala UI"", sans-serif;
            font-size: 12px;
        }

        .report-table th,
        .report-table td {
            border: 1px solid #000;
            padding: 4px;
            vertical-align: middle;
        }

        .amount-cell {
            text-align: right;
            width: 80px;
        }

        .description-cell {
            text-align: left;
        }
    </style>"
    End Function

#End Region

End Class
