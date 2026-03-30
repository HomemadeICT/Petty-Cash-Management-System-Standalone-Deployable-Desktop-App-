' ============================================================================
' ReportViewerForm.vb - Report Viewer Form Code-Behind
' Petty Cash Management System
' ============================================================================
' Purpose: View and print monthly expense reports using HTML generation.
'          Includes ◀ / ▶ month navigation so users can browse past reports
'          without returning to the Dashboard.
' ============================================================================

Imports System.IO
Imports System.Windows.Forms

Public Class ReportViewerForm

#Region "Private Fields"
    Private _report As MonthlyReportDTO
    Private _reportService As ReportService
    Private _notificationService As NotificationService
    Private _htmlFilePath As String
    Private _currentYear As Integer
    Private _currentMonth As Integer
#End Region

#Region "Constructors"

    ''' <summary>
    ''' Opens the viewer pre-loaded with the given report (legacy path from Dashboard).
    ''' The month navigation bar will be initialised from the report's Year/Month.
    ''' </summary>
    Public Sub New(report As MonthlyReportDTO)
        InitializeComponent()
        _report = report
        _currentYear = report.Year
        _currentMonth = report.Month
    End Sub

    ''' <summary>
    ''' Opens the viewer and immediately generates the report for the given year/month.
    ''' Preferred constructor — used by DashboardForm.
    ''' </summary>
    Public Sub New(year As Integer, month As Integer)
        InitializeComponent()
        _currentYear = year
        _currentMonth = month
        ' _report will be set in Form_Load via RegenerateReport()
    End Sub

#End Region

#Region "Form Events"

    Private Sub ReportViewerForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FormIconHelper.ApplyIcon(Me, FormIconHelper.FormType.Report)
        ' Initialize services
        Dim expenseRepo As New ExpenseRepository()
        Dim categoryRepo As New CategoryRepository()
        _reportService = New ReportService(expenseRepo, categoryRepo)
        _notificationService = New NotificationService()

        ' Ensure navigation buttons are on top
        btnPrevReport.BringToFront()
        btnNextReport.BringToFront()

        ' If we were given a pre-built report use it; otherwise generate fresh
        If _report Is Nothing Then
            RegenerateReport()
        Else
            UpdateNavBar()
            DisplayReport()
        End If
    End Sub

    Private Sub ReportViewerForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Clean up temp HTML file
        Try
            If Not String.IsNullOrEmpty(_htmlFilePath) AndAlso File.Exists(_htmlFilePath) Then
                File.Delete(_htmlFilePath)
            End If
        Catch
            ' Ignore cleanup errors
        End Try
    End Sub

#End Region

#Region "Month Navigation"

    Private Sub btnPrevReport_Click(sender As Object, e As EventArgs) Handles btnPrevReport.Click
        _currentMonth -= 1
        If _currentMonth < 1 Then
            _currentMonth = 12
            _currentYear -= 1
        End If
        RegenerateReport()
    End Sub

    Private Sub btnNextReport_Click(sender As Object, e As EventArgs) Handles btnNextReport.Click
        Dim nextDate = New Date(_currentYear, _currentMonth, 1).AddMonths(1)
        If nextDate <= New Date(Date.Now.Year, Date.Now.Month, 1) Then
            _currentMonth = nextDate.Month
            _currentYear = nextDate.Year
            RegenerateReport()
        End If
    End Sub

    ''' <summary>
    ''' Generates (or re-generates) the report for the current _currentYear/_currentMonth
    ''' and refreshes the navigation bar and HTML display.
    ''' </summary>
    Private Sub RegenerateReport()
        Try
            ' Re-generate from DB
            _report = _reportService.GenerateMonthlyReport(_currentYear, _currentMonth)

            ' Update navigation bar
            UpdateNavBar()

            ' Refresh display
            DisplayReport()

        Catch ex As Exception
            MessageBox.Show($"Error loading report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Updates the navigation bar label and Next button state.
    ''' </summary>
    Private Sub UpdateNavBar()
        Dim currentDate = New Date(_currentYear, _currentMonth, 1)
        Dim displayName = currentDate.ToString("MMMM yyyy")

        ' Update both labels
        lblReportMonth.Text = displayName
        lblReportMonthNav.Text = displayName
        Me.Text = $"Monthly Report - {displayName}"

        ' Disable Next if already at the current real month
        Dim isCurrentMonth = (currentDate >= New Date(Date.Now.Year, Date.Now.Month, 1))
        btnNextReport.Enabled = Not isCurrentMonth
        btnNextReport.BackColor = If(isCurrentMonth,
            System.Drawing.Color.FromArgb(80, 80, 80),
            System.Drawing.Color.FromArgb(0, 80, 160))
    End Sub

#End Region

#Region "Report Display"

    Private Sub DisplayReport()
        Try
            ' Delete previous temp file
            Try
                If Not String.IsNullOrEmpty(_htmlFilePath) AndAlso File.Exists(_htmlFilePath) Then
                    File.Delete(_htmlFilePath)
                End If
            Catch
            End Try

            ' Generate HTML
            Dim htmlContent = ReportHtmlGenerator.GenerateHtmlReport(_report)

            ' Save to new temp file
            _htmlFilePath = Path.Combine(Path.GetTempPath(),
                $"PettyCashReport_{_currentYear}_{_currentMonth}_{DateTime.Now:yyyyMMddHHmmss}.html")
            File.WriteAllText(_htmlFilePath, htmlContent, System.Text.Encoding.UTF8)

            ' Show in WebBrowser
            webReport.Navigate(_htmlFilePath)

        Catch ex As Exception
            MessageBox.Show($"Error generating report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "Button Events"

    Private Sub btnOpenInBrowser_Click(sender As Object, e As EventArgs) Handles btnOpenInBrowser.Click
        Try
            If Not String.IsNullOrEmpty(_htmlFilePath) AndAlso File.Exists(_htmlFilePath) Then
                System.Diagnostics.Process.Start(New System.Diagnostics.ProcessStartInfo() With {
                    .FileName = _htmlFilePath,
                    .UseShellExecute = True
                })
            Else
                MessageBox.Show("Report file not found. Please try regenerating the report.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            MessageBox.Show($"Error opening browser: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            webReport.ShowPrintDialog()
        Catch ex As Exception
            MessageBox.Show($"Error printing: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnSendReport_Click(sender As Object, e As EventArgs) Handles btnSendReport.Click
        btnSendReport.Enabled = False
        btnSendReport.Text = "Sending..."
        Application.DoEvents

        Try
            Dim result = _notificationService.SendMonthlyReport(_report)

            If result.AnySent Then
                MessageBox.Show($"Report sent successfully!{Environment.NewLine}{Environment.NewLine}{result.Summary}",
                              "Report Sent", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Failed to send report. Please check notification settings in Admin Settings.",
                              "Send Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
            MessageBox.Show($"Error sending report: {ex.Message}",
                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btnSendReport.Enabled = True
            btnSendReport.Text = "📧 Send Report"
        End Try
    End Sub

    Private Sub btnExportExcel_Click(sender As Object, e As EventArgs) Handles btnExportExcel.Click
        Try
            Dim exportService As New ExcelExportService()
            Dim suggestedName = exportService.GetDefaultFileName(_report)

            Using sfd As New SaveFileDialog()
                sfd.Filter = "Excel Files (*.xlsx)|*.xlsx"
                sfd.FileName = suggestedName
                sfd.Title = "Export Report to Excel"

                If sfd.ShowDialog() = DialogResult.OK Then
                    btnExportExcel.Enabled = False
                    btnExportExcel.Text = "Exporting..."
                    Application.DoEvents()

                    Dim result = exportService.ExportToExcel(_report, sfd.FileName)

                    If result.IsSuccess Then
                        If MessageBox.Show($"Report exported successfully!{Environment.NewLine}{Environment.NewLine}" &
                                           $"File: {IO.Path.GetFileName(sfd.FileName)}{Environment.NewLine}{Environment.NewLine}" &
                                           "Would you like to open the file?",
                                           "Export Successful", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                            System.Diagnostics.Process.Start(New System.Diagnostics.ProcessStartInfo() With {
                                .FileName = sfd.FileName,
                                .UseShellExecute = True
                            })
                        End If
                    Else
                        MessageBox.Show(result.ErrorMessage, "Export Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End If
            End Using

        Catch ex As Exception
            MessageBox.Show($"Error exporting to Excel: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btnExportExcel.Enabled = True
            btnExportExcel.Text = "📊 Export to Excel"
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

#End Region

End Class