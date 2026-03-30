<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ReportViewerForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        pnlHeader = New Panel()
        lblReportMonth = New Label()
        lblReportTitle = New Label()
        pnlNav = New Panel()
        btnPrevReport = New Button()
        lblReportMonthNav = New Label()
        btnNextReport = New Button()
        webReport = New WebBrowser()
        pnlFooter = New Panel()
        btnClose = New Button()
        btnPrint = New Button()
        btnExportExcel = New Button()
        btnOpenInBrowser = New Button()
        btnSendReport = New Button()
        pnlHeader.SuspendLayout()
        pnlFooter.SuspendLayout()
        SuspendLayout()
        ' 
        ' pnlHeader
        ' 
        pnlHeader.BackColor = Color.FromArgb(CByte(0), CByte(51), CByte(102))
        pnlHeader.Controls.Add(pnlNav)
        pnlHeader.Controls.Add(lblReportMonth)
        pnlHeader.Controls.Add(lblReportTitle)
        pnlHeader.Dock = DockStyle.Top
        pnlHeader.Location = New Point(0, 0)
        pnlHeader.Margin = New Padding(3, 4, 3, 4)
        pnlHeader.Name = "pnlHeader"
        pnlHeader.Size = New Size(914, 140)
        pnlHeader.TabIndex = 0
        ' 
        ' lblReportMonth
        ' 
        lblReportMonth.AutoSize = True
        lblReportMonth.Font = New Font("Segoe UI", 12.0F)
        lblReportMonth.ForeColor = Color.LightGray
        lblReportMonth.Location = New Point(23, 67)
        lblReportMonth.Name = "lblReportMonth"
        lblReportMonth.Size = New Size(138, 28)
        lblReportMonth.TabIndex = 1
        lblReportMonth.Text = "February 2026"
        ' 
        ' lblReportTitle
        ' 
        lblReportTitle.AutoSize = True
        lblReportTitle.Font = New Font("Segoe UI", 18.0F, FontStyle.Bold)
        lblReportTitle.ForeColor = Color.White
        lblReportTitle.Location = New Point(23, 20)
        lblReportTitle.Name = "lblReportTitle"
        lblReportTitle.Size = New Size(366, 41)
        lblReportTitle.TabIndex = 0
        lblReportTitle.Text = "Monthly Expense Report"
        ' 
        ' pnlNav (month navigation strip)
        ' 
        pnlNav.BackColor = Color.FromArgb(CByte(0), CByte(40), CByte(85))
        pnlNav.Controls.Add(lblReportMonthNav)
        pnlNav.Controls.Add(btnPrevReport)
        pnlNav.Controls.Add(btnNextReport)
        pnlNav.Dock = DockStyle.Bottom
        pnlNav.Height = 38
        pnlNav.Name = "pnlNav"
        pnlNav.Padding = New Padding(8, 4, 8, 4)
        ' 
        ' btnPrevReport
        ' 
        btnPrevReport.BackColor = Color.FromArgb(CByte(0), CByte(80), CByte(160))
        btnPrevReport.FlatAppearance.BorderSize = 0
        btnPrevReport.FlatStyle = FlatStyle.Flat
        btnPrevReport.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnPrevReport.ForeColor = Color.White
        btnPrevReport.Location = New Point(8, 4)
        btnPrevReport.Name = "btnPrevReport"
        btnPrevReport.Size = New Size(100, 30)
        btnPrevReport.TabIndex = 0
        btnPrevReport.Text = "◀ Prev"
        btnPrevReport.UseVisualStyleBackColor = False
        ' 
        ' btnNextReport
        ' 
        btnNextReport.BackColor = Color.FromArgb(CByte(0), CByte(80), CByte(160))
        btnNextReport.FlatAppearance.BorderSize = 0
        btnNextReport.FlatStyle = FlatStyle.Flat
        btnNextReport.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnNextReport.ForeColor = Color.White
        btnNextReport.Location = New Point(114, 4)
        btnNextReport.Name = "btnNextReport"
        btnNextReport.Size = New Size(100, 30)
        btnNextReport.TabIndex = 2
        btnNextReport.Text = "Next ▶"
        btnNextReport.UseVisualStyleBackColor = False
        ' 
        ' lblReportMonthNav
        ' 
        lblReportMonthNav.Dock = DockStyle.Fill
        lblReportMonthNav.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        lblReportMonthNav.ForeColor = Color.FromArgb(200, 220, 255)
        lblReportMonthNav.Name = "lblReportMonthNav"
        lblReportMonthNav.TabIndex = 1
        lblReportMonthNav.Text = "February 2026"
        lblReportMonthNav.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        ' 
        ' webReport
        ' 
        webReport.Dock = DockStyle.Fill
        webReport.Location = New Point(0, 140)
        webReport.Margin = New Padding(3, 4, 3, 4)
        webReport.MinimumSize = New Size(20, 20)
        webReport.Name = "webReport"
        webReport.Size = New Size(914, 527)
        webReport.TabIndex = 1
        ' 
        ' pnlFooter
        ' 
        pnlFooter.BackColor = Color.FromArgb(CByte(240), CByte(240), CByte(245))
        pnlFooter.Controls.Add(btnClose)
        pnlFooter.Controls.Add(btnPrint)
        pnlFooter.Controls.Add(btnExportExcel)
        pnlFooter.Controls.Add(btnOpenInBrowser)
        pnlFooter.Controls.Add(btnSendReport)
        pnlFooter.Dock = DockStyle.Bottom
        pnlFooter.Location = New Point(0, 667)
        pnlFooter.Margin = New Padding(3, 4, 3, 4)
        pnlFooter.Name = "pnlFooter"
        pnlFooter.Size = New Size(914, 80)
        pnlFooter.TabIndex = 2
        ' 
        ' btnClose
        ' 
        btnClose.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnClose.BackColor = Color.Gray
        btnClose.FlatAppearance.BorderSize = 0
        btnClose.FlatStyle = FlatStyle.Flat
        btnClose.Font = New Font("Segoe UI", 11.0F, FontStyle.Bold)
        btnClose.ForeColor = Color.White
        btnClose.Location = New Point(777, 16)
        btnClose.Margin = New Padding(3, 4, 3, 4)
        btnClose.Name = "btnClose"
        btnClose.Size = New Size(114, 51)
        btnClose.TabIndex = 4
        btnClose.Text = "Close"
        btnClose.UseVisualStyleBackColor = False
        ' 
        ' btnPrint
        ' 
        btnPrint.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnPrint.BackColor = Color.FromArgb(CByte(0), CByte(51), CByte(102))
        btnPrint.FlatAppearance.BorderSize = 0
        btnPrint.FlatStyle = FlatStyle.Flat
        btnPrint.Font = New Font("Segoe UI", 11.0F, FontStyle.Bold)
        btnPrint.ForeColor = Color.White
        btnPrint.Location = New Point(629, 16)
        btnPrint.Margin = New Padding(3, 4, 3, 4)
        btnPrint.Name = "btnPrint"
        btnPrint.Size = New Size(137, 51)
        btnPrint.TabIndex = 3
        btnPrint.Text = "🖨 Print"
        btnPrint.UseVisualStyleBackColor = False
        ' 
        ' btnOpenInBrowser
        ' 
        btnOpenInBrowser.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnOpenInBrowser.BackColor = Color.FromArgb(CByte(34), CByte(139), CByte(34))
        btnOpenInBrowser.FlatAppearance.BorderSize = 0
        btnOpenInBrowser.FlatStyle = FlatStyle.Flat
        btnOpenInBrowser.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnOpenInBrowser.ForeColor = Color.White
        btnOpenInBrowser.Location = New Point(445, 16)
        btnOpenInBrowser.Margin = New Padding(3, 4, 3, 4)
        btnOpenInBrowser.Name = "btnOpenInBrowser"
        btnOpenInBrowser.Size = New Size(173, 51)
        btnOpenInBrowser.TabIndex = 2
        btnOpenInBrowser.Text = "🌐 Open in Browser"
        btnOpenInBrowser.UseVisualStyleBackColor = False
        ' 
        ' btnSendReport
        ' 
        btnSendReport.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        btnSendReport.BackColor = Color.FromArgb(CByte(220), CByte(53), CByte(69))
        btnSendReport.FlatAppearance.BorderSize = 0
        btnSendReport.FlatStyle = FlatStyle.Flat
        btnSendReport.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnSendReport.ForeColor = Color.White
        btnSendReport.Location = New Point(23, 16)
        btnSendReport.Margin = New Padding(3, 4, 3, 4)
        btnSendReport.Name = "btnSendReport"
        btnSendReport.Size = New Size(173, 51)
        btnSendReport.TabIndex = 1
        btnSendReport.Text = "📧 Send Report"
        btnSendReport.UseVisualStyleBackColor = False
        ' 
        ' btnExportExcel
        ' 
        btnExportExcel.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnExportExcel.BackColor = Color.FromArgb(CByte(0), CByte(128), CByte(0))
        btnExportExcel.FlatAppearance.BorderSize = 0
        btnExportExcel.FlatStyle = FlatStyle.Flat
        btnExportExcel.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnExportExcel.ForeColor = Color.White
        btnExportExcel.Location = New Point(207, 16)
        btnExportExcel.Margin = New Padding(3, 4, 3, 4)
        btnExportExcel.Name = "btnExportExcel"
        btnExportExcel.Size = New Size(173, 51)
        btnExportExcel.TabIndex = 5
        btnExportExcel.Text = "📊 Export to Excel"
        btnExportExcel.UseVisualStyleBackColor = False
        ' 
        ' ReportViewerForm
        ' 
        AutoScaleDimensions = New SizeF(8.0F, 20.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(914, 747)
        Controls.Add(webReport)
        Controls.Add(pnlFooter)
        Controls.Add(pnlHeader)
        Margin = New Padding(3, 4, 3, 4)
        MinimumSize = New Size(797, 651)
        Name = "ReportViewerForm"
        StartPosition = FormStartPosition.CenterParent
        Text = "Monthly Report"
        pnlHeader.ResumeLayout(False)
        pnlHeader.PerformLayout()
        pnlFooter.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblReportTitle As Label
    Friend WithEvents lblReportMonth As Label
    Friend WithEvents pnlNav As Panel
    Friend WithEvents btnPrevReport As Button
    Friend WithEvents lblReportMonthNav As Label
    Friend WithEvents btnNextReport As Button
    Friend WithEvents webReport As WebBrowser
    Friend WithEvents pnlFooter As Panel
    Friend WithEvents btnSendReport As Button
    Friend WithEvents btnPrint As Button
    Friend WithEvents btnOpenInBrowser As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents btnExportExcel As Button

End Class
